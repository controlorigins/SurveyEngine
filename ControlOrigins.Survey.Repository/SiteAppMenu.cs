using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class SiteAppMenu
    {
        public int Id { get; set; }
        public int SiteAppId { get; set; }
        public string MenuText { get; set; } = null!;
        public string TartgetPage { get; set; } = null!;
        public string GlyphName { get; set; } = null!;
        public int MenuOrder { get; set; }
        public int SiteRoleId { get; set; }
        public bool ViewInMenu { get; set; }

        public virtual Application SiteApp { get; set; } = null!;
    }
}
