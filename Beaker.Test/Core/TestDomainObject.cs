// /*
// The MIT License (MIT)
//
// Copyright (c) 2015 Peter Cresswell (pcresswell@gmail.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// */

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;

namespace Beaker.Test.Core
{
    
    public class TestDomainObject<T> where T : DomainObject, new() 
    {
        protected T DomainObject;
        
        [Test]
        public void TestDomainObjectAttributes()
        {
            T t = new T();
            Guid g = Guid.NewGuid();

            t.AuthorID = g;
            t.DomainObjectID = g;
            t.ID = g;
            DateTime d = DateTime.Now;

            t.RecordEndDateTime = d;
            t.RecordStartDateTime = d;
            t.ValidEndDateTime = d;
            t.ValidStartDateTime = d;

            Assert.AreEqual(g, t.AuthorID);
            Assert.AreEqual(g, t.DomainObjectID);
            Assert.AreEqual(g, t.ID);

            Assert.AreEqual(d, t.RecordEndDateTime);
            Assert.AreEqual(d, t.RecordStartDateTime);
            Assert.AreEqual(d, t.ValidEndDateTime);
            Assert.AreEqual(d, t.ValidStartDateTime);
             
        }

        public void AttributeTest(string propertyName, object valueOne, object valueTwo)
        {
            T t = new T();
            t.ID = Guid.NewGuid();
            t.AuthorID = Guid.NewGuid();
            t.RecordStartDateTime = DateTime.Now;
            t.RecordEndDateTime = Beaker.Core.Dates.Infinity;
            t.ValidStartDateTime = DateTime.Now;
            t.ValidEndDateTime = Beaker.Core.Dates.Infinity;

            t.GetType().GetProperty(propertyName).SetValue(t, valueOne);

            T otherT = new T();
            otherT.ID = t.ID;
            otherT.AuthorID = t.AuthorID;
            otherT.RecordEndDateTime = t.RecordEndDateTime;
            otherT.RecordStartDateTime = t.RecordStartDateTime;
            otherT.ValidEndDateTime = t.ValidEndDateTime;
            otherT.ValidStartDateTime = t.ValidStartDateTime;

            otherT.GetType().GetProperty(propertyName).SetValue(otherT, valueTwo);

            Assert.IsFalse(otherT.SameAs(t));
            t.GetType().GetProperty(propertyName).SetValue(t, valueTwo);
            Assert.IsTrue(otherT.SameAs(t));
        }
    }
}
