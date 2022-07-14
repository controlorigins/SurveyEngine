using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Common.SDK
{
    public class SurveyItem
    {
        public int SurveyID { get; set; }
        public string SurveyNM { get; set; }
        public string SurveyDS { get; set; }
        public string SurveyShortNM { get; set; }
        public string CompletionMessage { get; set; }
        public string AutoAssignFilter { get; set; }
        public string ResponseNMTemplate { get; set; }
        public string ReviewerAccountNM { get; set; }
        public bool UseSurveyGroupsFL { get; set; }
        public DateTime? EndDT { get; set; }
        public DateTime? StartDT { get; set; }
        public int ModifiedID { get; set; }
        public int? ParentSurveyID { get; set; }
        public List<QuestionGroupItem> QuestionGroupList { get; set; } = new List<QuestionGroupItem>();
        public List<QuestionItem> QuestionList { get; set; } = new List<QuestionItem>();
        public List<SurveyEmailTemplateItem> EmailTemplateList { get; set; } = new List<SurveyEmailTemplateItem>();
        public List<SurveyReviewStatusItem> ReviewStatusList { get; set; } = new List<SurveyReviewStatusItem>();
        public List<SurveyStatusItem> StatusList { get; set; } = new List<SurveyStatusItem>();
        public SurveyTypeItem SurveyType { get; set; } = new SurveyTypeItem();
        public int ApplicationCount { get; set; }
        public int SurveyResponseCount { get; set; }
        public int QuestionCount { get; set; }
        public int QuestionGroupCount { get; set; }
    }
}