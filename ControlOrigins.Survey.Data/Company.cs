using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class Company
    {
        public Company()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
            Applications = new HashSet<Application>();
        }

        public int CompanyId { get; set; }
        public string CompanyNm { get; set; }
        public string CompanyCd { get; set; }
        public string CompanyDs { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }
        public string DefaultTheme { get; set; }
        public string GalleryFolder { get; set; }
        public string SiteUrl { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string FaxNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string DefaultPaymentTerms { get; set; }
        public string DefaultInvoiceDescription { get; set; }
        public bool ActiveFl { get; set; }
        public string Component { get; set; }
        public string FromEmail { get; set; }
        public string Smtp { get; set; }
        public DateTime ModifiedDt { get; set; }
        public int ModifiedId { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
