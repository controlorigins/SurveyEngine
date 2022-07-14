using System;

namespace ControlOrigins.Survey.Data
{
    public partial class SurveyEmailTemplate
    {
        public int SurveyEmailTemplateId { get; set; }
        public string SurveyEmailTemplateNm { get; set; }
        public int SurveyId { get; set; }
        public int StatusId { get; set; }
        public string SubjectTemplate { get; set; }
        public string EmailTemplate { get; set; }
        public string FromEmailAddress { get; set; }
        public string FilterCriteria { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
        public bool? Active { get; set; }
        public bool SendToSupervisor { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual Survey Survey { get; set; }
    }
}
