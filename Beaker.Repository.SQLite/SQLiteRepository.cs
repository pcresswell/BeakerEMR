using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Core;
using Beaker.Repository;
using System.Runtime.CompilerServices;
using AutoMapper;
using Beaker.Authorize;

[assembly: InternalsVisibleTo("Beaker.Repository.SQLite.Test")]
[assembly: InternalsVisibleTo("Beaker.Repository.Test")]
[assembly: InternalsVisibleTo("Beaker.Test")]
namespace Beaker.Repository.SQLite
{
    public abstract class SQLiteRepository<TPersistable, TTable> : Repository<TPersistable>, ISQLiteRepository where TPersistable : IPersistable, new() where TTable : Table, new()
    {
        internal BeakerSQLiteConnection Connection { get; set; }

        public SQLiteRepository(ICan can, IAuthor author) : base(can, author)
        {
        }

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

        internal void Initialize(IConfiguration autoMapperConfiguration)
        {
            this.Initialize();
            this.InitializeAutoMapper(autoMapperConfiguration);
        }

        protected abstract void InitializeAutoMapper(IConfiguration autoMapper);

        protected void CreateTwoWayMap<T1, T2>(IConfiguration cfg)
        {
            cfg.CreateMap<T1, T2>();
            cfg.CreateMap<T2, T1>();
        }

        public override bool IsPersisted(TPersistable persistable)
        {
            return this.Connection.IsPersisted<TTable>(persistable.ID);
        }
    }
}
