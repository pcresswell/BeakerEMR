﻿/*
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
    public class TestFamilyHistory : TestDomainObject<FamilyHealthCondition>
    {
        [Test]
        public void TestAttributes()
        {
            base.AttributeTest("Patient", new Patient(), new Patient());
            base.AttributeTest("StartDate", new FuzzyDateTime(2015, 1, 1), new FuzzyDateTime(2015, 2, 2));
            base.AttributeTest("AgeAtOnset", new FuzzyAge(24,FuzzyAgeAccuracy.Exact), new FuzzyAge(25, FuzzyAgeAccuracy.Exact));
            base.AttributeTest("Issue", "Something", "Different");
            base.AttributeTest("Note", "Something", "Different");
            base.AttributeTest("Relationship", new FamilyRelationship(FamilyRelationshipType.ChildFather), new FamilyRelationship(FamilyRelationshipType.ChildMother));
        }

        [Test]
        public void FamilyHistoryHasAStartDate()
        {
            Patient patient = new Patient();
            FuzzyDateTime startDate = new FuzzyDateTime(2010, 1, 10, FuzzyDateTimeAccuracy.Exact);

            FamilyHealthCondition history = new FamilyHealthCondition()
            {
                Patient = patient,
                StartDate = startDate
            };

            Assert.AreEqual(patient, history.Patient);
            Assert.AreEqual(startDate, history.StartDate);

            
            history.AgeAtOnset = new FuzzyAge(25, FuzzyAgeAccuracy.Exact);
            Assert.AreEqual(new FuzzyAge(25, FuzzyAgeAccuracy.Exact), history.AgeAtOnset);

            history.Issue = "Diabetes";
            Assert.AreEqual("Diabetes", history.Issue);

            history.Note = "A note";
            Assert.AreEqual("A note", history.Note);

            FamilyRelationship father = new FamilyRelationship(FamilyRelationshipType.ChildFather);
            history.Relationship = father;
            Assert.AreEqual(father, history.Relationship);

            history.LifeStage = LifeStage.Adult;
            Assert.AreEqual(LifeStage.Adult, history.LifeStage);
        }
    }
}