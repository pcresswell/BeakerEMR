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
using Beaker.Test;
using Beaker.Core.Authorize;

namespace Beaker.Repository.SQLite.Test
{
    [TestFixture]
    public class TestPatientRepository : TestHelper
    {
        
        [Test]
        public void SaveAPatient()
        {
            using (SQLiteDatabase database = new SQLiteDatabase(":memory:"))
            {
                Factory.RegisterRepositoriesWithDatabase(database);

                Patient patient = new Patient();
                patient.AuthorID = Guid.NewGuid();
                patient.Note = "Some note";
                patient.Person = new Person();
                
                Assert.IsTrue(Guid.Empty.Equals(patient.ID));
                Assert.IsTrue(!Guid.Empty.Equals(patient.DomainObjectID));

                database.Save<Patient>(patient);
                Assert.IsTrue(database.IsPersisted<Person>(patient.Person));
                Assert.AreEqual(1, database.Count<Person>());
                Assert.IsTrue(database.IsPersisted<Patient>(patient));
                Assert.IsTrue(Guid.Empty != patient.ID);
                Assert.AreEqual(Beaker.Core.Dates.Infinity, patient.RecordEndDateTime);
                Assert.AreEqual(Beaker.Core.Dates.Infinity, patient.ValidEndDateTime);
                Assert.That(patient.RecordStartDateTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
                Assert.That(patient.ValidStartDateTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));

                // Verify properties are actually saved
                Patient loadPatient = database.Find<Patient>(patient.DomainObjectID);

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
