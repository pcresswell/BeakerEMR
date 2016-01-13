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
    /// Represents a physical person. Does not contain any role information such as patient, provider, etc.
    /// </summary>
    public class Person : DomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Person"/> class.
        /// </summary>
        public Person()
        {
            this.Gender = Gender.Unknown;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.MiddleName = string.Empty;
            this.Prefix = string.Empty;
            this.Suffix = string.Empty;
            this.DateOfBirth = new DateTime(1899, 1, 1);
        }

        /// <summary>
        /// The person's first name.
        /// </summary>
        [SameAs]
        public string FirstName { get; set; }

        /// <summary>
        /// The person's last name.
        /// </summary>
        [SameAs]
        public string LastName { get; set; }

        /// <summary>
        /// The person's middle name.
        /// </summary>
        [SameAs]
        public string MiddleName { get; set; }

        /// <summary>
        /// The person's prefix. Example, "Mr." or "Ms.".
        /// </summary>
        [SameAs]
        public string Prefix { get; set; }

        /// <summary>
        /// The person's suffix. Example, "Jr.", "Sr.".
        /// </summary>
        [SameAs]
        public string Suffix { get; set; }

        /// <summary>
        /// The persons gender as identified at birth.
        /// </summary>
        [SameAs]
        public Gender Gender { get; set; }

        /// <summary>
        /// The person's date of birth.
        /// </summary>
        [SameAs]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Preferred official language. Official languages are specified based on the country of origin.
        /// For example, "English" or "French" for Canada are offical languages.
        /// </summary>
        [SameAs]
        public ISOLanguage PreferredOfficialLanguage { get; set; }

        /// <summary>
        /// Preferred language for conversation. Does not depend on the country of origin. 
        /// </summary>
        [SameAs]
        public ISOLanguage PreferredSpokenLanguage { get; set; }
    }
}