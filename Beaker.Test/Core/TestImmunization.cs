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

namespace Beaker.Core.Test
{
    [TestFixture]
    public class TestImmunization
    {
        [Test]
        public void CreateImmunization()
        {
            Patient patient = new Patient();
            Immunization immunization = new Immunization()
            {
                Patient = patient,
                Name = "Name",
                Code = "Code",
                Type = "Some type",
                Manufacturer = "Some manufacturer",
                LotNumber = "123456",
                Route = "route",
                Site = "Site",
                Dose = "Some dose",
                Date = new FuzzyDateTime(2014, 2, 2),
                RefusalDate = new FuzzyDateTime(2014, 2, 2),
                Refused = true,
                Instructions = "Some instructions",
                Notes = "Some notes"
            };

            Assert.AreEqual(patient, immunization.Patient);
            Assert.AreEqual("Name", immunization.Name);
            Assert.AreEqual("Code", immunization.Code);
            Assert.AreEqual("Some type", immunization.Type);
            Assert.AreEqual("Some manufacturer", immunization.Manufacturer);
            Assert.AreEqual("123456", immunization.LotNumber);
            Assert.AreEqual("route", immunization.Route);
            Assert.AreEqual("Site", immunization.Site);
            Assert.AreEqual("Some dose", immunization.Dose);
            Assert.AreEqual(new FuzzyDateTime(2014, 2, 2), immunization.Date);
            Assert.AreEqual(new FuzzyDateTime(2014, 2, 2), immunization.RefusalDate);
            Assert.IsTrue(immunization.Refused);
            Assert.AreEqual("Some instructions", immunization.Instructions);
            Assert.AreEqual("Some notes", immunization.Notes);
        }
    }
}
