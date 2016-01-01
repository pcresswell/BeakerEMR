using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Core;
using Beaker.Repository;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Beaker.Repository.SQLite.Test")]
[assembly: InternalsVisibleTo("Beaker.Repository.Test")]
namespace Beaker.Repository.SQLite
{
    public abstract class SQLiteRepository<TPersistable, TTable> : Repository<TPersistable>, ISQLiteRepository where TPersistable : IPersistable, new() where TTable : Table, new()
    {
        internal BeakerSQLiteConnection Connection { get; set; }

        public SQLiteRepository() {}

        public override int Count
        {
            get
            {
                return this.Connection.Count<TTable>();  
            }
        }

        BeakerSQLiteConnection ISQLiteRepository.Connection
        {
            set
            {
                this.Connection = value;
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
