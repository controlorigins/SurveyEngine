using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
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
        public string AnswerType { get; set; } = null!;
        public double? AnswerQuantity { get; set; }
        public DateTime? AnswerDate { get; set; }
        public string? AnswerComment { get; set; }
        public string? ModifiedComment { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual Question Question { get; set; } = null!;
        public virtual QuestionAnswer QuestionAnswer { get; set; } = null!;
        public virtual SurveyResponseSequence S { get; set; } = null!;
        public virtual ICollection<SurveyResponseAnswerReview> SurveyResponseAnswerReviews { get; set; }
    }
}
