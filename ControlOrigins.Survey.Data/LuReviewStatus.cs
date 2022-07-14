using System;

namespace ControlOrigins.Survey.Data
{
    public partial class LuReviewStatus
    {
        public int ReviewStatusId { get; set; }
        public string ReviewStatusNm { get; set; }
        public string ReviewStatusDs { get; set; }
        public bool ApprovedFl { get; set; }
        public bool CommentFl { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
    }
}
