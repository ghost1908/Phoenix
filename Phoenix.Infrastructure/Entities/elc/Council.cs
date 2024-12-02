using System;

namespace Phoenix.Infrastructure.Entities.elc
{
    public class CouncilItem
    {
        public Guid CNCL_ID { get; set; }
        public string CNCL_NAME { get; set; }
        public int WATCH_ORDER { get; set; }
    }

    public class CouncilDto
    {
        public Guid CNCL_ID { get; set; }
        public string CNCL_NAME { get; set; }
        public Guid CNCL_TYPE_ID { get; set; }
        public string CNCL_TYPE_NAME { get; set; }
        public Guid DSTR_ID { get; set; }
        public string DSTR_NUMBER { get; set; }
        public Guid DSTR_TYPE_ID { get; set; }
        public string DSTR_TYPE_NAME { get; set; }
    }
}
