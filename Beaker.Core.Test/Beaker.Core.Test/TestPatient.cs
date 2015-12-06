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
            patient = new Patient
            {
                Person = new Person
                {
                    FirstName = "Peter",
                    LastName = "Cresswell"
                }
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
            Assert.AreEqual("Peter", this.patient.Person.FirstName);
            Assert.AreEqual("Cresswell", this.patient.Person.LastName);
        }

        [Test]
        public void PatientHasAMiddleName()
        {
            this.patient.Person.MiddleName = "Graydon";
            Assert.AreEqual("Graydon", this.patient.Person.MiddleName);
        }

        [Test]
        public void PatientHasAPrefixAndSuffix()
        {
            this.patient.Person.Prefix = "Mr.";
            this.patient.Person.Suffix = "3rd";

            Assert.AreEqual("Mr.", this.patient.Person.Prefix);
            Assert.AreEqual("3rd", this.patient.Person.Suffix);
        }

        [Test]
        public void PatientMayHaveAGender()
        {
            this.patient.Person.Gender = Gender.Male;
            Assert.AreEqual(this.patient.Person.Gender, Gender.Male);
            Assert.AreEqual(this.patient.Person.Gender, new Gender("Male"));
        }

        [Test]
        public void PatientByDefaultHasUnknownGender()
        {
            Assert.AreEqual(this.patient.Person.Gender, Gender.Unknown);
        }

        [Test]
        public void PatientNameIsEmptyByDefault()
        {
            this.patient = new Patient();
            Assert.AreEqual(this.patient.Person.FirstName, string.Empty);
            Assert.AreEqual(this.patient.Person.LastName, string.Empty);
            Assert.AreEqual(this.patient.Person.MiddleName, string.Empty);
            Assert.AreEqual(this.patient.Person.Prefix, string.Empty);
            Assert.AreEqual(this.patient.Person.Suffix, string.Empty);
        }

        [Test]
        public void PatientByDefaultIsBornOnJan11899()
        {
            Assert.AreEqual(this.patient.Person.DateOfBirth, new DateTime(1899, 1, 1));
            this.patient.Person.DateOfBirth = new DateTime(1978, 3, 31);
            Assert.AreEqual(new DateTime(1978, 3, 31), this.patient.Person.DateOfBirth);
        }

        [Test]
        public void PatientMayHaveAPreferredSpokenLanguage()
        {
            patient.Person.PreferredSpokenLanguage = new ISOLanguage("eng", "English");
            Assert.AreEqual(new ISOLanguage("eng", "English"), patient.Person.PreferredSpokenLanguage);
        }

        [Test]
        public void PatientMayHaveAPreferredOfficialLanguage()
        {
            patient.Person.PreferredOfficialLanguage = new ISOLanguage("eng", "English");
            Assert.AreEqual(new ISOLanguage("eng", "English"), patient.Person.PreferredOfficialLanguage);
        }

        [Test]
        public void PatientHasANote()
        {
            patient.Note = "Likes to be called Pete";
            Assert.AreEqual("Likes to be called Pete", patient.Note);
        }
    }
}
