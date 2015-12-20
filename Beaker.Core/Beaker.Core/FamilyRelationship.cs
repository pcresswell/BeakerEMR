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
    /// Represents the relationship between two family members.
    /// </summary>
    public class FamilyRelationship : Entity
    {
        /// <summary>
        /// Contruct a new Family Relationship.
        /// </summary>
        /// <param name="type">The type of relationship.</param>
        public FamilyRelationship(string type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Person one. 
        /// </summary>
        public Person One { get; set; }
        /// <summary>
        /// Person two.
        /// </summary>
        public Person Two { get; set; }
        /// <summary>
        /// The type of relationship. Follows a convension of "One-Two" such as "Child-Parent" where 
        /// person one is the child and person two is the parent.
        /// </summary>
        public string Type { get; private set; }
    }

    public static class FamilyRelationshipType
    {
        public static readonly string ChildFather = "Child-Father";
        public static readonly string ChildMother = "Child-Mother";
        public static readonly string Sibling = "Sibling-Sibling";
        public static readonly string NeiceUncle = "Neice-Uncle";
        public static readonly string NeiceAunt = "Neice-Aunt";
        public static readonly string NephewUncle = "Nephew-Uncle";
        public static readonly string NephewAunt = "Nephew-Aunt";
        public static readonly string ChildGrandParent = "Child-GrandParent";
        public static readonly string Spouse = "Spouse-Spouse";
    }
}
