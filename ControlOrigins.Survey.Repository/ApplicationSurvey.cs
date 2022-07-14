using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class ApplicationSurvey
    {
        public int ApplicationSurveyId { get; set; }
        public int ApplicationId { get; set; }
        public int SurveyId { get; set; }
        public int DefaultRoleId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual Application Application { get; set; } = null!;
        public virtual Role DefaultRole { get; set; } = null!;
        public virtual Survey Survey { get; set; } = null!;
    }
}
