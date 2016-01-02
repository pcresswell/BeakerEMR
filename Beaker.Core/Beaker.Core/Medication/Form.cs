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
    /// Form of medication.
    /// </summary>
    public class Form : DomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Form"/> class.
        /// </summary>
        public Form()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Form"/> class.
        /// </summary>
        /// <param name="drugCode">Drug code.</param>
        /// <param name="pharmFormCode">Pharm form code.</param>
        /// <param name="pharmaceuticalForm">Pharmaceutical form.</param>
        public Form(int drugCode, int pharmFormCode, string pharmaceuticalForm)
        {
            this.DrugCode = drugCode;
            this.PharmaceuticalFormCode = pharmFormCode;
            this.PharmaceuticalForm = pharmaceuticalForm;
        }

        /// <summary>
        /// Gets or sets the drug code.
        /// </summary>
        /// <value>The drug code.</value>
        public int DrugCode { get; set; }

        /// <summary>
        /// Gets or sets the pharmaceutical form code.
        /// </summary>
        /// <value>The pharmaceutical form code.</value>
        public int PharmaceuticalFormCode { get; set; }

        /// <summary>
        /// Gets or sets the pharmaceutical form.
        /// </summary>
        /// <value>The pharmaceutical form.</value>
        public string PharmaceuticalForm { get; set; }
    }
}