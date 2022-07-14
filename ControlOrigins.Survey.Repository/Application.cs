using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class Application
    {
        public Application()
        {
            AppProperties = new HashSet<AppProperty>();
            ApplicationSurveys = new HashSet<ApplicationSurvey>();
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
            SiteAppMenus = new HashSet<SiteAppMenu>();
            SurveyResponses = new HashSet<SurveyResponse>();
            UserAppProperties = new HashSet<UserAppProperty>();
        }

        public int ApplicationId { get; set; }
        public string ApplicationNm { get; set; } = null!;
        public string ApplicationCd { get; set; } = null!;
        public string ApplicationShortNm { get; set; } = null!;
        public int ApplicationTypeId { get; set; }
        public string? ApplicationDs { get; set; }
        public int MenuOrder { get; set; }
        public string ApplicationFolder { get; set; } = null!;
        public int DefaultPageId { get; set; }
        public int? CompanyId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual LuApplicationType ApplicationType { get; set; } = null!;
        public virtual Company? Company { get; set; }
        public virtual ICollection<AppProperty> AppProperties { get; set; }
        public virtual ICollection<ApplicationSurvey> ApplicationSurveys { get; set; }
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public virtual ICollection<SiteAppMenu> SiteAppMenus { get; set; }
        public virtual ICollection<SurveyResponse> SurveyResponses { get; set; }
        public virtual ICollection<UserAppProperty> UserAppProperties { get; set; }
    }
}
