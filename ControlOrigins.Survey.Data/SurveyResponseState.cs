using System;

namespace ControlOrigins.Survey.Data
{
    public partial class SurveyResponseState
    {
        public int SurveyResponseStateId { get; set; }
        public int SurveyResponseId { get; set; }
        public int StatusId { get; set; }
        public int AssignedUserId { get; set; }
        public bool Active { get; set; }
        public bool EmailSent { get; set; }
        public string EmailBody { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual ApplicationUser AssignedUser { get; set; }
        public virtual LuSurveyResponseStatus Status { get; set; }
        public virtual SurveyResponse SurveyResponse { get; set; }
    }
}
