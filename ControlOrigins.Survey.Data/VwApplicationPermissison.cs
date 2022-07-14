﻿using System;

namespace ControlOrigins.Survey.Data
{
    public partial class VwApplicationPermissison
    {
        public int ApplicationId { get; set; }
        public string ApplicationNm { get; set; }
        public string ApplicationCd { get; set; }
        public string ApplicationShortNm { get; set; }
        public int ApplicationTypeId { get; set; }
        public string ApplicationDs { get; set; }
        public int MenuOrder { get; set; }
        public int ApplicationUserRoleId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
        public int ApplicationUserId { get; set; }
        public string FirstNm { get; set; }
        public string LastNm { get; set; }
        public string EMailAddress { get; set; }
        public string CommentDs { get; set; }
        public string AccountNm { get; set; }
        public DateTime? LastLoginDt { get; set; }
        public string LastLoginLocation { get; set; }
        public int RoleId { get; set; }
        public string RoleCd { get; set; }
        public string RoleNm { get; set; }
        public int ReviewLevel { get; set; }
        public bool ReadFl { get; set; }
        public bool UpdateFl { get; set; }
        public string RoleDs { get; set; }
    }
}
