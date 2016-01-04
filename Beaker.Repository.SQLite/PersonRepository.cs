using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Authorize;
using Beaker.Core;
using Beaker.Repository;
using Beaker.Repository.SQLite.Tables;
using SQLite;


namespace Beaker.Repository.SQLite
{
    public class PersonRepository : SQLiteRepository<Person, PersonTable>, IPersonRepository
    {
        public PersonRepository(ICan can, IAuthor author)
            : base(can, author)
        {
        }

        public override void Register(IRepositoryRegistrar registrar)
        {
            if (registrar == null)
                throw new ArgumentNullException("registrar");

            registrar.RegisterRepository<IPersonRepository, Person>(this);
        }

        #region implemented abstract members of SQLiteRepository

        protected override void InitializeAutoMapper(AutoMapper.IConfiguration autoMapper)
        {
            this.CreateTwoWayMap<Person, PersonTable>(autoMapper);
        }

        #endregion

        protected override Person Find(Guid domainObjectID, DateTime onDateTime)
        {
            PersonTable personTable = this.Connection.Find<PersonTable>(domainObjectID, onDateTime);

            if (personTable == null)
            {
                return default(Person);
            }

            return CreatePerson(personTable);
        }

        protected override Person Get(Guid id)
        {
            PersonTable personTable = this.Connection.Get<PersonTable>(id);

            if (personTable == null)
            {
                return default(Person);
            }

            return CreatePerson(personTable);
        }

        protected override void Insert(Person persistable)
        {
            PersonTable personTable = new PersonTable();
            personTable.Update(persistable);
            this.Connection.Insert(personTable);
        }

        protected override void Update(Person persistable)
        {
            PersonTable personTable = this.Connection.Get<PersonTable>(persistable.ID);
            personTable.Update(persistable);
            this.Connection.Update(personTable);
        }

        private Person CreatePerson(PersonTable personTable)
        {
            var person = new Person();
            personTable.CopyTo(person);
            return person;
        }
    }
}
