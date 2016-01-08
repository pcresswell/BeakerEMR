using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Core;
using Beaker.Repository;
using Beaker.Repository.SQLite.Tables;
using Beaker.Core.Authorize;

namespace Beaker.Repository.SQLite
{
    public class PatientRepository : SQLiteRepository<Patient, PatientTable>, IPatientRepository
    {
        public IPersonRepository PersonRepository { get; set; }

        public PatientRepository()
            : base()
        {
        }

        public override void Register(IRepositoryRegistrar registrar)
        {
            registrar.RegisterRepository<IPatientRepository, Patient>(this);
        }


        #region implemented abstract members of SQLiteRepository

        protected override void CustomMappingToPersistable(Patient persistable, PatientTable table)
        {
            if (!Guid.Empty.Equals(table.PersonID))
            {
                persistable.Person = ((IQuery<Person>)this.PersonRepository).Find(table.PersonID);
            }
        }

        #endregion

        #region implemented abstract members of SQLiteRepository

        protected override void CustomMappingToTable(PatientTable table, Patient persistable)
        {
            table.Note = persistable.Note;
            if (persistable.Person != null)
            {
                table.PersonID = persistable.Person.DomainObjectID;
                if (!PersonRepository.IsPersisted(persistable.Person))
                {
                    PersonRepository.Save(persistable.Person);
                }
            }
        }

        #endregion
    }
}
