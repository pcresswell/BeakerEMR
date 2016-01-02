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
    /// Ingredient.
    /// </summary>
    public class Ingredient : DomainObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Ingredient"/> class.
        /// </summary>
        public Ingredient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Beaker.Core.Medication.Ingredient"/> class.
        /// </summary>
        /// <param name="drugCode">Drug code.</param>
        /// <param name="activeIngredientCode">Active ingredient code.</param>
        /// <param name="ingredient">Ingredient.</param>
        /// <param name="ingredientSuppliedInd">Ingredient supplied ind.</param>
        /// <param name="strength">Strength.</param>
        /// <param name="strengthUnit">Strength unit.</param>
        /// <param name="strengthType">Strength type.</param>
        /// <param name="dosageValue">Dosage value.</param>
        /// <param name="baseValue">Base value.</param>
        /// <param name="dosageUnit">Dosage unit.</param>
        /// <param name="notes">Notes.</param>
        public Ingredient(
            int drugCode, 
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

        /// <summary>
        /// Gets or sets the drug code.
        /// </summary>
        /// <value>The drug code.</value>
        public int DrugCode { get; set; }

        /// <summary>
        /// Gets or sets the active ingredient code.
        /// </summary>
        /// <value>The active ingredient code.</value>
        public int ActiveIngredientCode { get; set; }

        /// <summary>
        /// Gets or sets the ingredient code.
        /// </summary>
        /// <value>The ingredient code.</value>
        public string IngredientCode { get; set; }

        /// <summary>
        /// Gets or sets the ingredient supplied ind.
        /// </summary>
        /// <value>The ingredient supplied ind.</value>
        public string IngredientSuppliedInd { get; set; }

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        /// <value>The strength.</value>
        public double Strength { get; set; }

        /// <summary>
        /// Gets or sets the strength unit.
        /// </summary>
        /// <value>The strength unit.</value>
        public string StrengthUnit { get; set; }

        /// <summary>
        /// Gets or sets the type of the strength.
        /// </summary>
        /// <value>The type of the strength.</value>
        public string StrengthType { get; set; }

        /// <summary>
        /// Gets or sets the dosage value.
        /// </summary>
        /// <value>The dosage value.</value>
        public string DosageValue { get; set; }

        /// <summary>
        /// Gets or sets the base.
        /// </summary>
        /// <value>The base.</value>
        public string Base { get; set; }

        /// <summary>
        /// Gets or sets the dosage unit.
        /// </summary>
        /// <value>The dosage unit.</value>
        public string DosageUnit { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }
    }
}