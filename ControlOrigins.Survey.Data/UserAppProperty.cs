using System;

namespace ControlOrigins.Survey.Data
{
    public partial class UserAppProperty
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AppId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public virtual Application App { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
