/*
The MIT License (MIT)

Copyright (c) 2015 Peter Cresswell (pcresswell@gmail.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using NUnit.Framework;
using Beaker.Core;
using Beaker.Repository.Memory;

namespace Beaker.Repository.Test
{
    [TestFixture]
    public class TestRepository
    {
        Patient patient;
        PatientMemoryPersistentStore store;
        Repository<Patient> patientRepository;

        [SetUp]
        public void Setup()
        {
            this.patient = new Patient();
            this.store = new PatientMemoryPersistentStore();
            this.patientRepository = new Repository<Patient>(store);
        }

        [TearDown]
        public void Teardown()
        {
            this.store.Clear();
        }

        [Test]
        public void SavingANewRecordUpdatesTheRecordValue()
        {
            Assert.AreEqual(DateTime.MinValue, patient.RecordStartDateTime);
            Assert.AreEqual(DateTime.MinValue, patient.RecordEndDateTime);
            Assert.AreEqual(DateTime.MinValue, patient.ValidStartDateTime);
            Assert.AreEqual(DateTime.MinValue, patient.ValidEndDateTime);

            patientRepository.Save(patient);

            Assert.That(patient.ValidStartDateTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(patient.RecordStartDateTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));

            Assert.AreEqual(Beaker.Core.Dates.Infinity, patient.RecordEndDateTime);
            Assert.AreEqual(Beaker.Core.Dates.Infinity, patient.ValidEndDateTime);

            // It should also save the object to the persistent store
            Assert.AreEqual(1, this.store.Count);
            Assert.AreEqual(1, this.patient.ID);
        }

        [Test]
        public void SavingAnExistingRecordAddsASecondRecord()
        {
            patientRepository.Save(patient);

            // It should also save the object to the persistent store
            Assert.AreEqual(1, store.Count);
            Assert.AreEqual(1, patient.ID);

            // Every save adds a new row.
            patientRepository.Save(patient);
            Assert.AreEqual(2, store.Count);
            Assert.AreEqual(2, patient.ID);
        }

        [Test]
        public void DeletingARecordMarksTheValidEndDateToNow()
        {
            Assert.IsFalse(patientRepository.IsPersisted(patient));
            patientRepository.Save(patient);
            Assert.IsTrue(patientRepository.IsPersisted(patient));
            Assert.AreEqual(1, store.Count);
            System.Threading.Thread.Sleep(1000);
            patientRepository.Delete(patient);
            Assert.AreEqual(1, store.Count);

            // Deleting a record marks the object as having an end valid datetime
            Assert.That(patient.ValidEndDateTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void DeletingARecordTwiceDoesNothing()
        {
            patientRepository.Save(patient);
            Assert.AreEqual(1, store.Count);
            patientRepository.Delete(patient);
            Assert.AreEqual(1, store.Count);

            // Deleting a record marks the object as having an end valid datetime
            Assert.That(patient.ValidEndDateTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            DateTime endDateTime = patient.ValidEndDateTime;

            System.Threading.Thread.Sleep(1000);
            patientRepository.Delete(patient);
            Assert.AreEqual(endDateTime, patient.ValidEndDateTime);
        }

        [Test]
        public void DeletingARecordWhichHasNeverBeenSavedMarksTheObjectAsEnded()
        {
            patientRepository.Delete(patient);
           
            // Deleting a record marks the object as having an end valid datetime
            Assert.That(patient.ValidEndDateTime, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            DateTime endDateTime = patient.ValidEndDateTime;
        }

        [Test]
        public void FindTheCurrentVersionOfAnEntity()
        {
            patientRepository.Save(patient);
            Assert.AreEqual(patient.ID, patientRepository.Find(patient.DomainObjectID).ID);
        }

        [Test]
        public void FindThePreviousVersionOfAnEntity()
        {
            patient.Note = "Some note";
            patientRepository.Save(patient);
            Assert.AreEqual("Some note", patientRepository.Find(patient.DomainObjectID).Note);

            DateTime firstVersionRecordDateTime = patient.RecordStartDateTime;

            //Now sleep for 5 seconds
            System.Threading.Thread.Sleep(5000);

            // now update the entity
            patient.Note = "Some different note";
            patientRepository.Save(patient);

            Assert.AreEqual("Some different note", patientRepository.Find(patient.DomainObjectID).Note);
        }
    }
}
