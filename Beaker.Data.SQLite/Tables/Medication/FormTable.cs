
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Beaker.Data.SQLite
{
    [Table("medication_forms")]
	internal class FormTable : Table
	{

        [Column("drug_code")]
		public int DrugCode { get; set; }

        [Column("pharmaceutical_form_code")]
        public int PharmaceuticalFormCode { get; set; }

        [Column("pharmaceutical_form")]
        public string PharmaceuticalForm { get; set; }
	}
}

