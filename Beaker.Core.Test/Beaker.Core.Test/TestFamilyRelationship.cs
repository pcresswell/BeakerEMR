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

namespace Beaker.Core.Test
{
    [TestFixture]
    public class TestFamilyRelationship
    {
        [Test]
        public void APersonMayHaveAFather()
        {
            Person son = new Person();
            Person father = new Person();

            FamilyRelationship relationship = new FamilyRelationship(FamilyRelationshipType.ChildParent)
            {
                One = son,
                Two = father
            };
            Assert.AreEqual(son, relationship.One);
            Assert.AreEqual(father, relationship.Two);
            Assert.AreEqual("Child-Parent", relationship.Type);
        }

        [Test]
        public void APersonMayHaveASibling()
        {
            Person brother = new Person();
            Person sister = new Person();

            FamilyRelationship sibling = new FamilyRelationship(FamilyRelationshipType.Sibling)
            {
                One = brother,
                Two = sister
            };
            Assert.AreEqual(brother, sibling.One);
            Assert.AreEqual(sister, sibling.Two);
            Assert.AreEqual("Sibling-Sibling", sibling.Type);
        }

        [Test]
        public void APersonMayHaveANeice()
        {
            FamilyRelationship relationship = new FamilyRelationship(FamilyRelationshipType.NeiceUncle);
            Person neice = new Person();
            Person uncle = new Person();

            relationship.One = neice;
            relationship.Two = uncle;
            Assert.AreEqual("Neice-Uncle", relationship.Type);
            Assert.AreEqual(neice, relationship.One);
            Assert.AreEqual(uncle, relationship.Two);
        }
    }
}