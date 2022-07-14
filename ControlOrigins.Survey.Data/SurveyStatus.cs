using System;

namespace ControlOrigins.Survey.Data
{
    public partial class SurveyStatus
    {
        public int SurveyStatusId { get; set; }
        public int SurveyId { get; set; }
        public int StatusId { get; set; }
        public string StatusNm { get; set; }
        public string StatusDs { get; set; }
        public string EmailTemplate { get; set; }
        public string EmailSubjectTemplate { get; set; }
        public int PreviousStatusId { get; set; }
        public int NextStatusId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual Survey Survey { get; set; }
    }
}
