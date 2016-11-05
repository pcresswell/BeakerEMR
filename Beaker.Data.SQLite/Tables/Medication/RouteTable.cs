using System;
using SQLite;

namespace Beaker.Data.SQLite
{
    [Table("medication_routes")]
    internal class RouteTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; set; }

        [Column("route_of_administration_code")]
        public int RouteOfAdministrationCode { get; set; }

        [Column("route_of_administration")]
        public string RouteOfAdministration { get; set; }
    }
}
