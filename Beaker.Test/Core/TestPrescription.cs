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

namespace Beaker.Test.Core
{
    [TestFixture]
    public class TestPrescription
    {
        [Test]
        public void CreateAPrescription()
        {
            Patient patient = new Patient();
            Provider provider = new Provider();

            Prescription prescription = new Prescription()
            {
                Patient = patient,
                WrittenDate = new DateTime(2014, 1, 1),
                StartDate = new DateTime(2014, 1, 2),
                MedicationName = "Asperin",
                DrugDescription = "Additional description",
                DrugCode = "1234",
                DrugStrength = "10 mg",
                Dosage = "1 tablet",
                DrugForm = "Tablet",
                Frequency = "Daily",
                Duration = "6 days",
                RefillDuration = "9 days",
                Quantity = "30",
                RefillQuantity = "45",
                NumberOfRefills = 4,
                LongTerm = false,
                PastMedication = true,
                PatientCompliance = true,
                Notes = "Some notes",
                Instructions = "Take daily with water on empty stomach",
                PrescriberName = "Peter Cresswell",
                PrescriberBillingNumber = "123456",
                Prescriber = provider,
                PriorPrescription = null,
                TreatmentType = "Some treatment type",
                Status = "Active",
                NonAuthoritative = false,
                DispenseInterval = "5 days",
                SubstitutionsNotAllowed = true,
                DispensingFacilityName = "Shoppers Drug store",
                DispensingFacilityAddress = "123 someplace",
                DispensingFacilityID = "123-ABC",
                EarliestPickupDate = new DateTime(2014, 1, 1),
                ProblemCode = "123",
                ProtocolIdentifier = "ABC",
            };

            Assert.AreEqual(patient, prescription.Patient);
            Assert.AreEqual(new DateTime(2014, 1, 1), prescription.WrittenDate);
            Assert.AreEqual(new DateTime(2014, 1, 2), prescription.StartDate);
            Assert.AreEqual("Asperin", prescription.MedicationName);
            Assert.AreEqual("Additional description", prescription.DrugDescription);
            Assert.AreEqual("1234", prescription.DrugCode);
            Assert.AreEqual("10 mg", prescription.DrugStrength);
            Assert.AreEqual("1 tablet", prescription.Dosage);
            Assert.AreEqual("Tablet", prescription.DrugForm);
            Assert.AreEqual("Daily", prescription.Frequency);
            Assert.AreEqual("6 days", prescription.Duration);
            Assert.AreEqual("9 days", prescription.RefillDuration);
            Assert.AreEqual("30", prescription.Quantity);
            Assert.AreEqual("45", prescription.RefillQuantity);
            Assert.AreEqual(4, prescription.NumberOfRefills);
            Assert.IsFalse(prescription.LongTerm);
            Assert.IsTrue(prescription.PastMedication);
            Assert.IsTrue(prescription.PatientCompliance);
            Assert.AreEqual("Some notes", prescription.Notes);
            Assert.AreEqual("Take daily with water on empty stomach", prescription.Instructions);
            Assert.AreEqual("Peter Cresswell", prescription.PrescriberName);
            Assert.AreEqual("123456", prescription.PrescriberBillingNumber);
            Assert.AreEqual(provider, prescription.Prescriber);
            Assert.IsNull(prescription.PriorPrescription);
            Assert.AreEqual("Some treatment type", prescription.TreatmentType);
            Assert.AreEqual("Active", prescription.Status);
            Assert.IsFalse(prescription.NonAuthoritative);
            Assert.AreEqual("5 days", prescription.DispenseInterval);
            Assert.IsTrue(prescription.SubstitutionsNotAllowed);
            Assert.AreEqual("Shoppers Drug store", prescription.DispensingFacilityName);
            Assert.AreEqual("123 someplace", prescription.DispensingFacilityAddress);
            Assert.AreEqual("123-ABC", prescription.DispensingFacilityID);
            Assert.AreEqual(new DateTime(2014, 1, 1), prescription.EarliestPickupDate);
            Assert.AreEqual("123", prescription.ProblemCode);
            Assert.AreEqual("ABC", prescription.ProtocolIdentifier);
        }
    }
}
