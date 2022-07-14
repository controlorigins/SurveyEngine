using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class VwApplication
    {
        public int ApplicationId { get; set; }
        public string ApplicationNm { get; set; } = null!;
        public string ApplicationCd { get; set; } = null!;
        public string ApplicationShortNm { get; set; } = null!;
        public string? ApplicationDs { get; set; }
        public int MenuOrder { get; set; }
        public int? ApplicationTypeId { get; set; }
        public string? ApplicationTypeNm { get; set; }
        public string? ApplicationTypeDs { get; set; }
        public int? SurveyCount { get; set; }
        public int? SurveyResponseCount { get; set; }
        public int? UserCount { get; set; }
    }
}
