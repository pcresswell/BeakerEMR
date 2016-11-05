using SQLite;
using System;

namespace Beaker.Data.SQLite
{
	[Table("medication_pharmaceuticals")]
	internal class PharmaceuticalTable : Table
	{
		[Column("drug_code")]
		public int DrugCode { get; private set; }

		[Column("pharmaceutical_std")]
		public string PharmaceuticalSTD { get; private set; }
	}
}
