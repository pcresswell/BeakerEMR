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
    /// A DateTime in which the accuracy of the value is not exactly known.
    /// </summary>
    public class FuzzyDateTime
    {
        /// <summary>
        /// The DateTime
        /// </summary>
        public DateTime DateTime { get; private set; }
        /// <summary>
        /// The accuracy of the DateTime value.
        /// </summary>
        public FuzzyDateTimeAccuracy Accuracy { get; private set; }

        /// <summary>
        /// Creates 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="accuracy"></param>
        public FuzzyDateTime(int year, int month, int day, FuzzyDateTimeAccuracy accuracy)
        {
            this.DateTime = new DateTime(year, month, day);
            this.Accuracy = accuracy;
        }

        /// <summary>
        /// Equality is based on the accuracy and the date.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {           
            FuzzyDateTime other = (FuzzyDateTime)obj;

            if (obj == null)
            {
                return false;
            }

            return (this.DateTime.Equals(other.DateTime) && this.Accuracy.Equals(other.Accuracy));
        }

        public override int GetHashCode()
        {
            return this.DateTime.GetHashCode();
        }
    }

    /// <summary>
    /// Accuracy values.
    /// </summary>
    public enum FuzzyDateTimeAccuracy
    {
        /// <summary>
        /// The date is known to be exact.
        /// </summary>
        Exact = 0,
        /// <summary>
        /// Plus or minus a minute.
        /// </summary>
        Minute = 1,
        /// <summary>
        /// Plus or minus an hour.
        /// </summary>
        Hour = 2,
        /// <summary>
        /// Plus or minus a day.
        /// </summary>
        Day = 3,
        /// <summary>
        /// Plus or minus a week.
        /// </summary>
        Week  = 4,
        /// <summary>
        /// Plus or minus a month.
        /// </summary>
        Month = 5,
        /// <summary>
        /// Plus or minus three months.
        /// </summary>
        ThreeMonths = 6,
        /// <summary>
        /// Plus or minus half a year.
        /// </summary>
        SixMonths = 7,
        /// <summary>
        /// Plus or minus a year.
        /// </summary>
        Year = 8,
        /// <summary>
        /// Plus or minus a couple of years.
        /// </summary>
        TwoYears = 9,
        /// <summary>
        /// Plus or minus five years.
        /// </summary>
        FiveYears = 10,
        /// <summary>
        /// Plus or minus a decade.
        /// </summary>
        Decade = 11,
        /// <summary>
        /// The exactness of the date cannot be estimated.
        /// </summary>
        Unknown = 12
    }
}
