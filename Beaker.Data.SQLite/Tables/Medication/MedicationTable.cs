using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Beaker.Data.SQLite
{
    [Table("medications")]
    internal class MedicationTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; set; }
    }
}
