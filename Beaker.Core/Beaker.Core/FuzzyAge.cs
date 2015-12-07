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
    /// A fuzzy age.
    /// </summary>
    public class FuzzyAge
    {
        /// <summary>
        /// The age.
        /// </summary>
        public int Age { get; private set; }
        /// <summary>
        /// The accuracy of the age recording.
        /// </summary>
        public FuzzyAgeAccuracy Accuracy { get; private set; }
        /// <summary>
        /// Creates a new FuzzyAge.
        /// </summary>
        /// <param name="age"></param>
        /// <param name="accuracy"></param>
        public FuzzyAge(int age, FuzzyAgeAccuracy accuracy)
        {
            this.Age = age;
            this.Accuracy = accuracy;
        }

        public override bool Equals(object obj)
        {
            FuzzyAge otherAge = (FuzzyAge)obj;
            if (otherAge == null)
            {
                return false;
            }

            return (otherAge.Age.Equals(this.Age) && otherAge.Accuracy.Equals(this.Accuracy));
        }

        public override int GetHashCode()
        {
            return (this.Age.GetHashCode() + (1000 * this.Accuracy.GetHashCode()));
        }
    }

    /// <summary>
    /// The accuracy of the age.
    /// </summary>
    public enum FuzzyAgeAccuracy
    {
        /// <summary>
        /// Exact.
        /// </summary>
        Exact = 0,
        /// <summary>
        /// Roughly accurate to +/- one year.
        /// </summary>
        OneYear = 1,
        /// <summary>
        /// Roughly accurate to +/- five years.
        /// </summary>
        FiveYears = 2,
        /// <summary>
        /// Roughly accurate to +/- a decade
        /// </summary>
        Decade = 3,
        /// <summary>
        /// Unknown accuracy.
        /// </summary>
        Unknown = 4
    }
}
