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
//

namespace Beaker.Services.Commands
{
    using System;
    using Beaker.Core;

    /// <summary>
    /// Merge a source patient information into a destination patient.
    /// Deletes the source patient after merging.
    /// </summary>
    public class MergePatientsCommand : Command
    {
        public MergePatientsCommand()
        {
        }

        /// <summary>
        /// Gets or sets the source patient. The source patient
        /// is ultimately deleted after the merge
        /// </summary>
        /// <value>The source.</value>
        public Patient Source { get; set; }

        /// <summary>
        /// Gets or sets the destination patient. The destination patient
        /// is updated with data from the source patient and lives on.
        /// </summary>
        /// <value>The destination.</value>
        public Patient Destination { get; set; }

        #region implemented abstract members of Command

        protected override void Run()
        {
            // Probably want something very generic here.
            // Ask repositories if they are patient specific and 
            // then pull their records by patient
            // and merge. Something like:
            // 1. Iterate over each repository
            // 2. Ask repository if it contains patient specific data.
            // 3. If it does, ask for the records by the source patient
            // 4. Merge the source data into the destination patient.
            // 5. Delete the source patient.

        }

        #endregion
    }
}

