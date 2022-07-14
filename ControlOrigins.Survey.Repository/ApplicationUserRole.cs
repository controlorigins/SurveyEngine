using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class ApplicationUserRole
    {
        public ApplicationUserRole()
        {
            SurveyResponseAnswerReviews = new HashSet<SurveyResponseAnswerReview>();
        }

        public int ApplicationUserRoleId { get; set; }
        public int ApplicationId { get; set; }
        public int ApplicationUserId { get; set; }
        public int RoleId { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
        public bool? IsDemo { get; set; }
        public DateTime? StartUpDate { get; set; }
        public bool? IsMonthlyPrice { get; set; }
        public decimal? Price { get; set; }
        public bool? UserInRolled { get; set; }
        public bool? IsUserAdmin { get; set; }

        public virtual Application Application { get; set; } = null!;
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<SurveyResponseAnswerReview> SurveyResponseAnswerReviews { get; set; }
    }
}
