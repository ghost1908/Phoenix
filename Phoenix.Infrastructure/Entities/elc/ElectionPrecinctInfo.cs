using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Infrastructure.Entities.elc
{
    class ElectionPrecinctInfo
    {
    }

    public class ElectionPrecinctInfoEdit
    {
        public Guid ELC_PRCT_INFO_ID { get; set; }
        public Guid CNCL_ID { get; set; }
        public string CNCL_NAME { get; set; }
        public int? BLT_RECV { get; set; }
    }
}
