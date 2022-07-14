using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class ChartSetting
    {
        public int Id { get; set; }
        public int SiteUserId { get; set; }
        public int SiteAppId { get; set; }
        public string SettingType { get; set; } = null!;
        public string SettingName { get; set; } = null!;
        public string SettingValue { get; set; } = null!;
        public string? SettingValueEnhanced { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
