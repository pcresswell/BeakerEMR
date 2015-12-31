using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beaker.Core.Test
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
