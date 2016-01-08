// The MIT License (MIT)
//
// Copyright (c) 2015 Peter Cresswell (pcresswell@gmail.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace Beaker.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Beaker.Core;
    using Beaker.Repository;

    /// <summary>
    /// Base class for all database implementations.
    /// </summary>
    public abstract class Database : IDatabase, IMigratable, IRepositoryRegistrar, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Repository.Database"/> class.
        /// </summary>
        public Database()
        {
            this.Repositories = new Dictionary<Type, object>();
            this.RepositoriesByDomainObjectDictionary = new Dictionary<Type, IRepository>();
        }

        /// <summary>
        /// Gets the repositories by repository interface,
        /// </summary>
        /// <value>The repositories.</value>
        protected IDictionary<Type, object> Repositories { get; private set; }

        /// <summary>
        /// Gets the repositories by domain object dictionary.
        /// </summary>
        /// <value>The repositories by domain object dictionary.</value>
        protected IDictionary<Type, IRepository> RepositoriesByDomainObjectDictionary { get; private set; }

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public virtual void Initialize()
        {
            foreach (var repo in this.Repositories.Values)
            {
                ((IRepository)repo).Initialize();
            }
        }

        public TPersistable Find<TPersistable>(Guid domainObjectID) where TPersistable : IPersistable
        {
            IQuery<TPersistable> repo = (IQuery<TPersistable>)this.RepositoriesByDomainObjectDictionary[typeof(TPersistable)];
            return repo.Find(domainObjectID);
        }

        public int Count<TPersistable>() where TPersistable : IPersistable
        {
            IRepository<TPersistable> repo = (IRepository<TPersistable>)this.RepositoriesByDomainObjectDictionary[typeof(TPersistable)];
            return repo.Count;
        }

        public bool IsPersisted<TPersistable>(TPersistable persistable) where TPersistable : IPersistable
        {
            IRepository<TPersistable> repo = (IRepository<TPersistable>)this.RepositoriesByDomainObjectDictionary[typeof(TPersistable)];
            return repo.IsPersisted(persistable);
        }

        /// <summary>
        /// Save the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        /// <typeparam name="TPersistable">The 1st type parameter.</typeparam>
        void IDatabase.Save<TPersistable>(TPersistable persistable)
        {
            this.Save(persistable);
        }

        /// <summary>
        /// Save the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        /// <typeparam name="TPersistable">The 1st type parameter.</typeparam>
        public void Save<TPersistable>(TPersistable persistable) where TPersistable : IPersistable
        {
            IRepository<TPersistable> repo = (IRepository<TPersistable>)this.RepositoriesByDomainObjectDictionary[typeof(TPersistable)];
            repo.Save(persistable);
        }

        /// <summary>
        /// Delete the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        /// <typeparam name="TPersistable">The 1st type parameter.</typeparam>
        void IDatabase.Delete<TPersistable>(TPersistable persistable)
        {
            this.Delete(persistable);
        }

        /// <summary>
        /// Delete the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        /// <typeparam name="TPersistable">The 1st type parameter.</typeparam>
        public void Delete<TPersistable>(TPersistable persistable) where TPersistable : IPersistable
        {
            IRepository<TPersistable> repo = (IRepository<TPersistable>)this.RepositoriesByDomainObjectDictionary[typeof(TPersistable)];
            repo.Delete(persistable);
        }

        /// <summary>
        /// Apply the migration but only if it hasn't already been applied.
        /// </summary>
        /// <param name="migration">The migration.</param>
        void IMigratable.Apply(IMigration migration)
        {
            IMigratable m = (IMigratable)this;
            if (m.HasMigration(migration))
            {
                return;
            }

            try
            {
                this.StartTransaction();
                migration.Apply(this);
                this.Persist(migration);
                this.CommitTransaction();
            }
            catch (RollbackException)
            {
                this.RollbackTransaction();
                throw new FailedToApplyMigrationException("Failed to apply the migration.");
            }
            catch (Exception ex)
            {
                this.RollbackTransaction();
                throw new FailedToApplyMigrationException("Failed to apply the migration.", ex);
            }
        }

        /// <summary>
        /// Persist the specified migration.
        /// </summary>
        /// <param name="migration">Migration.</param>
        protected abstract void Persist(IMigration migration);

        /// <summary>
        /// Starts the transaction.
        /// </summary>
        protected abstract void StartTransaction();

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        protected abstract void RollbackTransaction();

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        protected abstract void CommitTransaction();

        /// <summary>
        /// Called after the repository is registered.
        /// </summary>
        /// <param name="repository">Repository</param>
        /// <typeparam name="TRepository">Repository interface.</typeparam>
        protected abstract void AfterRegistration<TRepository>(TRepository repository) where TRepository : IRepository;

        /// <summary>
        /// Determines whether this instance has migration the specified migration.
        /// </summary>
        /// <returns><c>true</c> if this instance has migration the specified migration; otherwise, <c>false</c>.</returns>
        /// <param name="migration">Migration.</param>
        protected abstract bool HasMigration(IMigration migration);

        /// <summary>
        /// Commit the current transaction.
        /// </summary>
        void ITransactable.CommitTransaction()
        {
            foreach (var repository in this.Repositories.Values)
            {
                ((ITransactionTimestamp)repository).TransactionDateTime = DateTime.MinValue;
            }

            this.CommitTransaction();
        }

        /// <summary>
        /// Determines whether this instance has migration the specified migration.
        /// </summary>
        /// <returns><c>true</c> if this instance has migration the specified migration; otherwise, <c>false</c>.</returns>
        /// <param name="migration">Migration.</param>
        bool IMigratable.HasMigration(IMigration migration)
        {
            return this.HasMigration(migration);
        }

        /// <summary>
        /// Repository this instance.
        /// </summary>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        T IMigratable.Repository<T>()
        {
            return (T)this.Repositories[typeof(T)];
        }

        /// <summary>
        /// Rollback the current transaction.
        /// </summary>
        void ITransactable.RollbackTransaction()
        {
            foreach (var repository in this.Repositories.Values)
            {
                ((ITransactionTimestamp)repository).TransactionDateTime = DateTime.MinValue;
            }

            this.RollbackTransaction();
        }

        /// <summary>
        /// Starts the transaction.
        /// </summary>
        void ITransactable.StartTransaction()
        {
            foreach (var repository in this.Repositories.Values)
            {
                ((ITransactionTimestamp)repository).TransactionDateTime = DateTime.UtcNow;
            }

            this.StartTransaction();
        }


        /// <summary>
        /// Registers the repository.
        /// </summary>
        /// <param name="repository">Repository.</param>
        /// <typeparam name="TRepository">Repository interface</typeparam>
        /// <typeparam name="TPersistable">Persistable interface.</typeparam>
        void IRepositoryRegistrar.RegisterRepository<TRepository, TPersistable>(TRepository repository)
        {
            this.RegisterRepository<TRepository, TPersistable>(repository);
        }

        /// <summary>
        /// Get the repository by interface.
        /// </summary>
        /// <typeparam name="TRepository">Repository interface.</typeparam>
        TRepository IRepositoryRegistrar.Repository<TRepository>()
        {
            return (TRepository)this.Repositories[typeof(TRepository)];
        }

        #region IDisposable implementation

        /// <summary>
        /// Releases all resource used by the <see cref="Beaker.Repository.Database"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Beaker.Repository.Database"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="Beaker.Repository.Database"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="Beaker.Repository.Database"/> so the garbage collector can reclaim the memory that the
        /// <see cref="Beaker.Repository.Database"/> was occupying.</remarks>
        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        /// <summary>
        /// Releases all resource used by the <see cref="Beaker.Repository.Database"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Beaker.Repository.Database"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="Beaker.Repository.Database"/> in an unusable state. After
        /// calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="Beaker.Repository.Database"/> so the garbage collector can reclaim the memory that the
        /// <see cref="Beaker.Repository.Database"/> was occupying.</remarks>
        protected abstract void Dispose();

        #endregion



        /// <summary>
        /// Registers the repository.
        /// </summary>
        /// <param name="repository">Repository.</param>
        /// <typeparam name="TRepository">The 1st type parameter.</typeparam>
        /// <typeparam name="TPersistable">The 2nd type parameter.</typeparam>
        private void RegisterRepository<TRepository, TPersistable>(TRepository repository) where TRepository : IRepository where TPersistable : IPersistable
        {
            this.Repositories[typeof(TRepository)] = repository;
            this.RepositoriesByDomainObjectDictionary[typeof(TPersistable)] = repository;
            this.AfterRegistration<TRepository>(repository);
        }

        #region IQueryable implementation

        public TQuery Queries<TQuery>() where TQuery : IQuery
        {
            foreach (var repository in this.Repositories.Values)
            {
                if (repository is TQuery)
                {
                    return (TQuery)repository;
                }
            }

            throw new ArgumentException("No such Query interface exists.");
        }

        #endregion
    }
}
