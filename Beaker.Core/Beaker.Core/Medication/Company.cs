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
    /// Medication company.
    /// </summary>
    public class Company : DomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Company"/> class.
        /// </summary>
        public Company()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Company"/> class.
        /// </summary>
        /// <param name="drugCode">Drug code.</param>
        /// <param name="mfrCode">Mfr code.</param>
        /// <param name="companyCode">Company code.</param>
        /// <param name="companyName">Company name.</param>
        /// <param name="companyType">Company type.</param>
        /// <param name="addressMailingFlag">Address mailing flag.</param>
        /// <param name="addressBillingFlag">Address billing flag.</param>
        /// <param name="addressNotificationFlag">Address notification flag.</param>
        /// <param name="addressOther">Address other.</param>
        /// <param name="suiteNumber">Suite number.</param>
        /// <param name="streetName">Street name.</param>
        /// <param name="cityName">City name.</param>
        /// <param name="province">Province.</param>
        /// <param name="country">Country.</param>
        /// <param name="postalCode">Postal code.</param>
        /// <param name="postOfficeBox">Post office box.</param>
        public Company(
            int drugCode,
            string mfrCode,
            string companyCode,
            string companyName,
            string companyType,
            string addressMailingFlag,
            string addressBillingFlag,
            string addressNotificationFlag,
            string addressOther,
            string suiteNumber,
            string streetName,
            string cityName,
            string province,
            string country,
            string postalCode,
            string postOfficeBox)
        {
            this.DrugCode = drugCode;
            this.MFRCode = mfrCode;
            this.CompanyCode = companyCode;
            this.CompanyName = companyName;
            this.CompanyType = companyType;
            this.AddressMailingFlag = addressMailingFlag;
            this.AddressBillingFlag = addressBillingFlag;
            this.AddressNotificationFlag = addressNotificationFlag;
            this.AddressOther = addressOther;
            this.SuiteNumber = suiteNumber;
            this.Street = streetName;
            this.City = cityName;
            this.Province = province;
            this.Country = country;
            this.PostalCode = postalCode;
            this.POBox = postOfficeBox;
        }

        /// <summary>
        /// Gets or sets the drug code.
        /// </summary>
        /// <value>The drug code.</value>
        public int DrugCode { get; set; }

        /// <summary>
        /// Gets or sets the MFR code.
        /// </summary>
        /// <value>The MFR code.</value>
        public string MFRCode { get; set; }

        /// <summary>
        /// Gets or sets the company code.
        /// </summary>
        /// <value>The company code.</value>
        public string CompanyCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the type of the company.
        /// </summary>
        /// <value>The type of the company.</value>
        public string CompanyType { get; set; }

        /// <summary>
        /// Gets or sets the address mailing flag.
        /// </summary>
        /// <value>The address mailing flag.</value>
        public string AddressMailingFlag { get; set; }

        /// <summary>
        /// Gets or sets the address billing flag.
        /// </summary>
        /// <value>The address billing flag.</value>
        public string AddressBillingFlag { get; set; }

        /// <summary>
        /// Gets or sets the address notification flag.
        /// </summary>
        /// <value>The address notification flag.</value>
        public string AddressNotificationFlag { get; set; }

        /// <summary>
        /// Gets or sets the address other.
        /// </summary>
        /// <value>The address other.</value>
        public string AddressOther { get; set; }

        /// <summary>
        /// Gets or sets the suite number.
        /// </summary>
        /// <value>The suite number.</value>
        public string SuiteNumber { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>The street.</value>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>The province.</value>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the PO box.
        /// </summary>
        /// <value>The PO box.</value>
        public string POBox { get; set; }
    }
}