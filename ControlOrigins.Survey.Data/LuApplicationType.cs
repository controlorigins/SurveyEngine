using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class LuApplicationType
    {
        public LuApplicationType()
        {
            Applications = new HashSet<Application>();
        }

        public int ApplicationTypeId { get; set; }
        public string ApplicationTypeNm { get; set; }
        public string ApplicationTypeDs { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual ICollection<Application> Applications { get; set; }
    }
}
