using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class SurveyReviewStatus
    {
        public int SurveyReviewStatusId { get; set; }
        public int SurveyId { get; set; }
        public int ReviewStatusId { get; set; }
        public string ReviewStatusNm { get; set; } = null!;
        public string ReviewStatusDs { get; set; } = null!;
        public bool ApprovedFl { get; set; }
        public bool CommentFl { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual Survey Survey { get; set; } = null!;
    }
}
