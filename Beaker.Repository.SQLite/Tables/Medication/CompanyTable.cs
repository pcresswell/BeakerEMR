using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beaker.Core.Medication;
using SQLite;
using AutoMapper;

namespace Beaker.Repository.SQLite.Tables.Medication
{
    [Table("medication_companies")]
    public class CompanyTable : Table
    {
        static CompanyTable()
        {
           
        }

        [Column("drug_code")]
        public int DrugCode { get; set; }

        [Column("mfr_code")]
        public string MFRCode { get; set; }

        [Column("company_code")]
        public string CompanyCode { get; set; }

        [Column("company_name")]
        public string CompanyName { get; set; }

        [Column("company_type")]
        public string CompanyType { get; set; }

        [Column("address_mailing_flag")]
        public string AddressMailingFlag { get; set; }

        [Column("address_billing_flag")]
        public string AddressBillingFlag { get; set; }

        [Column("address_notification_flag")]
        public string AddressNotificationFlag { get; set; }

        [Column("address_other")]
        public string AddressOther { get; set; }

        [Column("suite_number")]
        public string SuiteNumber { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("province")]
        public string Province { get; set; }

        internal void CopyTo(Company company)
        {
            base.CopyTo(company);

        }

        [Column("country")]
        public string Country { get; set; }

        [Column("postal_code")]
        public string PostalCode { get; set; }

        internal void Update(Company company)
        {
            base.Update(company);
        }

        [Column("po_box")]
        public string POBox { get; set; }
    }
}
