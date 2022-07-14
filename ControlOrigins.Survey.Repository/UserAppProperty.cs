using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class UserAppProperty
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AppId { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;

        public virtual Application App { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
    }
}
