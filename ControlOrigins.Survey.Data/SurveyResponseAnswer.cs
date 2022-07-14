using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class SurveyResponseAnswer
    {
        public SurveyResponseAnswer()
        {
            SurveyResponseAnswerReviews = new HashSet<SurveyResponseAnswerReview>();
        }

        public int SurveyAnswerId { get; set; }
        public int SurveyResponseId { get; set; }
        public int SequenceNumber { get; set; }
        public int QuestionId { get; set; }
        public int QuestionAnswerId { get; set; }
        public string AnswerType { get; set; }
        public double? AnswerQuantity { get; set; }
        public DateTime? AnswerDate { get; set; }
        public string AnswerComment { get; set; }
        public string ModifiedComment { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual Question Question { get; set; }
        public virtual QuestionAnswer QuestionAnswer { get; set; }
        public virtual SurveyResponseSequence S { get; set; }
        public virtual ICollection<SurveyResponseAnswerReview> SurveyResponseAnswerReviews { get; set; }
    }
}
