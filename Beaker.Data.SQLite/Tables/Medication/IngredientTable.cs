using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core;
using SQLite;

namespace Beaker.Data.SQLite
{
    [Table("medication_ingredients")]
	internal class IngredientTable : Table
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

