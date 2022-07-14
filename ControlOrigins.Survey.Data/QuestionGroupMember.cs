using System;

namespace ControlOrigins.Survey.Data
{
    public partial class QuestionGroupMember
    {
        public int QuestionGroupMemberId { get; set; }
        public int QuestionGroupId { get; set; }
        public int QuestionId { get; set; }
        public decimal QuestionWeight { get; set; }
        public int DisplayOrder { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual Question Question { get; set; }
        public virtual QuestionGroup QuestionGroup { get; set; }
    }
}
