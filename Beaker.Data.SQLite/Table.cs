using System;
using SQLite;
using Beaker.Core;

namespace Beaker.Data.SQLite
{
	public class Table : IRecord
	{
		[Column("id")]
		[PrimaryKey]
		public Guid ID { get; set; }

		[Column("domain_object_id")]
		public Guid DomainObjectID { get; set; }

		[Column("record_end")]
		public DateTime RecordEnd { get; set; }

		[Column("record_start")]
		public DateTime RecordStart { get; set; }

		[Column("valid_end")]
		public DateTime ValidEnd { get; set; }

		[Column("valid_start")]
		public DateTime ValidStart { get; set; }

		[Column("author_id")]
		public Guid AuthorID { get; set; }
	}
}
