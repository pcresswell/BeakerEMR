using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Core;
using Beaker.Repository;

namespace Beaker.Repository.SQLite
{
    public abstract class SQLiteRepository<TPersistable, TTable> : Repository<TPersistable> where TPersistable : IPersistable, new() where TTable : Table, new()
    {
        protected BeakerSQLiteConnection Connection { get; private set; }
        public SQLiteRepository(BeakerSQLiteConnection connection)
        {
            this.Connection = connection;
        }

        public override int Count
        {
            get
            {
                return this.Connection.Count<TTable>();  
            }
        }

        public override void Initialize()
        {
            this.Connection.Initialize<TTable>();
        }

        public override bool IsPersisted(TPersistable persistable)
        {
            return this.Connection.IsPersisted<TTable>(persistable.ID);
        }
    }
}
