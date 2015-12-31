﻿using System;
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
    [Table("medication_statuses")]
    public class StatusTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; private set; }

        [Column("current_status_flag")]
        public string CurrentStatusFlag { get; private set; }

        [Column("status_code")]
        public string StatusCode { get; private set; }

        [Column("history_date")]
        public DateTime HistoryDate { get; private set; }
    }
}
