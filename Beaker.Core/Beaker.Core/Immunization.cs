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
    using Beaker.Core.Attributes;

    /// <summary>
    /// Immunization.
    /// </summary>
    public class Immunization
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [SameAs]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        [SameAs]
        public FuzzyDateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the dose.
        /// </summary>
        /// <value>The dose.</value>
        [SameAs]
        public string Dose { get; set; }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>The instructions.</value>
        [SameAs]
        public string Instructions { get; set; }

        /// <summary>
        /// Gets or sets the lot number.
        /// </summary>
        /// <value>The lot number.</value>
        [SameAs]
        public string LotNumber { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer.
        /// </summary>
        /// <value>The manufacturer.</value>
        [SameAs]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [SameAs]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [SameAs]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>The patient.</value>
        [SameAs]
        public Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets the refusal date.
        /// </summary>
        /// <value>The refusal date.</value>
        [SameAs]
        public FuzzyDateTime RefusalDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Beaker.Core.Immunization"/> is refused.
        /// </summary>
        /// <value><c>true</c> if refused; otherwise, <c>false</c>.</value>
        [SameAs]
        public bool Refused { get; set; }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route.</value>
        [SameAs]
        public string Route { get; set; }

        /// <summary>
        /// Gets or sets the site.
        /// </summary>
        /// <value>The site.</value>
        [SameAs]
        public string Site { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [SameAs]
        public string Type { get; set; }
    }
}