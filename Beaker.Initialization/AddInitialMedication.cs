// 
// The MIT License (MIT)
//
// Copyright (c) 2015 Peter Cresswell (pcresswell@gmail.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace Beaker.Initialization
{
    using System;
    using Beaker.Core;
    using Beaker.Repository;
    using Beaker.Update.Medication.June2015;

    /// <summary>
    /// Add initial medication.
    /// </summary>
    internal class AddInitialMedication
    {   

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Initialization.AddInitialMedication"/> class.
        /// </summary>
        /// <param name="migratable">Migratable.</param>
        public AddInitialMedication(IMigratable migratable)
        {
            this.Migratable = migratable;
        }

        /// <summary>
        /// Gets or sets the migratable.
        /// </summary>
        /// <value>The migratable.</value>
        private IMigratable Migratable { get; set;}

        /// <summary>
        /// Run this instance.
        /// </summary>
        public void Run()
        {
            Medication20150601Migration migration = new Medication20150601Migration();
            this.Migratable.Apply(migration);
        }
    }
}