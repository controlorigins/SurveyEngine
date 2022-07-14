using System;
using System.Collections.Generic;

namespace ControlOrigins.Survey.Repository
{
    public partial class ImportHistory
    {
        public int ImportHistoryId { get; set; }
        public string FileName { get; set; } = null!;
        public string ImportType { get; set; } = null!;
        public int NumberOfRows { get; set; }
        public string? ImportLog { get; set; }
        public int ModifiedId { get; set; }
        public DateTime ModifiedDt { get; set; }
    }
}
