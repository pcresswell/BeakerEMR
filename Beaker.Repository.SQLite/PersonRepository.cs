using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core.Authorize;
using Beaker.Core;
using Beaker.Repository;
using Beaker.Repository.SQLite.Tables;
using SQLite;


namespace Beaker.Repository.SQLite
{
    public class PersonRepository : SQLiteRepository<Person, PersonTable>, IPersonRepository
    {
        public PersonRepository()
            : base()
        {
        }

        public override void Register(IRepositoryRegistrar registrar)
        {
            registrar.RegisterRepository<IPersonRepository, Person>(this);
        }

        #region implemented abstract members of SQLiteRepository



        #endregion

        private Person CreatePerson(PersonTable personTable)
        {
            var person = new Person();
            personTable.CopyTo(person);
            return person;
        }


        #region implemented abstract members of SQLiteRepository
        protected override void CustomMappingToPersistable(Person persistable, PersonTable table)
        {
            // nothing to do
        }
        #endregion

        #region implemented abstract members of SQLiteRepository

        protected override void CustomMappingToTable(PersonTable table, Person persistable)
        {
            
        }

        #endregion
    }
}
