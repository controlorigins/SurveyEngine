using ControlOrigins.Survey.Common.SDK.SurveyResponse;
using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Common.SDK
{
    public class ApplicationUserItem
    {
        public int ApplicationUserID { get; set; }
        public string UserLogin { get; set; }
        public string AccountNM { get; set; }
        public string FirstNM { get; set; }
        public string LastNM { get; set; }
        public string EMailAddress { get; set; }
        public string CommentDS { get; set; }
        public int CompanyID { get; set; }
        public string CompanyNM { get; set; }
        public string SupervisorAccountNM { get; set; }
        public DateTime LastLoginDT { get; set; }
        public string LastLoginLocation { get; set; }
        public List<SurveyResponseItem> SurveyResponseList { get; set; } = new List<SurveyResponseItem>();
        public List<ApplicationUserRoleItem> ApplicationUserRoleList { get; set; } = new List<ApplicationUserRoleItem>();
        public int SurveyResponseCount { get; set; }
        public int ApplicationUserRoleCount { get; set; }
        public DateTime ModifiedDT { get; set; }
        public int ModifiedID { get; set; }
        public bool HasMessages { get; set; }
        public int MessageCount { get; set; }
        public int UserRoleID { get; set; }
        public string UserRoleName { get; set; }
        public List<SiteMessageItem> Messages { get; set; } = new List<SiteMessageItem>();
        public List<UserAppPropertyItem> Properties { get; set; } = new List<UserAppPropertyItem>();
        public string DisplayName { get; set; }
        public Guid UserKey { get; set; }
        public string VerifyCode { get; set; }
        public bool EmailVerified { get; set; }
    }
}