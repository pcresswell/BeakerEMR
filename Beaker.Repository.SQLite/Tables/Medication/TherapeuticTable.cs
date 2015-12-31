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
    [Table("medication_therapeutics")]
    public class TherapeuticTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; set; }

        [Column("tc_atc_number")]
        public string TCATCNumber { get; set; }

        [Column("tc_atc")]
        public string TCATC { get; set; }

        [Column("tc_ahfs_number")]
        public string TCAHFSNumber { get; set; }

        [Column("tc_ahfs")]
        public string TCAHFS { get; set; }
    }
}
