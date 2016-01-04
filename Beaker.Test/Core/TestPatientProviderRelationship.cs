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
    public class TestPatientProviderRelationship
    {
        [Test]
        public void RelationshipHasAPatientAndProvider()
        {
            Patient patient = new Patient();
            Provider provider = new Provider();

            PatientProviderRelationship relationship = new PatientProviderRelationship()
            {
                Type = "Primary",
                Patient = patient,
                Provider = provider,
                StartDate = new DateTime(2010,1,3)
            };

            Assert.AreEqual("Primary", relationship.Type);
            Assert.AreEqual(patient, relationship.Patient);
            Assert.AreEqual(provider, relationship.Provider);
            Assert.AreEqual(new DateTime(2010, 1, 3), relationship.StartDate);
            Assert.AreEqual(Dates.Infinity, relationship.EndDate);

            relationship.EndDate = new DateTime(2015, 2, 2);
            Assert.AreEqual(new DateTime(2015, 2, 2), relationship.EndDate);
        }
    }
}
