﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Beaker.Data.SQLite
{
    [Table("medication_packages")]
    internal class PackageTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; set; }

        [Column("upc")]
        public string UPC { get; set; }

        [Column("package_size_unit")]
        public string PackageSizeUnit { get; set; }

        [Column("package_type")]
        public string PackageType { get; set; }

        [Column("package_size")]
        public string PackageSize { get; set; }

        [Column("product_information")]
        public string ProductInformation { get; set; }
    }
}
