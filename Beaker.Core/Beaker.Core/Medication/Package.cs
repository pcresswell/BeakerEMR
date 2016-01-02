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

    /// <summary>
    /// Package for the medication.
    /// </summary>
    public class Package : DomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Package"/> class.
        /// </summary>
        public Package()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Package"/> class.
        /// </summary>
        /// <param name="drugCode">Drug code.</param>
        /// <param name="upc">Upc.</param>
        /// <param name="packageSizeUnit">Package size unit.</param>
        /// <param name="packageType">Package type.</param>
        /// <param name="packageSize">Package size.</param>
        /// <param name="productInformation">Product information.</param>
        public Package(
            int drugCode, 
            string upc,
            string packageSizeUnit,
            string packageType,
            string packageSize, 
            string productInformation)
        {
            this.DrugCode = drugCode;
            this.UPC = upc;
            this.PackageSizeUnit = packageSizeUnit;
            this.PackageType = packageType;
            this.PackageSize = packageSize;
            this.ProductInformation = productInformation;
        }

        /// <summary>
        /// Gets or sets the drug code.
        /// </summary>
        /// <value>The drug code.</value>
        public int DrugCode { get; set; }

        /// <summary>
        /// Gets or sets UPC
        /// </summary>
        /// <value>UP.</value>
        public string UPC { get; set; }

        /// <summary>
        /// Gets or sets the package size unit.
        /// </summary>
        /// <value>The package size unit.</value>
        public string PackageSizeUnit { get; set; }

        /// <summary>
        /// Gets or sets the type of the package.
        /// </summary>
        /// <value>The type of the package.</value>
        public string PackageType { get; set; }

        /// <summary>
        /// Gets or sets the size of the package.
        /// </summary>
        /// <value>The size of the package.</value>
        public string PackageSize { get; set; }

        /// <summary>
        /// Gets or sets the product information.
        /// </summary>
        /// <value>The product information.</value>
        public string ProductInformation { get; set; }
    }
}