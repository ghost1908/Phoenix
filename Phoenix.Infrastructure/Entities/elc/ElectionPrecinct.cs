using Phoenix.Infrastructure.Entities.elc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Infrastructure.Entities
{
    class ElectionPrecinct
    {
    }

    public class ElectionPrecinctList
    {
        public Guid ELC_PRCT_ID { get; set; }
        public string PRCT_NUMBER { get; set; }
        public string REG_NAME { get; set; }
        public string CMN_NAME { get; set; }
        public bool? PRCT_OPENED { get; set; }
        public string PRCT_NOTOPEN_CAUSE { get; set; }
        public int? PRCT_VOTERS { get; set; }
    }

    public class ElectionPrecinctEdit
    {
        public Guid ELC_PRCT_ID { get; set; }
        public string PRCT_NUMBER { get; set; }
        public string REG_NAME { get; set; }
        public string CMN_NAME { get; set; }
        public bool? PRCT_OPENED { get; set; }
        public string PRCT_NOTOPEN_CAUSE { get; set; }
        public int? PRCT_VOTERS { get; set; }
        public List<ElectionPrecinctInfoEdit> BLT_RECEIVED { get; set; }
    }
}
