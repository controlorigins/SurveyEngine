using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class ApplicationUser
    {
        public ApplicationUser()
        {
            ApplicationUserRoles = new HashSet<ApplicationUserRole>();
            SurveyResponseStates = new HashSet<SurveyResponseState>();
            SurveyResponses = new HashSet<SurveyResponse>();
            UserAppProperties = new HashSet<UserAppProperty>();
            UserMessageFromUsers = new HashSet<UserMessage>();
            UserMessageToUsers = new HashSet<UserMessage>();
        }

        public int ApplicationUserId { get; set; }
        public string FirstNm { get; set; } = null!;
        public string LastNm { get; set; } = null!;
        public string EMailAddress { get; set; } = null!;
        public string? CommentDs { get; set; }
        public string AccountNm { get; set; } = null!;
        public string? SupervisorAccountNm { get; set; }
        public DateTime? LastLoginDt { get; set; }
        public string? LastLoginLocation { get; set; }
        public string DisplayName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public Guid UserKey { get; set; }
        public string UserLogin { get; set; } = null!;
        public bool EmailVerified { get; set; }
        public string VerifyCode { get; set; } = null!;
        public int? CompanyId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual Company? Company { get; set; }
        public virtual SiteRole Role { get; set; } = null!;
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public virtual ICollection<SurveyResponseState> SurveyResponseStates { get; set; }
        public virtual ICollection<SurveyResponse> SurveyResponses { get; set; }
        public virtual ICollection<UserAppProperty> UserAppProperties { get; set; }
        public virtual ICollection<UserMessage> UserMessageFromUsers { get; set; }
        public virtual ICollection<UserMessage> UserMessageToUsers { get; set; }
    }
}
