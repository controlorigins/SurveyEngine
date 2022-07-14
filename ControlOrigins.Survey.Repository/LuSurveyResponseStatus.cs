using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class LuSurveyResponseStatus
    {
        public LuSurveyResponseStatus()
        {
            SurveyResponseStates = new HashSet<SurveyResponseState>();
        }

        public int StatusId { get; set; }
        public string StatusNm { get; set; } = null!;
        public string StatusDs { get; set; } = null!;
        public string? EmailTemplate { get; set; }
        public int PreviousStatusId { get; set; }
        public int NextStatusId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual ICollection<SurveyResponseState> SurveyResponseStates { get; set; }
    }
}
