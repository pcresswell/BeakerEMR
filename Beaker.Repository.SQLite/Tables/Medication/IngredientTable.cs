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
using SQLite;
using Beaker.Repository.SQLite;
using Beaker.Core;
using AutoMapper;
using Beaker.Core.Medication;

namespace Beaker.Repository.SQLite.Tables.Medication
{
    [Table("medication_ingredients")]
	public class IngredientTable : Table
	{
 
        [Column("drug_code")]
        public int DrugCode { get; set; }

        [Column("active_ingredient_code")]
        public int ActiveIngredientCode { get; set; }

        [Column("ingredient_code")]
        public string IngredientCode { get; set; }

        [Column("ingredient_supplied_ind")]
        public string IngredientSuppliedInd { get; set; }

        [Column("strength")]
        public double Strength { get; set; }

        [Column("strength_unit")]
        public string StrengthUnit { get; set; }

        [Column("strength_type")]
        public string StrengthType { get; set; }

        [Column("dosage_value")]
        public string DosageValue { get; set; }

        [Column("base")]
        public string Base { get; set; }

        [Column("dosage_unit")]
        public string DosageUnit { get; set; }

        [Column("notes")]
        public string Notes { get; set; }

    }
}
