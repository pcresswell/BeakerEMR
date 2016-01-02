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
    /// A DateTime in which the accuracy of the value is not exactly known.
    /// </summary>
    public class FuzzyDateTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.FuzzyDateTime"/> class.
        /// </summary>
        /// <param name="year">Year.</param>
        /// <param name="month">Month.</param>
        /// <param name="day">Day.</param>
        /// <param name="accuracy">Accuracy.</param>
        public FuzzyDateTime(int year, int month, int day, FuzzyDateTimeAccuracy accuracy) : this(year, month, day, 0, 0, 0, accuracy)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.FuzzyDateTime"/> class.
        /// </summary>
        /// <param name="year">Year.</param>
        /// <param name="month">Month.</param>
        /// <param name="day">Day.</param>
        /// <param name="hour">Hour.</param>
        /// <param name="minute">Minute.</param>
        /// <param name="second">Second.</param>
        /// <param name="accuracy">Accuracy.</param>
        public FuzzyDateTime(int year, int month, int day, int hour, int minute, int second, FuzzyDateTimeAccuracy accuracy)
        {
            this.DateTime = new DateTime(year, month, day, hour, minute, second);
            this.Accuracy = accuracy;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.FuzzyDateTime"/> class.
        /// </summary>
        /// <param name="year">Year.</param>
        /// <param name="month">Month.</param>
        /// <param name="day">Day.</param>
        public FuzzyDateTime(int year, int month, int day) : this(year, month, day, FuzzyDateTimeAccuracy.Exact)
        { 
        }

        /// <summary>
        /// The DateTime
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// The accuracy of the DateTime value.
        /// </summary>
        public FuzzyDateTimeAccuracy Accuracy { get; private set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Beaker.Core.FuzzyDateTime"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Beaker.Core.FuzzyDateTime"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="Beaker.Core.FuzzyDateTime"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            FuzzyDateTime otherFuzzyDateTime = obj as FuzzyDateTime;

            if (otherFuzzyDateTime == null)
            {
                return false;
            }
            else
            {
                return this.DateTime.Equals(otherFuzzyDateTime.DateTime) && this.Accuracy.Equals(otherFuzzyDateTime.Accuracy);
            }
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Beaker.Core.FuzzyDateTime"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return this.DateTime.GetHashCode();
        }
    }
}