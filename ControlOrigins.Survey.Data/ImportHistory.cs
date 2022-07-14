using System;

namespace ControlOrigins.Survey.Data
{
    public partial class ImportHistory
    {
        public int ImportHistoryId { get; set; }
        public string FileName { get; set; }
        public string ImportType { get; set; }
        public int NumberOfRows { get; set; }
        public string ImportLog { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
    }
}
