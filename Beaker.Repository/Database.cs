using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beaker.Repository
{
    public abstract class Database : IMigratable , IRepositoryRegistrar
    {
        protected IDictionary<Type, IRepository> Repositories { get; private set; }

        public Database()
        {
            this.Repositories = new Dictionary<Type, IRepository>();
        }

        public virtual void Initialize()
        {
            foreach (var repo in this.Repositories.Values)
            {
                repo.Initialize();
            }
        }

        /// <summary>
        /// Apply the migration but only if it hasn't already been applied.
        /// </summary>
        /// <param name="migration"></param>
        void IMigratable.Apply(IMigration migration)
        {
            IMigratable m = (IMigratable)this;
            if (m.HasMigration(migration)) return;

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

        protected abstract void Persist(IMigration migration);

        /// <summary>
        /// Commit the current transaction.
        /// </summary>
        void IMigratable.CommitTransaction()
        {
            this.CommitTransaction();
        }

        /// <summary>
        /// Has the given migration been applied already?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IMigratable.HasMigration(IMigration migration)
        {
            return this.HasMigration(migration);
        }

        /// <summary>
        /// Returns a repository given the repository type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T IMigratable.Repository<T>()
        {
            return (T)this.Repositories[typeof(T)];
        }

        public T Repository<T>() where T : IRepository
        {
            try
            {
                return ((IMigratable)this).Repository<T>();
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }            
        }

        /// <summary>
        /// Rollback the current transaction.
        /// </summary>
        void IMigratable.RollbackTransaction()
        {
            this.RollbackTransaction();
        }

        void IMigratable.StartTransaction()
        {
            this.StartTransaction();
        }

        protected abstract void StartTransaction();
        protected abstract void RollbackTransaction();
        protected abstract void CommitTransaction();
        protected abstract bool HasMigration(IMigration migration);

        void IRepositoryRegistrar.RegisterRepository<TRepository>(TRepository repository)
        {
            RegisterRepository(repository);
        }

        TRepository IRepositoryRegistrar.Repository<TRepository>()
        {
            return (TRepository)this.Repositories[typeof(TRepository)];
        }

        protected abstract void AfterRegistration<TRepository>(TRepository repository) where TRepository : IRepository;
        
        private void RegisterRepository<TRepository>(TRepository repository) where TRepository : IRepository
        {
            this.Repositories[typeof(TRepository)] = repository;
            this.AfterRegistration<TRepository>(repository);
        }
    }

    public class FailedToApplyMigrationException : Exception
    {
        public FailedToApplyMigrationException() { }
        public FailedToApplyMigrationException(string message) : base(message) { }
        public FailedToApplyMigrationException(string message, Exception inner) : base(message, inner) { }
        
    }
}
