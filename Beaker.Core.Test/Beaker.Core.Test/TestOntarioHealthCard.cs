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
    public class TestOntarioHealthCard
    {
        private OntarioHealthCard healthCard;
        [Test]
        public void TestHealthCardHasANumber()
        {
            this.healthCard = new OntarioHealthCard();
            this.healthCard.Number = "1234567890";
            Assert.AreEqual(this.healthCard.Number, "1234567890");
        }

        [Test]
        public void TestHealthCardHasAVersion()
        {
            this.healthCard = new OntarioHealthCard() { Version = "VR" };
            Assert.AreEqual("VR", this.healthCard.Version);
        }

        [Test]
        public void TestHealthCardHasExpiryDate()
        {
            this.healthCard = new OntarioHealthCard() { ExpiryDate = new DateTime(2020, 1, 2) };
            Assert.AreEqual(new DateTime(2020, 1, 2), healthCard.ExpiryDate);
        }

        [Test]
        public void TestHealthCardHasAnOwner()
        {
            Patient patient = new Patient();
            this.healthCard = new OntarioHealthCard() { Owner = patient };
            Assert.AreEqual(patient, healthCard.Owner);
        }
    }
}
