using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class VwAnadarkoApplicationUserValidate
    {
        public string RowAction { get; set; } = null!;
        public int ApplicationUserId { get; set; }
        public int? ApplicationUserRoleId { get; set; }
        public int? ApplicationId { get; set; }
        public int? RoleId { get; set; }
        public int? SupervisorApplicationUserId { get; set; }
    }
}
