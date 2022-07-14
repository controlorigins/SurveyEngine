
namespace ControlOrigins.Survey.Common.SDK
{
    public class QuestionGroupMemberItem
    {
        public int QuestionGroupMemberID { get; set; }
        public int QuestionGroupID { get; set; }
        public int QuestionID { get; set; }
        public int DisplayOrder { get; set; }
        public double QuestionWeight { get; set; }
        public string QuestionGroupNM { get; set; }
        public string QuestionNM { get; set; }
        public string QuestionShortNM { get; set; }
        public string QuestionGroupShortNM { get; set; }
        public int ModifiedID { get; set; }
        public bool MarkedForDeletion { get; set; } = false;
    }
}