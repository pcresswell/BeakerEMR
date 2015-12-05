using System;
using NUnit.Framework;
using Beaker.Core;

namespace Beaker.Core.Test
{
    [TestFixture]
    public class TestPatient
    {
        [SetUp]
        public void Setup()
        {
            patient = new Patient {
                FirstName = "Peter",
                LastName = "Cresswell"
            };    
        }

        private Patient patient;

        [Test]
        public void TestTruth()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void PatientHasAFirstAndLastName()
        {
            Assert.AreEqual("Peter", this.patient.FirstName);
            Assert.AreEqual("Cresswell", this.patient.LastName);
        }

        [Test]
        public void PatientHasAMiddleName()
        {
            this.patient.MiddleName = "Graydon";
            Assert.AreEqual("Graydon", this.patient.MiddleName);
        }

        [Test]
        public void PatientHasAPrefixAndSuffix()
        {
            this.patient.Prefix = "Mr.";
            this.patient.Suffix = "3rd";

            Assert.AreEqual("Mr.", this.patient.Prefix);
            Assert.AreEqual("3rd", this.patient.Suffix);
        }
       
        [Test]
        public void PatientMayHaveAGender()
        {
            this.patient.Gender = Gender.Male;
            Assert.AreEqual(this.patient.Gender, Gender.Male);
            Assert.AreEqual(this.patient.Gender, new Gender("Male"));
        }

        [Test]
        public void PatientByDefaultHasUnknownGender()
        {
            Assert.AreEqual(this.patient.Gender, Gender.Unknown);
        }

        [Test]
        public void PatientNameIsEmptyByDefault()
        {
           this. patient = new Patient();
            Assert.AreEqual(this.patient.FirstName, string.Empty);
            Assert.AreEqual(this.patient.LastName, string.Empty);
            Assert.AreEqual(this.patient.MiddleName, string.Empty);
            Assert.AreEqual(this.patient.Prefix, string.Empty);
            Assert.AreEqual(this.patient.Suffix, string.Empty);
        }

        [Test]
        public void PatientByDefaultIsBornOnJan11899()
        {
            Assert.AreEqual(this.patient.DateOfBirth, new DateTime(1899, 1, 1));
            this.patient.DateOfBirth = new DateTime(1978, 3, 31);
            Assert.AreEqual(new DateTime(1978, 3, 31), this.patient.DateOfBirth);
        }

    }
}
