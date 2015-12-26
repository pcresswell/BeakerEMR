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

namespace Beaker.Core.Medication
{
	public class Company : DomainObject
	{
		public Company (int drugCode,
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

		public int DrugCode { get; private set; }

		public string MFRCode { get; private set; }

		public string CompanyCode { get; private set; }

		public string CompanyName { get; private set; }

		public string CompanyType { get; private set; }

		public string AddressMailingFlag { get; private set; }

		public string AddressBillingFlag { get; private set; }

		public string AddressNotificationFlag	 { get; private set; }

		public string AddressOther { get; private set; }

		public string SuiteNumber { get; private set; }

		public string Street { get; private set; }

		public string City { get; private set; }

		public string Province { get; private set; }

		public string Country { get; private set; }

		public string PostalCode { get; private set; }

		public string POBox { get; private set; }
	}
}

