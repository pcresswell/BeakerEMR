using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beaker.Repository
{
    public abstract class Database : IMigratable
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
        void IMigratable.Apply(Migration migration)
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

        protected abstract void Persist(Migration migration);

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
        bool IMigratable.HasMigration(Migration migration)
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
            return ((IMigratable)this).Repository<T>();
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
        protected abstract bool HasMigration(Migration migration);


    }

    public class FailedToApplyMigrationException : Exception
    {
        public FailedToApplyMigrationException() { }
        public FailedToApplyMigrationException(string message) : base(message) { }
        public FailedToApplyMigrationException(string message, Exception inner) : base(message, inner) { }
        
    }
}
