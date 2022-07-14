using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class VwQuestionLibrary
    {
        public int QuestionId { get; set; }
        public string QuestionShortNm { get; set; } = null!;
        public string QuestionNm { get; set; } = null!;
        public string QuestionDs { get; set; } = null!;
        public int QuestionSort { get; set; }
        public int ReviewRoleLevel { get; set; }
        public int QuestionValue { get; set; }
        public int? SurveyTypeId { get; set; }
        public string? SurveyTypeShortNm { get; set; }
        public string? SurveyTypeNm { get; set; }
        public int? QuestionTypeId { get; set; }
        public string? QuestionTypeCd { get; set; }
        public string? QuestionTypeDs { get; set; }
        public string? AnswerDataType { get; set; }
        public int? AnswerCount { get; set; }
        public int? MinScore { get; set; }
        public int? MaxScore { get; set; }
        public int? SurveyCount { get; set; }
        public int? CommentFl { get; set; }
        public int? UnitOfMeasureId { get; set; }
        public string? UnitOfMeasureNm { get; set; }
        public string? UnitOfMeasureDs { get; set; }
        public int? ResponseAnswerCount { get; set; }
        public string? Keywords { get; set; }
        public byte[]? FileData { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
    }
}
