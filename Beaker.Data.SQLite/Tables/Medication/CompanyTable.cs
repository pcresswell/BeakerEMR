using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace Beaker.Data.SQLite
{
    [Table("medication_companies")]
    internal class CompanyTable : Table
    {
        [Column("drug_code")]
        internal int DrugCode { get; set; }

        [Column("mfr_code")]
        internal string MFRCode { get; set; }

        [Column("company_code")]
        internal string CompanyCode { get; set; }

        [Column("company_name")]
        internal string CompanyName { get; set; }

        [Column("company_type")]
        internal string CompanyType { get; set; }

        [Column("address_mailing_flag")]
        internal string AddressMailingFlag { get; set; }

        [Column("address_billing_flag")]
        internal string AddressBillingFlag { get; set; }

        [Column("address_notification_flag")]
        internal string AddressNotificationFlag { get; set; }

        [Column("address_other")]
        internal string AddressOther { get; set; }

        [Column("suite_number")]
        internal string SuiteNumber { get; set; }

        [Column("street")]
        internal string Street { get; set; }

        [Column("city")]
        internal string City { get; set; }

        [Column("province")]
        internal string Province { get; set; }

        [Column("country")]
        internal string Country { get; set; }

        [Column("postal_code")]
        internal string PostalCode { get; set; }

        [Column("po_box")]
        internal string POBox { get; set; }
    }
}
