using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class SurveyResponseSequence
    {
        public SurveyResponseSequence()
        {
            SurveyResponseAnswers = new HashSet<SurveyResponseAnswer>();
        }

        public int SurveyResponseSequenceId { get; set; }
        public int SurveyResponseId { get; set; }
        public int SequenceNumber { get; set; }
        public string SequenceText { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual SurveyResponse SurveyResponse { get; set; }
        public virtual ICollection<SurveyResponseAnswer> SurveyResponseAnswers { get; set; }
    }
}
