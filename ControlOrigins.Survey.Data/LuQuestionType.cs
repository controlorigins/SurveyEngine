using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Data
{
    public partial class LuQuestionType
    {
        public LuQuestionType()
        {
            Questions = new HashSet<Question>();
        }

        public int QuestionTypeId { get; set; }
        public string QuestionTypeCd { get; set; }
        public string QuestionTypeDs { get; set; }
        public string ControlName { get; set; }
        public string AnswerDataType { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
