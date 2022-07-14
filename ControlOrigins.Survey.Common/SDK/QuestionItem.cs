using System.Collections.Generic;

namespace ControlOrigins.Survey.Common.SDK
{
    public class QuestionItem
    {
        public int QuestionID { get; set; }
        public int QuestionSort { get; set; }
        public string QuestionNM { get; set; }
        public string QuestionShortNM { get; set; }
        public string QuestionTypeCD { get; set; }
        public int QuestionTypeID { get; set; }
        public string ControlNM { get; set; }
        public string AnswerDataType { get; set; }
        public bool CommentFL { get; set; }
        public int MaxQuestionValue { get; set; }
        public string QuestionDS { get; set; }
        public decimal QuestionValue { get; set; }
        public int ReviewRoleLevel { get; set; }
        public int SurveyTypeID { get; set; }
        public int UnitOfMeasureID { get; set; }
        public byte[] FileData { get; set; }
        public int ModifiedID { get; set; }
        public string Keywords { get; set; }
        // 
        // Possible Answers for this Question
        // 
        public List<QuestionAnswerItem> QuestionAnswerItemList { get; set; } = new List<QuestionAnswerItem>();
        // 
        // Only Implemented when Question is in a QuestionGroup or SurveyQuestionList
        public QuestionGroupMemberItem QuestionGroupMember { get; set; } = new QuestionGroupMemberItem();
        // Only Implemented when Question is in a SurveyResponse.Survey.Question 
        public List<SurveyResponseAnswerItem> SurveyResponseAnswerItemList { get; set; } = new List<SurveyResponseAnswerItem>();
        public int SurveyDisplayOrder { get; set; }
    }
}