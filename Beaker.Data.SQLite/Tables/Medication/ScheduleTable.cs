using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Beaker.Data.SQLite
{
    [Table("medication_schedules")]
    internal class ScheduleTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; set; }

        [Column("schedule_code")]
        public string ScheduleCode { get; set; }
    }
}
