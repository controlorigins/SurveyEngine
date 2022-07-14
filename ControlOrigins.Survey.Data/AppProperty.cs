using System;

namespace ControlOrigins.Survey.Data
{
    public partial class AppProperty
    {
        public int Id { get; set; }
        public int SiteAppId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public virtual Application SiteApp { get; set; }
    }
}
