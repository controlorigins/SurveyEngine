using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class SurveyResponseState
    {
        public int SurveyResponseStateId { get; set; }
        public int SurveyResponseId { get; set; }
        public int StatusId { get; set; }
        public int AssignedUserId { get; set; }
        public bool Active { get; set; }
        public bool EmailSent { get; set; }
        public string? EmailBody { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual ApplicationUser AssignedUser { get; set; } = null!;
        public virtual LuSurveyResponseStatus Status { get; set; } = null!;
        public virtual SurveyResponse SurveyResponse { get; set; } = null!;
    }
}
