using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
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
        public string ModifiedComment { get; set; } = null!;

        public virtual ApplicationUserRole ApplicationUserRole { get; set; } = null!;
        public virtual SurveyResponseAnswer SurveyAnswer { get; set; } = null!;
    }
}
