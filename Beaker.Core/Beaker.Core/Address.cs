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
    /// An address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// The city.
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// The country.
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Postal code or zip code.
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// Province or state.
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// The person associated with the address.
        /// </summary>
        public Person Resident { get; set; }
        /// <summary>
        /// The street.
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// The type of address. See AddressType.
        /// </summary>
        public string Type { get; set; }
    }

    /// <summary>
    /// Types of addresses.
    /// </summary>
    public static class AddressType
    {
        /// <summary>
        /// Home address.
        /// </summary>
        public static readonly string Home = "Home";
        /// <summary>
        /// Work address.
        /// </summary>
        public static readonly string Work = "Work";
        /// <summary>
        /// Other address.
        /// </summary>
        public static readonly string Other = "Other";
    }
}
