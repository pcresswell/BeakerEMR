using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Repository;
using Beaker.Core;
using Beaker.Repository.SQLite.Tables;

namespace Beaker.Repository.SQLite
{
    public class BeakerSQLiteConnection : IDisposable
    {
        private SQLiteConnection Connection { get; set; }

        public BeakerSQLiteConnection(SQLiteConnection connection)
        {
            this.Connection = connection;
        }

        internal void CommitTransaction()
        {
            this.Connection.Commit();
        }

        internal bool HasMigration(string id)
        {
            return this.Connection.Table<MigrationTable>().Count(m => m.MigrationID == id) > 0;
        }

        internal void RollbackTransaction()
        {
            this.Connection.Rollback();
        }

        internal void StartTransaction()
        {
            this.Connection.BeginTransaction();
        }

        /// <summary>
        /// Finds the version of the object given the datetime and domain object ID value.
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <param name="domainObjectID">The domain object to find.</param>
        /// <param name="onDateTime">Finds the version that was active on this date time.</param>
        /// <returns>The single or default value.</returns>
        internal TTable Find<TTable>(Guid domainObjectID, DateTime onDateTime) where TTable : Beaker.Repository.SQLite.Table, new()
        {
            return this.Connection.Table<TTable>()
                .Where(p => (p.DomainObjectID == domainObjectID) && (p.RecordEndDateTime >= onDateTime && p.RecordStartDateTime <= onDateTime))
                .SingleOrDefault<TTable>();
        }

        /// <summary>
        /// Initialize the given table.
        /// </summary>
        /// <typeparam name="TTable">Type of table.</typeparam>
        internal void Initialize<TTable>() where TTable: Table, new()
        {
            this.Connection.CreateTable<TTable>();
        }


        internal TableQuery<TTable> Table<TTable>() where TTable : Table, new()
        {
            return this.Connection.Table<TTable>();
        }

        /// <summary>
        /// Is the given object persisted.
        /// </summary>
        /// <typeparam name="TTable">Type of table.</typeparam>
        /// <param name="id">Row identifier.</param>
        /// <returns>True if a row with the given id exists. False otherwise.</returns>
        internal bool IsPersisted<TTable>(Guid id) where TTable : Table, new()
        {
            return (this.Get<TTable>(id) != null);
        }

        /// <summary>
        /// Inserts a new record for the given object.
        /// </summary>
        /// <param name="o">object to insert.</param>
        /// <returns></returns>
        internal int Insert(object o)
        {
            return this.Connection.Insert(o);
        }

        /// <summary>
        /// Number of table records.
        /// </summary>
        /// <typeparam name="TTable">Type of table.</typeparam>
        /// <returns></returns>
        internal int Count<TTable>() where TTable : Table, new()
        {
            return this.Connection.Table<TTable>().Count();
        }

        /// <summary>
        /// Updates the given row with the object provided.
        /// </summary>
        /// <param name="o">object to update.</param>
        /// <returns></returns>
        internal int Update(object o)
        {
            return this.Connection.Update(o);
        }

        public void Dispose()
        {
            ((IDisposable)Connection).Dispose();
        }

        /// <summary>
        /// Get the object with the given row id.
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        internal TTable Get<TTable>(Guid id) where TTable : Table , new()
        {
            try
            {
                return this.Connection.Get<TTable>(id);
            }
            catch (InvalidOperationException)
            {
                return default(TTable);
            }
        }
    }
}
