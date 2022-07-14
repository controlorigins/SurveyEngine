using System;

namespace ControlOrigins.Survey.Data
{
    public partial class SurveyResponseAnswerReview
    {
        public int SurveyResponseAnswerReviewId { get; set; }
        public int SurveyAnswerId { get; set; }
        public int ApplicationUserRoleId { get; set; }
        public int ReviewLevel { get; set; }
        public int ReviewStatusId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
        public string ModifiedComment { get; set; }

        public virtual ApplicationUserRole ApplicationUserRole { get; set; }
        public virtual SurveyResponseAnswer SurveyAnswer { get; set; }
    }
}
