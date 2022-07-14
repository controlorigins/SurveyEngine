using System;

namespace ControlOrigins.Survey.Data
{
    public partial class VwValidateImportUser
    {
        public string RowAction { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationNm { get; set; }
        public int SurveyId { get; set; }
        public string SurveyNm { get; set; }
        public int ApplicationUserId { get; set; }
        public string SurveyResponseNm { get; set; }
        public int? SurveyResponseId { get; set; }
        public int EmployeeApplicationUserId { get; set; }
        public string AccountNm { get; set; }
        public int ApplicationUserRoleId { get; set; }
        public string DataSource { get; set; }
        public int? StatusId { get; set; }
        public int RoleId { get; set; }
        public string FirstNm { get; set; }
        public string LastNm { get; set; }
        public string EMailAddress { get; set; }
        public string CommentDs { get; set; }
        public int? SupervisorApplicationUserId { get; set; }
        public string SupervisorAccountNm { get; set; }
        public string SupervisorFirstNm { get; set; }
        public string SupervisorLastNm { get; set; }
        public string SupervisoreMailAddress { get; set; }
    }
}
