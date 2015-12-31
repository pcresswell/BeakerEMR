using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Core;

namespace Beaker.Repository.SQLite.Tables
{
    [Table("patients")]
    public class PatientTable : Table
    {
        public PatientTable()
        {
            this.PersonID = Guid.Empty;
        }

        [Column("note")]
        public string Note { get; internal set; }

        [Column("person_id")]
        public Guid PersonID { get; set; }

        /// <summary>
        /// Update the patient record with the values from the table.
        /// </summary>
        /// <param name="patient">Patient to be updated.</param>
        /// <param name="personRepository">Person Repository</param>
        internal void CopyTo(Patient patient, IPersonRepository personRepository)
        {
            base.CopyTo(patient);

            patient.Note = this.Note;
            if (!Guid.Empty.Equals(this.PersonID))
            {
                patient.Person = personRepository.Find(this.PersonID);
            }
        }

        /// <summary>
        /// Updates the table record with the data from the patient.
        /// </summary>
        /// <param name="persistable">The patient with the data.</param>
        /// <param name="personRepository">Person Repository.</param>
        internal void Update(Patient persistable, IPersonRepository personRepository)
        {
            base.Update(persistable);

            this.Note = persistable.Note;
            if (persistable.Person != null)
            {
                this.PersonID = persistable.Person.DomainObjectID;
                if (!personRepository.IsPersisted(persistable.Person))
                {
                    personRepository.Save(persistable.Person);
                }
            }
        }
    }
}
