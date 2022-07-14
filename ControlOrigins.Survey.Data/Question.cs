using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class Question
    {
        public Question()
        {
            QuestionAnswers = new HashSet<QuestionAnswer>();
            QuestionGroupMembers = new HashSet<QuestionGroupMember>();
            SurveyResponseAnswers = new HashSet<SurveyResponseAnswer>();
        }

        public int QuestionId { get; set; }
        public int SurveyTypeId { get; set; }
        public string QuestionShortNm { get; set; }
        public string QuestionNm { get; set; }
        public string QuestionDs { get; set; }
        public string Keywords { get; set; }
        public int QuestionSort { get; set; }
        public int ReviewRoleLevel { get; set; }
        public int QuestionTypeId { get; set; }
        public bool CommentFl { get; set; }
        public int QuestionValue { get; set; }
        public int UnitOfMeasureId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
        public byte[] FileData { get; set; }

        public virtual LuQuestionType QuestionType { get; set; }
        public virtual LuSurveyType SurveyType { get; set; }
        public virtual LuUnitOfMeasure UnitOfMeasure { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual ICollection<QuestionGroupMember> QuestionGroupMembers { get; set; }
        public virtual ICollection<SurveyResponseAnswer> SurveyResponseAnswers { get; set; }
    }
}
