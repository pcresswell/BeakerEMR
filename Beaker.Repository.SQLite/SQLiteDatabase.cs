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
        internal BeakerSQLiteConnection Connection { get; private set; }

        public SQLiteDatabase(string path)
        {
            this.Connection = new BeakerSQLiteConnection(new SQLiteConnection(path));
            this.Connection.Initialize<MigrationTable>();
        }

        protected override void CommitTransaction()
        {
            this.Connection.CommitTransaction();
        }

        #region implemented abstract members of Database

        protected override void Dispose()
        {
            this.Connection.Dispose();
        }

        #endregion

        protected override bool HasMigration(IMigration migration)
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

        protected override void Persist(IMigration migration)
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

        protected override void AfterRegistration<TRepository>(TRepository repository)
        {
           var sqlRepo =  (ISQLiteRepository)repository;
           sqlRepo.Connection = this.Connection;
        }
    }
}
