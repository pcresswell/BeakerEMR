﻿/*
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
    /// Represents a health condition for a patient.
    /// </summary>
    public class HealthCondition : Entity
    {
        /// <summary>
        /// Age of onset for the issue.
        /// </summary>
        public FuzzyAge AgeAtOnset { get; set; }

        /// <summary>
        /// The problem or diagnosis.
        /// </summary>
        public string Issue { get; set; }

        /// <summary>
        /// The lifestage at onset.
        /// </summary>
        public LifeStage LifeStage { get; set; }

        /// <summary>
        /// A note.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// The patient whose family member experienced the issue.
        /// </summary>
        public Patient Patient { get; set; }

        /// <summary>
        /// The approximate date on which the issue was idetified or treated.
        /// </summary>
        public FuzzyDateTime StartDate { get; set; }

        /// <summary>
        /// The approximate date on which the issue was resolved.
        /// </summary>
        public FuzzyDateTime ResolutionDate { get; set; }

        /// <summary>
        /// Identifies the condition status
        /// </summary>
        public string Status { get; set; }
    }

    public static class HealthConditionStatus
    {
        public static readonly string Active = "Active";
        public static readonly string Inactive = "Inactive";
    }
}
