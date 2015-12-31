using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Core;
using Beaker.Repository;
using Beaker.Repository.SQLite.Tables;

namespace Beaker.Repository.SQLite
{ 
    public class PatientRepository : SQLiteRepository<Patient, PatientTable>, IPatientRepository
    {
        public IPersonRepository PersonRepository { get; set; }

        public PatientRepository(BeakerSQLiteConnection connection) : base(connection)
        {
        }

        protected override Patient Find(Guid domainObjectID, DateTime onDateTime)
        {
            PatientTable patientTable = this.Connection.Find<PatientTable>(domainObjectID, onDateTime);

            if (patientTable == null)
            {
                return default(Patient);
            }

            return this.CreatePatient(patientTable);
        }

        protected override Patient Get(Guid id)
        {
            PatientTable patientTable = this.Connection.Get<PatientTable>(id);

            if (patientTable == null)
            {
                return default(Patient);
            }

            return this.CreatePatient(patientTable);
        }

        private Patient CreatePatient(PatientTable p)
        {
            Patient patient = new Patient();
            p.CopyTo(patient, this.PersonRepository);
            return patient;
        }

        protected override void Insert(Patient persistable)
        {
            PatientTable patientTable = new PatientTable();
            patientTable.Update(persistable, this.PersonRepository);
            this.Connection.Insert(patientTable);
        }

        protected override void Update(Patient persistable)
        {
            PatientTable patientTable = this.Connection.Get<PatientTable>(persistable.ID);
            patientTable.Update(persistable, this.PersonRepository);
            this.Connection.Update(patientTable);
        }
    }
}
