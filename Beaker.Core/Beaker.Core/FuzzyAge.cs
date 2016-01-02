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
    /// A fuzzy age.
    /// </summary>
    public class FuzzyAge
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.FuzzyAge"/> class.
        /// </summary>
        /// <param name="age">Age.</param>
        /// <param name="accuracy">Accuracy.</param>
        public FuzzyAge(int age, FuzzyAgeAccuracy accuracy)
        {
            this.Age = age;
            this.Accuracy = accuracy;
        }

        /// <summary>
        /// The age.
        /// </summary>
        public int Age { get; private set; }

        /// <summary>
        /// The accuracy of the age recording.
        /// </summary>
        public FuzzyAgeAccuracy Accuracy { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Beaker.Core.FuzzyAge"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Beaker.Core.FuzzyAge"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="Beaker.Core.FuzzyAge"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            FuzzyAge otherAge = (FuzzyAge)obj;
            if (otherAge == null)
            {
                return false;
            }

            return otherAge.Age.Equals(this.Age) && otherAge.Accuracy.Equals(this.Accuracy);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Beaker.Core.FuzzyAge"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return this.Age.GetHashCode() + (1000 * this.Accuracy.GetHashCode());
        }
    }
}