using System;
using SQLite;
namespace Beaker.Data.SQLite
{
    [Table("permissions")]
    internal class PermissionTable : Table
    {
        internal PermissionTable()
        {
        }

        [Column("content")]
        internal string Content { get; set; }
    }
}

