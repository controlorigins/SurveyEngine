using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class AppProperty
    {
        public int Id { get; set; }
        public int SiteAppId { get; set; }
        public string Key { get; set; } = null!;
        public string? Value { get; set; }

        public virtual Application SiteApp { get; set; } = null!;
    }
}
