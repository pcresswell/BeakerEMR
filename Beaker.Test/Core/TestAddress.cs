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
using System.Reflection;
using Beaker.Core;

namespace Beaker.Core.Test
{
    [TestFixture]
    public class TestAddress : TestDomainObject<Address>
    {
        [Test]
        public void AddressProperties()
        {
            Person patient = new Person();

            Address address = new Address()
            {
                Type = AddressType.Home,
                Street = "109 Clydesdale Drive",
                PostalCode = "M2J 3N3",
                City = "Toronto",
                Province = "Ontario",
                Country = "Canada",
                Resident = patient
            };

            Assert.AreEqual(address.Type, AddressType.Home);
            Assert.AreEqual("109 Clydesdale Drive", address.Street);
            Assert.AreEqual("M2J 3N3", address.PostalCode);
            Assert.AreEqual("Toronto", address.City);
            Assert.AreEqual("Ontario", address.Province);
            Assert.AreEqual("Canada", address.Country);
            Assert.AreEqual(patient, address.Resident);
        }

        [TestCase("City", "Toronto", "Brampton")]
        [TestCase("Country", "Canada", "America")]
        [TestCase("PostalCode", "123", "456")]
        [TestCase("Street", "Samsung", "Toshiba")]
        [TestCase("Type", "Home", "Work")]
        public void TestAddressSameAsAttributes(string propertyName, object valueOne, object valueTwo)
        {
            base.AttributeTest(propertyName, valueOne, valueTwo);
        }

        [Test]
        public void TestSameAsResident()
        {
            base.AttributeTest("Resident", new Person() { FirstName = "Peter" }, new Person() { FirstName = "Susana" });
        }
    }
}
