using System;

namespace ControlOrigins.Survey.Data
{
    public partial class TblFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
