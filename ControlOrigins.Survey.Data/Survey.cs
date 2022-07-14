using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class Survey
    {
        public Survey()
        {
            ApplicationSurveys = new HashSet<ApplicationSurvey>();
            QuestionGroups = new HashSet<QuestionGroup>();
            SurveyEmailTemplates = new HashSet<SurveyEmailTemplate>();
            SurveyResponses = new HashSet<SurveyResponse>();
            SurveyReviewStatuses = new HashSet<SurveyReviewStatus>();
            SurveyStatuses = new HashSet<SurveyStatus>();
        }

        public int SurveyId { get; set; }
        public int SurveyTypeId { get; set; }
        public bool UseQuestionGroupsFl { get; set; }
        public string SurveyNm { get; set; }
        public string SurveyShortNm { get; set; }
        public string SurveyDs { get; set; }
        public string CompletionMessage { get; set; }
        public string ResponseNmtemplate { get; set; }
        public string ReviewerAccountNm { get; set; }
        public string AutoAssignFilter { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
        public int? ParentSurveyId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual LuSurveyType SurveyType { get; set; }
        public virtual ICollection<ApplicationSurvey> ApplicationSurveys { get; set; }
        public virtual ICollection<QuestionGroup> QuestionGroups { get; set; }
        public virtual ICollection<SurveyEmailTemplate> SurveyEmailTemplates { get; set; }
        public virtual ICollection<SurveyResponse> SurveyResponses { get; set; }
        public virtual ICollection<SurveyReviewStatus> SurveyReviewStatuses { get; set; }
        public virtual ICollection<SurveyStatus> SurveyStatuses { get; set; }
    }
}
