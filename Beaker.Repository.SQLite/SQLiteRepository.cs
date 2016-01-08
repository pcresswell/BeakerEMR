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
using Beaker.Core.Authorize;

[assembly: InternalsVisibleTo("Beaker.Repository.SQLite.Test")]
[assembly: InternalsVisibleTo("Beaker.Repository.Test")]
[assembly: InternalsVisibleTo("Beaker.Test")]
namespace Beaker.Repository.SQLite
{
    /// <summary>
    /// Abstract base class for SQLite Repositories.
    /// </summary>
    public abstract class SQLiteRepository<TPersistable, TTable> : Repository<TPersistable>, ISQLiteRepository<TPersistable> where TPersistable : IPersistable, new() where TTable : Beaker.Repository.SQLite.Table, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Repository.SQLite.SQLiteRepository`2"/> class.
        /// </summary>
        public SQLiteRepository() : base()
        {
        }

        #region implemented abstract members of Repository

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public override int Count
        {
            get
            {
                return this.Connection.Count<TTable>();
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        internal BeakerSQLiteConnection Connection { get; set; }

        /// <summary>
        /// Initialize this instance. Called on startup.
        /// Perform any one time setup during repository initialization.
        /// </summary>
        public override void Initialize()
        {
            this.Connection.Initialize<TTable>();
        }

        /// <summary>
        /// Initialize the repository along with an AutoMapper configuration.
        /// Configure the automapper if required.
        /// </summary>
        /// <param name="autoMapperConfiguration">Auto mapper configuration.</param>
        internal void Initialize(IConfiguration autoMapperConfiguration)
        {
            this.Initialize();
            this.InitializeAutoMapper(autoMapperConfiguration);
        }

        /// <summary>
        /// Determines whether this instance is persisted.
        /// </summary>
        /// <returns>true</returns>
        /// <c>false</c>
        /// <param name="persistable">Persistable.</param>
        public override bool IsPersisted(TPersistable persistable)
        {
            return this.Connection.IsPersisted<TTable>(persistable.ID);
        }

        /// <summary>
        /// Initializes the auto mapper configuration. 
        /// By default, creates a two way map between the 
        /// Domain object and the table.
        /// </summary>
        /// <param name="autoMapper">Auto mapper.</param>
        protected virtual void InitializeAutoMapper(IConfiguration autoMapper)
        {
            this.CreateTwoWayMap<TPersistable, TTable>(autoMapper);
        }

        /// <summary>
        /// Creates a two way map between the types using the automapper configuration.
        /// </summary>
        /// <param name="cfg">Cfg.</param>
        /// <typeparam name="T1">The 1st type parameter.</typeparam>
        /// <typeparam name="T2">The 2nd type parameter.</typeparam>
        protected void CreateTwoWayMap<T1, T2>(IConfiguration cfg)
        {
            cfg.CreateMap<T1, T2>();
            cfg.CreateMap<T2, T1>();
        }

        /// <summary>
        /// Retrieve the object with the given ID from persistence.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override TPersistable Get(Guid id)
        {
            TTable t = this.Connection.Get<TTable>(id);
            if (t == null)
            {
                return default(TPersistable);
            }
            TPersistable persistable = Mapper.Map<TPersistable>(t);
            this.CustomMappingToPersistable(persistable, t);
            return persistable;
        }

        /// <summary>
        /// Find the specified domainObject on the given DateTime.
        /// </summary>
        /// <param name="domainObjectID">Domain object I.</param>
        /// <param name="onDateTime">On date time.</param>
        public override TPersistable Find(Guid domainObjectID, DateTime onDateTime)
        {
            TTable t = this.Connection.Find<TTable>(domainObjectID, onDateTime);
            if (null == t)
            {
                return default(TPersistable);
            }

            return this.Get(t.ID);
        }

        /// <summary>
        /// Insert the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        protected override void Insert(TPersistable persistable)
        {
            TTable table = Mapper.Map<TTable>(persistable);
            this.CustomMappingToTable(table , persistable);
            this.Connection.Insert(table);
        }

        /// <summary>
        /// Update the specified persistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        protected override void Update(TPersistable persistable)
        {
            TTable table = Mapper.Map<TTable>(persistable);
            this.CustomMappingToTable(table , persistable);
            this.Connection.Update(table);
        }

        /// <summary>
        /// Called after AutoMapper has mapped the TTable to the TPersistable.
        /// Used for custom mapping to the TPersistable.
        /// </summary>
        /// <param name="persistable">Persistable.</param>
        /// <param name="table">Table.</param>
        protected abstract void CustomMappingToPersistable(TPersistable persistable, TTable table);

        /// <summary>
        /// Called after AutoMapper has mapped the TPersistable to the TTable.
        /// Update TTable with data from the TPersistable.
        /// </summary>
        /// <returns>The mapping to table.</returns>
        /// <param name="table">Table.</param>
        /// <param name="persistable">Persistable.</param>
        protected abstract void CustomMappingToTable(TTable table, TPersistable persistable);

        /// <summary>
        /// Sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        BeakerSQLiteConnection ISQLiteRepository.Connection
        {
            set
            {
                this.Connection = value;
            }
        }
    }
}
