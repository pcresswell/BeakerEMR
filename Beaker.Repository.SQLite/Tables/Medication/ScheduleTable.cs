using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Core.Medication;
using Beaker.Repository.SQLite.Tables;
using AutoMapper;

namespace Beaker.Repository.SQLite.Tables.Medication
{
    [Table("medication_schedules")]
    public class ScheduleTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; set; }

        [Column("schedule_code")]
        public string ScheduleCode { get; set; }
    }
}
