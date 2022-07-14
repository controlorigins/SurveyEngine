using System;

namespace ControlOrigins.Survey.Data
{
    public partial class UserMessage
    {
        public int Id { get; set; }
        public int? ToUserId { get; set; }
        public int? FromUserId { get; set; }
        public string Message { get; set; }
        public bool? Opened { get; set; }
        public DateTime? CratedDateTime { get; set; }
        public string Subject { get; set; }
        public bool? Deleted { get; set; }
        public int? AppId { get; set; }
        public int? ShowonPage { get; set; }
        public bool? FromApp { get; set; }

        public virtual ApplicationUser FromUser { get; set; }
        public virtual ApplicationUser ToUser { get; set; }
    }
}
