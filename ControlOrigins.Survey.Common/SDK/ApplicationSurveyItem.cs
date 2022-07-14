using ControlOrigins.Survey.Common.SDK.SurveyResponse;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Common.SDK
{
    public class ApplicationSurveyItem
    {
        public int ApplicationSurveyID { get; set; }
        public SurveyItem Survey { get; set; } = new SurveyItem();
        public int ApplicationID { get; set; }
        public int DefaultRoleID { get; set; }
        public List<SurveyResponseItem> SurveyResponseList { get; set; } = new List<SurveyResponseItem>();
    }
}