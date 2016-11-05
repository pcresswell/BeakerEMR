using System;
using SQLite;
namespace Beaker.Data.SQLite
{
	[Table("patients")]
	public class PatientTable : Table 
	{
		[Column("first_name")]
		public string FirstName { get; set; }

		[Column("last_name")]
		public string LastName { get; set; }
	}
}
