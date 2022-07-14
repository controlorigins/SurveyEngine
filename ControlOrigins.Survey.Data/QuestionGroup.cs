﻿using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class QuestionGroup
    {
        public QuestionGroup()
        {
            QuestionGroupMembers = new HashSet<QuestionGroupMember>();
        }

        public int QuestionGroupId { get; set; }
        public int SurveyId { get; set; }
        public int GroupOrder { get; set; }
        public string QuestionGroupShortNm { get; set; }
        public string QuestionGroupNm { get; set; }
        public string QuestionGroupDs { get; set; }
        public decimal QuestionGroupWeight { get; set; }
        public string GroupHeader { get; set; }
        public string GroupFooter { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
        public int? DependentQuestionGroupId { get; set; }
        public decimal? DependentMinScore { get; set; }
        public decimal? DependentMaxScore { get; set; }

        public virtual Survey Survey { get; set; }
        public virtual ICollection<QuestionGroupMember> QuestionGroupMembers { get; set; }
    }
}
