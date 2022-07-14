using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class SiteRole
    {
        public SiteRole()
        {
            ApplicationUsers = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
