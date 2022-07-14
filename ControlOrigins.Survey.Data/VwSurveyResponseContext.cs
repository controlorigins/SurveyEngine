using System;

namespace ControlOrigins.Survey.Data
{
    public partial class VwSurveyResponseContext
    {
        public int SurveyResponseId { get; set; }
        public string SurveyResponseNm { get; set; }
        public int SurveyId { get; set; }
        public int ApplicationId { get; set; }
        public int? AssignedUserId { get; set; }
        public int StatusId { get; set; }
        public string DataSource { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
        public string SurveyNm { get; set; }
        public string SurveyShortNm { get; set; }
        public string SurveyDs { get; set; }
        public string ApplicationNm { get; set; }
        public string ApplicationCd { get; set; }
        public string ApplicationShortNm { get; set; }
        public string EMailAddress { get; set; }
        public string FirstNm { get; set; }
        public string LastNm { get; set; }
        public string AccountNm { get; set; }
        public string CommentDs { get; set; }
        public string SupervisorAccountNm { get; set; }
        public int? QuestionId { get; set; }
        public int? QuestionAnswerId { get; set; }
        public string QuestionNm { get; set; }
        public string QuestionAnswerNm { get; set; }
        public int? ApplicationUserId { get; set; }
    }
}
