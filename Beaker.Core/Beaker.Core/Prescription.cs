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

namespace Beaker.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Beaker.Core.Attributes;

    /// <summary>
    /// A patient medication prescription
    /// </summary>
    public class Prescription
    {
        /// <summary>
        /// Dispensing interval.
        /// Indicates a minimum amount of time that must occur between dispenses. 
        /// </summary>
        [SameAs]
        public string DispenseInterval { get; set; }

        /// <summary>
        /// Address of the dispensing facility.
        /// </summary>
        [SameAs]
        public string DispensingFacilityAddress { get; set; }

        /// <summary>
        /// Identifier for the dispensing facility
        /// </summary>
        [SameAs]
        public string DispensingFacilityID { get; set; }
        
        /// <summary>
        /// Name of the dispensing facility.
        /// </summary>
        [SameAs]
        public string DispensingFacilityName { get; set; }

        /// <summary>
        /// Dose amount and unit of measure of the medication
        /// intended to be consumed during a single administration
        /// as prescribed by the provider. 
        /// </summary>
        [SameAs]
        public string Dosage { get; set; }

        /// <summary>
        /// Drug Code.
        /// </summary>
        [SameAs]
        public string DrugCode { get; set; }

        /// <summary>
        /// Drug description.
        /// </summary>
        [SameAs]
        public string DrugDescription { get; set; }

        /// <summary>
        /// Drug Form.
        /// </summary>
        [SameAs]
        public string DrugForm { get; set; }

        /// <summary>
        /// Drug Strength.
        /// </summary>
        [SameAs]
        public string DrugStrength { get; set; }

        /// <summary>
        /// Duration of prescription.
        /// </summary>
        [SameAs]
        public string Duration { get; set; }

        /// <summary>
        /// Earliest Pickup Date.
        /// </summary>
        [SameAs]
        public DateTime EarliestPickupDate { get; set; }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>The frequency.</value>
        [SameAs]
        public string Frequency { get; set; }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>The instructions.</value>
        [SameAs]
        public string Instructions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Beaker.Core.Prescription"/> long term.
        /// </summary>
        /// <value><c>true</c> if long term; otherwise, <c>false</c>.</value>
        [SameAs]
        public bool LongTerm { get; set; }

        /// <summary>
        /// Gets or sets the name of the medication.
        /// </summary>
        /// <value>The name of the medication.</value>
        [SameAs]
        public string MedicationName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Beaker.Core.Prescription"/> non authoritative.
        /// </summary>
        /// <value><c>true</c> if non authoritative; otherwise, <c>false</c>.</value>
        [SameAs]
        public bool NonAuthoritative { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [SameAs]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the number of refills.
        /// </summary>
        /// <value>The number of refills.</value>
        [SameAs]
        public int NumberOfRefills { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Beaker.Core.Prescription"/> past medication.
        /// </summary>
        /// <value><c>true</c> if past medication; otherwise, <c>false</c>.</value>
        [SameAs]
        public bool PastMedication { get; set; }

        /// <summary>
        /// Gets or sets the patient.
        /// </summary>
        /// <value>The patient.</value>
        [SameAs]
        public Patient Patient { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Beaker.Core.Prescription"/> patient compliance.
        /// </summary>
        /// <value><c>true</c> if patient compliant; otherwise, <c>false</c>.</value>
        [SameAs]
        public bool PatientCompliance { get; set; }

        /// <summary>
        /// Gets or sets the prescriber.
        /// </summary>
        /// <value>The prescriber.</value>
        [SameAs]
        public Provider Prescriber { get; set; }

        /// <summary>
        /// Gets or sets the prescriber billing number.
        /// </summary>
        /// <value>The prescriber billing number.</value>
        [SameAs]
        public string PrescriberBillingNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the prescriber.
        /// </summary>
        /// <value>The name of the prescriber.</value>
        [SameAs]
        public string PrescriberName { get; set; }

        /// <summary>
        /// Gets or sets the prior prescription.
        /// </summary>
        /// <value>The prior prescription.</value>
        [SameAs]
        public object PriorPrescription { get; set; }

        /// <summary>
        /// Gets or sets the problem code.
        /// </summary>
        /// <value>The problem code.</value>
        [SameAs]
        public string ProblemCode { get; set; }

        /// <summary>
        /// Gets or sets the protocol identifier.
        /// </summary>
        /// <value>The protocol identifier.</value>
        [SameAs]
        public string ProtocolIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>The quantity.</value>
        [SameAs]
        public string Quantity { get; set; }

        /// <summary>
        /// Gets or sets the duration of the refill.
        /// </summary>
        /// <value>The duration of the refill.</value>
        [SameAs]
        public string RefillDuration { get; set; }

        /// <summary>
        /// Gets or sets the refill quantity.
        /// </summary>
        /// <value>The refill quantity.</value>
        [SameAs]
        public string RefillQuantity { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        [SameAs]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        [SameAs]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        /// <value>The strength.</value>
        [SameAs]
        public string Strength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Beaker.Core.Prescription"/> substitutions not allowed.
        /// </summary>
        /// <value><c>true</c> if substitutions not allowed; otherwise, <c>false</c>.</value>
        [SameAs]
        public bool SubstitutionsNotAllowed { get; set; }

        /// <summary>
        /// Gets or sets the type of the treatment.
        /// </summary>
        /// <value>The type of the treatment.</value>
        [SameAs]
        public string TreatmentType { get; set; }

        /// <summary>
        /// Gets or sets the written date.
        /// </summary>
        /// <value>The written date.</value>
        [SameAs]
        public DateTime WrittenDate { get; set; }
    }
}