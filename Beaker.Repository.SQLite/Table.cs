using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Repository;
using Beaker.Core;

namespace Beaker.Repository.SQLite
{
    public abstract class Table
    {
        [Column("id")]
        [PrimaryKey]
        public Guid ID { get; set; }

        [Column("domain_object_id")]
        public Guid DomainObjectID { get; set; }

        [Column("record_end_date_time")]
        public DateTime RecordEndDateTime { get; set; }

        [Column("record_start_date_time")]
        public DateTime RecordStartDateTime { get; set; }

        [Column("valid_end_date_time")]
        public DateTime ValidEndDateTime { get; set; }

        [Column("valid_start_date_time")]
        public DateTime ValidStartDateTime { get; set; }

        [Column("author_id")]
        public Guid AuthorID { get; set; }

        /// <summary>
        /// Update the table with the values from the persistable.
        /// </summary>
        /// <param name="persistable"></param>
        public virtual void Update(IPersistable persistable)
        {
            this.AuthorID = persistable.AuthorID;
            this.DomainObjectID = persistable.DomainObjectID;
            this.ID = persistable.ID;
            this.RecordEndDateTime = persistable.RecordEndDateTime;
            this.RecordStartDateTime = persistable.RecordStartDateTime;
            this.ValidEndDateTime = persistable.ValidEndDateTime;
            this.ValidStartDateTime = persistable.ValidStartDateTime;
        }

        /// <summary>
        /// Update the persistable with data from this table.
        /// </summary>
        /// <param name="persistable"></param>
        public virtual void CopyTo(IPersistable persistable)
        {
            persistable.AuthorID = this.AuthorID;
            persistable.DomainObjectID = this.DomainObjectID;
            persistable.ID = this.ID;
            persistable.RecordEndDateTime = this.RecordEndDateTime;
            persistable.RecordStartDateTime = this.RecordStartDateTime;
            persistable.ValidEndDateTime = this.ValidEndDateTime;
            persistable.ValidStartDateTime = this.ValidStartDateTime;
        }
    }
}
