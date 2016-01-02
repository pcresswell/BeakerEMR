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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Gender class. Represents a person's gender.
    /// </summary>
    public class Gender
    {
        /// <summary>
        /// The Male gender
        /// </summary>
        public static readonly Gender Male = new Gender("Male");

        /// <summary>
        /// The Female gender
        /// </summary>
        public static readonly Gender Female = new Gender("Female");

        /// <summary>
        /// Unknown gender.
        /// </summary>
        public static readonly Gender Unknown = new Gender("Unknown");

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Gender"/> class.
        /// </summary>
        /// <param name="name">Name of the gender.</param>
        public Gender(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// The name of the gender. For example "Male" or "Female".
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Beaker.Core.Gender"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Beaker.Core.Gender"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="Beaker.Core.Gender"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            Gender otherGender = obj as Gender;
            if (otherGender == null)
            {
                return false;
            }

            return this.Name.Equals(otherGender.Name);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Beaker.Core.Gender"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}