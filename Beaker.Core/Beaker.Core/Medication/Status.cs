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

namespace Beaker.Core.Medication
{
    using System;

    /// <summary>
    /// Status.
    /// </summary>
    public class Status : DomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Status"/> class.
        /// </summary>
        public Status()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Status"/> class.
        /// </summary>
        /// <param name="drugCode">Drug code.</param>
        /// <param name="currentStatusFlag">Current status flag.</param>
        /// <param name="status">Status.</param>
        /// <param name="historyDate">History date.</param>
        public Status(int drugCode, string currentStatusFlag, string status, DateTime historyDate)
        {
            this.DrugCode = drugCode;
            this.CurrentStatusFlag = currentStatusFlag;
            this.StatusCode = status;
            this.HistoryDate = historyDate;
        }

        /// <summary>
        /// Gets or sets the drug code.
        /// </summary>
        /// <value>The drug code.</value>
        public int DrugCode { get; set; }

        /// <summary>
        /// Gets or sets the current status flag.
        /// </summary>
        /// <value>The current status flag.</value>
        public string CurrentStatusFlag { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public string StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the history date.
        /// </summary>
        /// <value>The history date.</value>
        public DateTime HistoryDate { get; set; }
    }
}