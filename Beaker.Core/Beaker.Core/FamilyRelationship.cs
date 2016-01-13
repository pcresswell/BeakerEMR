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

namespace Beaker.Core
{
    using System;
    using Beaker.Core.Attributes;

    /// <summary>
    /// Represents the relationship between two family members.
    /// </summary>
    public class FamilyRelationship : DomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.FamilyRelationship"/> class.
        /// </summary>
        /// <param name="type">Type of relationship.</param>
        public FamilyRelationship(string type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Person one. 
        /// </summary>
        [SameAs]
        public Person One { get; set; }

        /// <summary>
        /// Person two.
        /// </summary>
        [SameAs]
        public Person Two { get; set; }

        /// <summary>
        /// The type of relationship. Follows a convension of "One-Two" such as "Child-Parent" where 
        /// person one is the child and person two is the parent.
        /// </summary>
        [SameAs]
        public string Type { get; private set; }
    }
}