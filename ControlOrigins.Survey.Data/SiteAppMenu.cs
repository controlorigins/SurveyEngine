using System;

namespace ControlOrigins.Survey.Data
{
    public partial class SiteAppMenu
    {
        public int Id { get; set; }
        public int SiteAppId { get; set; }
        public string MenuText { get; set; }
        public string TartgetPage { get; set; }
        public string GlyphName { get; set; }
        public int MenuOrder { get; set; }
        public int SiteRoleId { get; set; }
        public bool ViewInMenu { get; set; }

        public virtual Application SiteApp { get; set; }
    }
}
