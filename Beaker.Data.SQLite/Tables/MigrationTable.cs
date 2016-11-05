using System;
using SQLite;
namespace Beaker.Data.SQLite
{
	[Table("migrations")]
	internal class MigrationTable : Table
	{
		[Column("migration_id")]
		internal string MigrationID { get; set; }
	}
}
