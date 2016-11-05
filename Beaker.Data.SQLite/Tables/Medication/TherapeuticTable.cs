using System;
using SQLite;

namespace Beaker.Data.SQLite
{
	[Table("medication_therapeutics")]
	internal class TherapeuticTable : Table
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
