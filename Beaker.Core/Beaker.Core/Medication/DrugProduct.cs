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
    using System.Reflection;

    /// <summary>
    /// Drug product.
    /// </summary>
    public class DrugProduct : DomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.DrugProduct"/> class.
        /// </summary>
        /// <param name="drugCode">Drug code.</param>
        /// <param name="productCategorization">Product categorization.</param>
        /// <param name="classValue">Class value.</param>
        /// <param name="drugIdentificationNumber">Drug identification number.</param>
        /// <param name="brandName">Brand name.</param>
        /// <param name="descriptor">Descriptor.</param>
        /// <param name="pediatricFlag">Pediatric flag.</param>
        /// <param name="accessionNumber">Accession number.</param>
        /// <param name="numberOfAIS">Number of AI.</param>
        /// <param name="lastUpdateDate">Last update date.</param>
        /// <param name="aiGroupNo">Ai group no.</param>
        public DrugProduct(
            int drugCode,
            string productCategorization,
            string classValue,
            string drugIdentificationNumber,
            string brandName,
            string descriptor,
            string pediatricFlag,
            string accessionNumber,
            string numberOfAIS,
            DateTime lastUpdateDate,
            string aiGroupNo)
        {
            this.DrugCode = drugCode;
            this.ProductCategorization = productCategorization;
            this.ProductClass = classValue;
            this.DrugIdentificationNumber = drugIdentificationNumber;
            this.BrandName = brandName;
            this.Descriptor = descriptor;
            this.PediatricFlag = pediatricFlag;
            this.AccessionNumber = accessionNumber;
            this.NumberOfAIS = numberOfAIS;
            this.LastUpdateDate = lastUpdateDate;
            this.AIGroupNumber = aiGroupNo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.DrugProduct"/> class.
        /// </summary>
        public DrugProduct()
        {
        }

        /// <summary>
        /// Gets or sets the drug code.
        /// </summary>
        /// <value>The drug code.</value>
        public int DrugCode { get; set; }

        /// <summary>
        /// Gets or sets the product categorization.
        /// </summary>
        /// <value>The product categorization.</value>
        public string ProductCategorization { get; set; }

        /// <summary>
        /// Gets or sets the product class.
        /// </summary>
        /// <value>The product class.</value>
        public string ProductClass { get; set; }

        /// <summary>
        /// Gets or sets the drug identification number.
        /// </summary>
        /// <value>The drug identification number.</value>
        public string DrugIdentificationNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the brand.
        /// </summary>
        /// <value>The name of the brand.</value>
        public string BrandName { get; set; }

        /// <summary>
        /// Gets or sets the descriptor.
        /// </summary>
        /// <value>The descriptor.</value>
        public string Descriptor { get; set; }

        /// <summary>
        /// Gets or sets the pediatric flag.
        /// </summary>
        /// <value>The pediatric flag.</value>
        public string PediatricFlag { get; set; }

        /// <summary>
        /// Gets or sets the accession number.
        /// </summary>
        /// <value>The accession number.</value>
        public string AccessionNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of AI.
        /// </summary>
        /// <value>The number of AI.</value>
        public string NumberOfAIS { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        /// <value>The last update date.</value>
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the AI group number.
        /// </summary>
        /// <value>The AI group number.</value>
        public string AIGroupNumber { get; set; }
    }
}