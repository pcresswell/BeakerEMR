using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Repository.SQLite;
using AutoMapper;
using Beaker.Core.Medication;

namespace Beaker.Repository.SQLite.Tables.Medication
{ 
    [Table("medication_drug_products")]
    public class DrugProductTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; set; }

        [Column("product_categorization")]
        public string ProductCategorization { get; set; }

        [Column("product_class")]
        public string ProductClass { get; set; }

        [Column("drug_identification_number")]
        public string DrugIdentificationNumber { get; set; }

        [Column("brand_name")]
        public string BrandName { get; set; }

        [Column("descriptor")]
        public string Descriptor { get; set; }

        [Column("pediatric_flag")]
        public string PediatricFlag { get; set; }

        [Column("accession_number")]
        public string AccessionNumber { get; set; }

        [Column("number_of_ais")]
        public string NumberOfAIS { get; set; }

        [Column("last_update_date")]
        public DateTime LastUpdateDate { get; set; }

        [Column("ai_group_number")]
        public string AIGroupNumber { get; private set; }
    }
}
