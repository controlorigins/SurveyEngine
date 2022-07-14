﻿using System;

namespace ControlOrigins.Survey.Data
{
    public partial class VwApplicationSurveyResponsePermission
    {
        public int SurveyResponseId { get; set; }
        public string SurveyResponseNm { get; set; }
        public string AccountNm { get; set; }
        public int ModifiedId { get; set; }
        public int? AssignedUserId { get; set; }
        public int StatusId { get; set; }
        public string DataSource { get; set; }
        public string SurveyShortNm { get; set; }
        public int VariantCount { get; set; }
        public int ComplianceReview { get; set; }
        public string FirstNm { get; set; }
        public string LastNm { get; set; }
        public string EMailAddress { get; set; }
        public string SurveyNm { get; set; }
        public int SurveyId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime ModifiedDt { get; set; }
        public int? DaySinceModified { get; set; }
        public string StatusNm { get; set; }
        public int NextStatusId { get; set; }
        public int PreviousStatusId { get; set; }
    }
}
