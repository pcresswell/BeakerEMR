using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Repository.SQLite;
using Beaker.Core;
using Beaker.Core.Medication;
using AutoMapper;

namespace Beaker.Repository.SQLite.Tables.Medication
{
    [Table("medication_pharmaceuticals")]
    public class PharmaceuticalTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; private set; }

        [Column("pharmaceutical_std")]
        public string PharmaceuticalSTD { get; private set; }
    }
}
