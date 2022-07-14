using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class LuSurveyType
    {
        public LuSurveyType()
        {
            Questions = new HashSet<Question>();
            Surveys = new HashSet<Survey>();
        }

        public int SurveyTypeId { get; set; }
        public string SurveyTypeShortNm { get; set; }
        public string SurveyTypeNm { get; set; }
        public string SurveyTypeDs { get; set; }
        public string SurveyTypeComment { get; set; }
        public int ApplicationTypeId { get; set; }
        public int? ParentSurveyTypeId { get; set; }
        public bool MutiSequenceFl { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
