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
	public class Ingredient : DomainObject
	{
        public Ingredient() { }

		public Ingredient (int drugCode, 
			int activeIngredientCode,
			string ingredient,
			string ingredientSuppliedInd,
			double strength,
			string strengthUnit,
			string strengthType,
			string dosageValue,
			string baseValue,
			string dosageUnit,
			string notes)
		{
			this.DrugCode = drugCode;
			this.ActiveIngredientCode = activeIngredientCode;
			this.IngredientCode = ingredient;
			this.IngredientSuppliedInd = ingredientSuppliedInd;
			this.Strength = strength;
			this.StrengthUnit = strengthUnit;
			this.StrengthType = strengthType;
			this.DosageValue = dosageValue;
			this.Base = baseValue;
			this.DosageUnit = dosageUnit;
			this.Notes = notes;
		}

		public int DrugCode { get;  set; }

		public int ActiveIngredientCode { get;  set; }

		public string IngredientCode { get;  set; }

		public string IngredientSuppliedInd { get;  set; }

		public double Strength { get;  set; }

		public string StrengthUnit { get;  set; }

		public string StrengthType { get;  set; }

		public string DosageValue { get;  set; }

		public string Base { get;  set; }

		public string DosageUnit { get;  set; }

		public string Notes{ get;  set; }

	}
}

