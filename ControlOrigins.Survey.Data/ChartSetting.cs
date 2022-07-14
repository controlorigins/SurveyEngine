using System;

namespace ControlOrigins.Survey.Data
{
    public partial class ChartSetting
    {
        public int Id { get; set; }
        public int SiteUserId { get; set; }
        public int SiteAppId { get; set; }
        public string SettingType { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public string SettingValueEnhanced { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
