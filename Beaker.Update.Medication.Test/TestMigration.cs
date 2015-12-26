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
using Beaker.Core.Medication;
using Beaker.Repository.Memory;
using Beaker.Update.Medication.June2015;
using System.Linq;


namespace Beaker.Repository.Test
{
    [TestFixture]
    public class TestMigration
    {
        [Test]
        public void ApplyMedicationMigration()
        {
            Medication20150601Migration migration = new Medication20150601Migration();
            Database memoryDatabase = new Database();
            memoryDatabase.Apply(migration);
            IMedicationRepository medicationRepository = memoryDatabase.Repository<IMedicationRepository>();
            Assert.AreEqual(16153, medicationRepository.Count);
            Assert.IsTrue(memoryDatabase.HasMigration(migration.ID));

            Medication medication = memoryDatabase.Repository<IMedicationRepository>().FindByDrugCode(47738);
            Assert.IsNotNull(medication);
            Assert.AreEqual("SUCRETS FOR KIDS", medication.Product.BrandName);
            Assert.AreEqual("INSIGHT PHARMACEUTICALS LLC", medication.Company.CompanyName);
            Assert.AreEqual("LOZENGE", medication.Forms.First().PharmaceuticalForm);
            Assert.AreEqual("MG", medication.Ingredients.First().StrengthUnit);
            Assert.AreEqual("18", medication.Packages.First().ProductInformation);
            Assert.AreEqual("MFR", medication.Pharmaceuticals.First().PharmaceuticalSTD);
            Assert.AreEqual("ORAL", medication.Routes.First().RouteOfAdministration);
            Assert.AreEqual("OTC", medication.Schedules.First().ScheduleCode);
            Assert.AreEqual("MARKETED (NOTIFIED)", medication.Statuses.First().StatusCode);
            Assert.AreEqual("52:16.00", medication.Therapeutics.First().TCAHFSNumber);
            
        }
    }
}
