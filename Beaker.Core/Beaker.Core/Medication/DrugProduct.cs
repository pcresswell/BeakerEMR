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
using System.Reflection;

namespace Beaker.Core.Medication
{
	public class DrugProduct : DomainObject
	{
		public DrugProduct (int drugCode,
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

        public DrugProduct() { }

		public int DrugCode { get;  set; }

		public string ProductCategorization { get;  set; }

		public string ProductClass { get;  set; }

		public string DrugIdentificationNumber { get;  set; }

		public string BrandName { get;  set; }

		public string Descriptor { get;  set; }

		public string PediatricFlag { get;  set; }

		public string AccessionNumber { get;  set; }

		public string NumberOfAIS { get;  set; }

		public DateTime LastUpdateDate { get;  set; }

		public string AIGroupNumber { get;  set; }
	}
}

