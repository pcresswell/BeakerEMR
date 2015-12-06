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
    public class TestAlternateContact
    {
        [Test]
        public void AlternateContactHasAPersonAsAContact()
        {
            Patient patient = new Patient();
            Person susana = new Person()
            {
                FirstName = "Susana",
                LastName = "Hsu"
            };
            PhoneNumber workPhoneNumber = new PhoneNumber
            {
                AreaCode = "416",
                Number = "345 6789",
                Owner = susana,
                Type = PhoneNumberType.Work
            };
            PhoneNumber homePhoneNumber = new PhoneNumber
            {
                AreaCode = "416",
                Number = "543 7654",
                Owner = susana,
                Type = PhoneNumberType.Home
            };

            PhoneNumber cellPhoneNumber = new PhoneNumber
            {
                AreaCode = "416",
                Number = "454 6565",
                Owner = susana,
                Type = PhoneNumberType.Mobile
            };

            EmailAddress email = new EmailAddress()
            {
                Value = "susana.hsu@gmail.com",
                Type = EmailAddressType.Home,
                Owner = susana
            };

            AlternateContact alternateContact = new AlternateContact()
            {
                Patient = patient,
                ContactPerson = susana,
                Purpose = AlternateContactPurpose.Emergency,
                HomePhoneNumber = homePhoneNumber,
                WorkPhoneNumber = workPhoneNumber,
                CellPhoneNumber = cellPhoneNumber,
                Email = email,
                Note = "Contact in case of emergency."
            };

            Assert.AreEqual(patient, alternateContact.Patient);
            Assert.AreEqual("Susana", alternateContact.ContactPerson.FirstName);
            Assert.AreEqual("Hsu", alternateContact.ContactPerson.LastName);
            Assert.AreEqual(AlternateContactPurpose.Emergency, alternateContact.Purpose);
            Assert.AreEqual(homePhoneNumber, alternateContact.HomePhoneNumber);
            Assert.AreEqual(workPhoneNumber, alternateContact.WorkPhoneNumber);
            Assert.AreEqual(cellPhoneNumber, alternateContact.CellPhoneNumber);
            Assert.AreEqual(email, alternateContact.Email);
            Assert.AreEqual("Contact in case of emergency", alternateContact.Note);
        }
    }
}
