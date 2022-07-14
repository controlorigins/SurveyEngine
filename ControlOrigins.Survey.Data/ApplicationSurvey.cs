using System;

namespace ControlOrigins.Survey.Data
{
    public partial class ApplicationSurvey
    {
        public int ApplicationSurveyId { get; set; }
        public int ApplicationId { get; set; }
        public int SurveyId { get; set; }
        public int DefaultRoleId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual Application Application { get; set; }
        public virtual Role DefaultRole { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
