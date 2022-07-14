﻿using System;

namespace ControlOrigins.Survey.Common.SDK
{
    [Serializable()]
    public class SurveyResponseSequenceItem
    {
        public int SequenceID { get; set; }
        public int SurveyResponseID { get; set; }
        public int SequenceNumber { get; set; }
        public string SequenceText { get; set; }
    }
}