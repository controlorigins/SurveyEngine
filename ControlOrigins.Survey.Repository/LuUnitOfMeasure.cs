using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class LuUnitOfMeasure
    {
        public LuUnitOfMeasure()
        {
            Questions = new HashSet<Question>();
        }

        public int UnitOfMeasureId { get; set; }
        public string UnitOfMeasureNm { get; set; } = null!;
        public string? UnitOfMeasureDs { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
