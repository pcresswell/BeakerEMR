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
    /// A patient medication prescription
    /// </summary>
    public class Prescription
    {
        /// <summary>
        /// Dispensing interval.
        /// 
        /// Indicates a minimum amount of time that must occur between dispenses. 
        /// </summary>
        public string DispenseInterval { get; set; }

        /// <summary>
        /// Address of the dispensing facility.
        /// </summary>
        public string DispensingFacilityAddress { get; set; }

        /// <summary>
        /// Identifier for the dispensing facility
        /// </summary>
        public string DispensingFacilityID { get; set; }
        
        /// <summary>
        /// Name of the dispensing facility.
        /// </summary>
        public string DispensingFacilityName { get; set; }

        /// <summary>
        /// Dose amount and unit of measure of the medication
        /// intended to be consumed during a single administration
        /// as prescribed by the provider. 
        /// </summary>
        public string Dosage { get; set; }

        /// <summary>
        /// Drug Code.
        /// </summary>
        public string DrugCode { get; set; }
        /// <summary>
        /// Drug description.
        /// </summary>
        public string DrugDescription { get; set; }
        /// <summary>
        /// Drug Form.
        /// </summary>
        public string DrugForm { get; set; }
        /// <summary>
        /// Drug Strength.
        /// </summary>
        public string DrugStrength { get; set; }
        /// <summary>
        /// Duration of prescription.
        /// </summary>
        public string Duration { get; set; }
        /// <summary>
        /// Earliest Pickup Date.
        /// </summary>
        public DateTime EarliestPickupDate { get; set; }
        public string Frequency { get; set; }
        public string Instructions { get; set; }
        public bool LongTerm { get; set; }
        public string MedicationName { get; set; }
        public bool NonAuthoritative { get; set; }
        public string Notes { get; set; }
        public int NumberOfRefills { get; set; }
        public bool PastMedication { get; set; }
        public Patient Patient { get; set; }
        public bool PatientCompliance { get; set; }
        public Provider Prescriber { get; set; }
        public string PrescriberBillingNumber { get; set; }
        public string PrescriberName { get; set; }
        public object PriorPrescription { get; set; }
        public string ProblemCode { get; set; }
        public string ProtocolIdentifier { get; set; }
        public string Quantity { get; set; }
        public string RefillDuration { get; set; }
        public string RefillQuantity { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
        public string Strength { get; set; }
        public bool SubstitutionsNotAllowed { get; set; }
        public string TreatmentType { get; set; }
        public DateTime WrittenDate { get; set; }
    }
}
