using System;

namespace ControlOrigins.Survey.Data
{
    public partial class VwApplicationSurveyResponseSummary
    {
        public string SurveyResponseNm { get; set; }
        public string StatusNm { get; set; }
        public int StatusId { get; set; }
        public string DataSource { get; set; }
        public string SurveyShortNm { get; set; }
        public int? AnswerCount { get; set; }
        public int? QuestionCount { get; set; }
        public int? CommentCount { get; set; }
        public int? PendingReviewCount { get; set; }
        public int? PercentComplete { get; set; }
        public string SurveyNm { get; set; }
        public DateTime ModifiedDt { get; set; }
        public int? DaySinceModified { get; set; }
        public int? AssignedUserId { get; set; }
        public int SurveyResponseId { get; set; }
        public int SurveyId { get; set; }
        public int ApplicationId { get; set; }
        public int ModifiedId { get; set; }
        public string FirstNm { get; set; }
        public string LastNm { get; set; }
        public string EMailAddress { get; set; }
        public int? ApplicationUserId { get; set; }
        public decimal? SurveyResponseScore { get; set; }
        public string ApplicationNm { get; set; }
        public string ApplicationCd { get; set; }
        public string ApplicationShortNm { get; set; }
        public string AccountNm { get; set; }
        public string SupervisorAccountNm { get; set; }
    }
}
