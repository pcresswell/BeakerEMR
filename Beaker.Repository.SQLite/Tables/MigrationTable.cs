using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Beaker.Repository.SQLite.Tables
{
    [Table("migrations")]
    public class MigrationTable : Table
    {
        [Column("migration_id")]
        public string MigrationID { get; set; }
    }
}
