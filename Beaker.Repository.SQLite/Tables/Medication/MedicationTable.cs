using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core.Medication;
using SQLite;
using AutoMapper;

namespace Beaker.Repository.SQLite.Tables.Medication
{
    [Table("medications")]
    public class MedicationTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; set; }

        internal void Update(Core.Medication.Medication medication)
        {
            base.Update(medication);
            this.DrugCode = medication.DrugCode;
        }

        internal void CopyTo(Core.Medication.Medication medication)
        {
            base.CopyTo(medication);
            medication.DrugCode = this.DrugCode;
        }
    }
}
