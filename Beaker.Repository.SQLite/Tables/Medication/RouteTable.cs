using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Beaker.Repository.SQLite;
using Beaker.Core.Medication;
using AutoMapper;

namespace Beaker.Repository.SQLite.Tables.Medication
{
    [Table("medication_routes")]
    public class RouteTable : Table
    {
        [Column("drug_code")]
        public int DrugCode { get; set; }

        [Column("route_of_administration_code")]
        public int RouteOfAdministrationCode { get; set; }

        [Column("route_of_administration")]
        public string RouteOfAdministration { get; set; }
    }
}
