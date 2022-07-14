using System;

namespace ControlOrigins.Survey.Data
{
    public partial class WebPortal
    {
        public int WebPortalId { get; set; }
        public string WebPortalNm { get; set; }
        public string WebPortalDs { get; set; }
        public string WebPortalUrl { get; set; }
        public string WebServiceUrl { get; set; }
        public bool? ActiveFl { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
    }
}
