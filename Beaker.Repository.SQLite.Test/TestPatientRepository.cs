using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;
using Beaker.Repository.SQLite;
using SQLite;
using Beaker.Core.Medication;
using Beaker.Repository.SQLite.Tables.Medication;
using AutoMapper;

namespace Beaker.Repository.SQLite.Test
{
    [TestFixture]
    public class TestPatientRepository
    {
        [Test]
        public void SaveAPatient()
        {
            using (BeakerSQLiteConnection connection = new BeakerSQLiteConnection(new SQLiteConnection(":memory:")))
            {
                IPersonRepository personRepository = new PersonRepository() { Connection = connection };

                IPatientRepository patientRepository = new PatientRepository() {
                    PersonRepository = personRepository,
                    Connection = connection
                };
               
                patientRepository.Initialize();
                personRepository.Initialize();
                Patient patient = new Patient();
                patient.AuthorID = Guid.NewGuid();
                patient.Note = "Some note";
                patient.Person = new Person();
                
                Assert.IsTrue(Guid.Empty.Equals(patient.ID));
                Assert.IsTrue(!Guid.Empty.Equals(patient.DomainObjectID));

                patientRepository.Save(patient);
                Assert.IsTrue(personRepository.IsPersisted(patient.Person));
                Assert.AreEqual(1, patientRepository.Count);
                Assert.IsTrue(patientRepository.IsPersisted(patient));
                Assert.IsTrue(Guid.Empty != patient.ID);
                Assert.AreEqual(Beaker.Core.Dates.Infinity, patient.RecordEndDateTime);
                Assert.AreEqual(Beaker.Core.Dates.Infinity, patient.ValidEndDateTime);
                Assert.That(patient.RecordStartDateTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
                Assert.That(patient.ValidStartDateTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));

                // Verify properties are actually saved
                Patient loadPatient = patientRepository.Find(patient.DomainObjectID);

                Assert.AreEqual(loadPatient.AuthorID, patient.AuthorID);
                Assert.AreEqual(loadPatient.DomainObjectID, patient.DomainObjectID);
                Assert.AreEqual(loadPatient.ID, patient.ID);
                Assert.AreEqual(loadPatient.Note, patient.Note);
                Assert.AreEqual(loadPatient.Person.ID, patient.Person.ID);
                Assert.AreEqual(loadPatient.ValidEndDateTime, patient.ValidEndDateTime);
                Assert.AreEqual(loadPatient.ValidStartDateTime, patient.ValidStartDateTime);
                Assert.AreEqual(loadPatient.RecordEndDateTime, patient.RecordEndDateTime);
                Assert.AreEqual(loadPatient.RecordStartDateTime, patient.RecordStartDateTime);
                Assert.AreEqual(loadPatient.Note, "Some note");
            }
            
        }
    }
}
