using System;
namespace Beaker.Core
{
	/// <summary>
	/// Represents a record in the storage system. All repositories must operate against an IRecord object
	/// </summary>
	public interface IRecord
	{
		Guid ID { get; set; }
		Guid DomainObjectID { get; set; }
		Guid AuthorID { get; set; }
		DateTime ValidStart { get; set; }
		DateTime ValidEnd { get; set; }
		DateTime RecordStart { get; set; }
		DateTime RecordEnd { get; set; }
	}
}
