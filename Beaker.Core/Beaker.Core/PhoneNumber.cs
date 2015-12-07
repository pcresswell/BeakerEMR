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
    public static class PhoneNumberType
    {
        public static readonly string Home = "Home";
        public static readonly string Work = "Work";
        public static readonly string Other = "Other";
        public static readonly string Mobile = "Mobile";
    }

    /// <summary>
    /// A phone number.
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>
        /// The area code.
        /// </summary>
        public string AreaCode { get; set; }
        /// <summary>
        /// The extension.
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// The phone number.
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// The owner of the phone number.
        /// </summary>
        public Person Owner { get; set; }
        /// <summary>
        /// The type of phone number. For example, "Home" or "Work".
        /// </summary>
        public string Type { get; set; }
    }
}
