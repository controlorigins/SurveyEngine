using System;

namespace ControlOrigins.Survey.Common.SDK
{
    public class ApplicationChartItem
    {
        public int ApplicationChartId { get; set; }
        public int SiteUserID { get; set; }
        public int SiteAppID { get; set; }
        public string SettingType { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public string SettingValueEnhanced { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}