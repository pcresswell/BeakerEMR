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

using System.Runtime.CompilerServices;

namespace Beaker.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Beaker.Core.Attributes;

    /// <summary>
    /// Base class for all domain objects. 
    /// </summary>
    public abstract class DomainObject : IPersistable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.DomainObject"/> class.
        /// </summary>
        /// <param name="domainObjectID">Domain object I.</param>
        public DomainObject(Guid domainObjectID)
        {
            this.DomainObjectID = domainObjectID;
            this.ID = Guid.Empty;
            this.RecordEndDateTime = DateTime.MinValue;
            this.RecordStartDateTime = DateTime.MinValue;
            this.ValidEndDateTime = DateTime.MinValue;
            this.ValidStartDateTime = DateTime.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.DomainObject"/> class.
        /// </summary>
        public DomainObject() : this(Guid.NewGuid()) 
        { 
        }

        /// <summary>
        /// Represents the identity of the domain object. Storage independent identity. 
        /// Used for tracking identity across systems and over time.
        /// </summary>
        public Guid DomainObjectID { get; set; }

        /// <summary>
        /// Row identifier. Unique per record.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Represents the author of the current version of the entity. Not necessarily the 
        /// initial author.
        /// </summary>
        public Guid AuthorID { get; set; }

        /// <summary>
        /// Represents when the entity came into existance in real time. Does NOT represent
        /// when the entity was recorded. Repesents when the entity became true.
        /// This is the date that represents a truth REGARDLESS of when it was recorded.
        /// Recorded in UTC.
        /// </summary>
        public DateTime ValidStartDateTime { get; set; }

        /// <summary>
        /// Represents when the entity existence was no longer true. Does NOT represent
        /// when the entity was deleted. Represents when the entity is no longer true.
        /// This is the date that represents when a truth ceases to be true,
        /// REGARDLESS of when it was recorded. Recoreded in UTC.
        /// </summary>
        public DateTime ValidEndDateTime { get; set; }

        /// <summary>
        /// Represents when the entity became known to be true according to this system.
        /// Recorded in UTC.
        /// </summary>
        public DateTime RecordStartDateTime { get; set; }

        /// <summary>
        /// Represents when the entity becamse known to be false according to this system.
        /// Recorded in UTC.
        /// </summary>
        public DateTime RecordEndDateTime { get; set; }

        /// <summary>
        /// Returns true if this object contains the same values
        /// as the persistable object. Returns false if they are they are different.
        /// Shallow comparison.
        /// </summary>
        /// <param name="persistable">The persistable object of comparison.</param>
        /// <returns>True if objects have the same values. False otherwise.</returns>
        public virtual bool SameAs(object persistable)
        {
            if (persistable == null)
            {
                return false;
            }

            if (!this.GetType().Equals(persistable.GetType()))
            {
                return false;
            }

            DomainObject other = (DomainObject)persistable;

            if (other.ID != this.ID)
            {
                return false;
            }

            var properties = this.GetType().GetRuntimeProperties();
            foreach (var prop in properties)
            {
                // Only compare properties with the SameAs attribute
                var att = prop.GetCustomAttribute<SameAsAttribute>();
                if (att == null)
                {
                    continue;
                }

                var propertyValue = prop.GetValue(this);
                var otherPropertyValue = other.GetType().GetRuntimeProperty(prop.Name).GetValue(other);

                if (propertyValue == null)
                {
                    if (otherPropertyValue != null)
                    {
                        return false;
                    }
                }
                else if (!propertyValue.Equals(otherPropertyValue))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Beaker.Core.DomainObject"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Beaker.Core.DomainObject"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="Beaker.Core.DomainObject"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!obj.GetType().Equals(this.GetType()))
            {
                return false;
            }

            var other = obj as DomainObject;
            if (Guid.Empty.Equals(other.ID))
            {
                return base.Equals(obj);
            }

            if (Guid.Empty.Equals(this.ID))
            {
                return base.Equals(obj);
            }

            return other.ID.Equals(this.ID);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Beaker.Core.DomainObject"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            if (Guid.Empty.Equals(this.ID))
            {
                return base.GetHashCode();
            }

            return this.ID.GetHashCode();
        }
    }
}