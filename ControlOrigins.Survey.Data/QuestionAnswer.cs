using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class QuestionAnswer
    {
        public QuestionAnswer()
        {
            SurveyResponseAnswers = new HashSet<SurveyResponseAnswer>();
        }

        public int QuestionAnswerId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionAnswerSort { get; set; }
        public string QuestionAnswerShortNm { get; set; }
        public string QuestionAnswerNm { get; set; }
        public int QuestionAnswerValue { get; set; }
        public string QuestionAnswerDs { get; set; }
        public bool CommentFl { get; set; }
        public bool ActiveFl { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual Question Question { get; set; }
        public virtual ICollection<SurveyResponseAnswer> SurveyResponseAnswers { get; set; }
    }
}
