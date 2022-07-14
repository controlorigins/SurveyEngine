using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class SurveyResponseAnswerError
    {
        public int SurveyAnswerErrorId { get; set; }
        public int SurveyResponseId { get; set; }
        public int SequenceNumber { get; set; }
        public int QuestionId { get; set; }
        public int? QuestionAnswerId { get; set; }
        public string? AnswerType { get; set; }
        public string? AnswerQuantity { get; set; }
        public string? AnswerDate { get; set; }
        public string? AnswerComment { get; set; }
        public string? ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string ProgramName { get; set; } = null!;
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
    }
}
