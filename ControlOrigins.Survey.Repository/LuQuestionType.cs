using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class LuQuestionType
    {
        public LuQuestionType()
        {
            Questions = new HashSet<Question>();
        }

        public int QuestionTypeId { get; set; }
        public string QuestionTypeCd { get; set; } = null!;
        public string QuestionTypeDs { get; set; } = null!;
        public string ControlName { get; set; } = null!;
        public string AnswerDataType { get; set; } = null!;
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
