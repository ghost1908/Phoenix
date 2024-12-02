using System;

namespace Phoenix.Infrastructure.Entities
{
    public class ElectionInfo
    {
        public Guid ID { get; set; }
        public Guid ET_ID { get; set; }
        public string ELC_NAME { get; set; }
        public DateTime ELC_DATE { get; set; }
    }
}
