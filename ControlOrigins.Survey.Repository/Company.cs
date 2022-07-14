using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class Company
    {
        public Company()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
            Applications = new HashSet<Application>();
        }

        public int CompanyId { get; set; }
        public string CompanyNm { get; set; } = null!;
        public string CompanyCd { get; set; } = null!;
        public string? CompanyDs { get; set; }
        public string Title { get; set; } = null!;
        public string Theme { get; set; } = null!;
        public string DefaultTheme { get; set; } = null!;
        public string GalleryFolder { get; set; } = null!;
        public string SiteUrl { get; set; } = null!;
        public string Address1 { get; set; } = null!;
        public string? Address2 { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string? FaxNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DefaultPaymentTerms { get; set; }
        public string? DefaultInvoiceDescription { get; set; }
        public bool ActiveFl { get; set; }
        public string? Component { get; set; }
        public string? FromEmail { get; set; }
        public string? Smtp { get; set; }
        public DateTime ModifiedDt { get; set; }
        public int ModifiedId { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
