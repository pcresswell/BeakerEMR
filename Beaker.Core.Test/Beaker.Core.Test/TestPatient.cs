using System;
using NUnit.Framework;
using Beaker.Core;

namespace Beaker.Core.Test
{
    [TestFixture]
    public class TestPatient
    {
        [Test]
        public void TestTruth()
        {
            Assert.IsTrue(true);
        }

        [Test]
        public void TestPatientHasAFirstAndLastName()
        {
            Patient patient = new Patient(firstName: "Peter", lastName: "Cresswell");
            Assert.AreEqual("Peter", patient.FirstName);
            Assert.AreEqual("Cresswell", patient.LastName);
        }
    }
}
