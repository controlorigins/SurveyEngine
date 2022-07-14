using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class SurveyResponseHistory
    {
        public int SurveyResponseHistoryId { get; set; }
        public int ApplicationUserId { get; set; }
        public int SurveyResponseId { get; set; }
        public string SurveyResponseNm { get; set; } = null!;
        public int StatusId { get; set; }
        public int? QuestionGroupId { get; set; }
        public string? UserNm { get; set; }
        public string? Answers { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual SurveyResponse SurveyResponse { get; set; } = null!;
    }
}
