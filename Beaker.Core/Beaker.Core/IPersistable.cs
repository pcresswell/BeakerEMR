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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beaker.Core
{
    /// <summary>
    /// Anything that would be persisted must implement this interface.
    /// </summary>
    public interface IPersistable
    {
        /// <summary>
        /// Represents the identity of an entity. Storage independent identity. 
        /// Used for tracking identity across systems. 
        /// </summary>
        Guid DomainObjectID { get; set; }

        /// <summary>
        /// Identifier at the table/row level. Globally unique.
        /// </summary>
        Guid ID { get; set; }

        /// <summary>
        /// Represents the author of the current version of the entity. Not necessarily the 
        /// initial author.
        /// </summary>
        Guid AuthorID { get; set; }

        /// <summary>
        /// Represents when the entity came into existance in real time. Does NOT represent
        /// when the entity was recorded. Repesents when the entity became true.
        /// 
        /// This is the date that represents a truth REGARDLESS of when it was recorded.
        /// 
        /// Recorded in UTC.
        /// </summary>
        DateTime ValidStartDateTime { get; set; }

        /// <summary>
        /// Represents when the entity existence was no longer true. Does NOT represent
        /// when the entity was deleted. Represents when the entity is no longer true.
        /// 
        /// This is the date that represents when a truth ceases to be true, REGARDLESS of when it was recorded.
        /// 
        /// Recoreded in UTC.
        /// </summary>
        DateTime ValidEndDateTime { get; set; }

        /// <summary>
        /// Represents when the entity became known to be true according to this system.
        /// 
        /// Recorded in UTC.
        /// </summary>
        DateTime RecordStartDateTime { get; set; }

        /// <summary>
        /// Represents when the entity becamse known to be false according to this system.
        /// 
        /// Recorded in UTC.
        /// </summary>
        DateTime RecordEndDateTime { get; set; }

        /// <summary>
        /// Returns true if this object contains the same values
        /// as the persistable object. Returns false if they are they are different. 
        /// </summary>
        /// <param name="persistable">The persistable object of comparison.</param>
        /// <returns></returns>
        bool SameAs(object persistable);
    }
}
