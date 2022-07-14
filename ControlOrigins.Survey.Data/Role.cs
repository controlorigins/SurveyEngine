using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class Role
    {
        public Role()
        {
            ApplicationSurveys = new HashSet<ApplicationSurvey>();
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
        }

        public int RoleId { get; set; }
        public string RoleCd { get; set; }
        public string RoleNm { get; set; }
        public string RoleDs { get; set; }
        public int ReviewLevel { get; set; }
        public bool ReadFl { get; set; }
        public bool UpdateFl { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual ICollection<ApplicationSurvey> ApplicationSurveys { get; set; }
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
