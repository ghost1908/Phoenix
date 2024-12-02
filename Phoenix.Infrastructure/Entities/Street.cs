using System;

namespace Phoenix.Infrastructure.Entities
{
    public class Street
    {
        public Guid ID { get; set; }
        public Guid STYPE_ID { get; set; }
        public string NAME { get; set; }
    }

    public class StreetList
    {
        public Guid ID { get; set; }
        public string STR_NAME { get; set; }
        public Guid STRT_ID { get; set; }
        public string STRT_NAME { get; set; }
    }
}
