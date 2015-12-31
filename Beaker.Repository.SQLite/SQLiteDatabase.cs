using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Repository;
using SQLite;
using Beaker.Repository.SQLite.Tables;
using AutoMapper;

namespace Beaker.Repository.SQLite
{
    public class SQLiteDatabase : Database
    {
        public BeakerSQLiteConnection Connection { get; private set; }

        public SQLiteDatabase(BeakerSQLiteConnection connection)
        {
            this.Connection = connection;
            this.Repositories[typeof(IMedicationRepository)] = new MedicationRepository(this.Connection);
            this.Connection.Initialize<MigrationTable>();
        }

        protected override void CommitTransaction()
        {
            this.Connection.CommitTransaction();
        }

        protected override bool HasMigration(Migration migration)
        {
            return this.Connection.HasMigration(migration.ID);
        }

        protected override void RollbackTransaction()
        {
            this.Connection.RollbackTransaction();
        }

        protected override void StartTransaction()
        {
            this.Connection.StartTransaction();
        }

        protected override void Persist(Migration migration)
        {
            MigrationTable t = new MigrationTable();
            t.MigrationID = migration.ID;
            t.DomainObjectID = Guid.NewGuid();
            t.ID = Guid.NewGuid();
            t.RecordStartDateTime = DateTime.UtcNow;
            t.ValidStartDateTime = DateTime.UtcNow;
            t.RecordEndDateTime = Beaker.Core.Dates.Infinity;
            t.ValidEndDateTime = Beaker.Core.Dates.Infinity;
            this.Connection.Insert(t);
        }
    }
}
