using System;
using System.Collections.Generic;

namespace Phoenix.Infrastructure.Entities.elc
{
    public class OnlineOpenPrecinct
    {
        public string AREA_NAME { get; set; }
        public string REG_NAME { get; set; }
        public string CMN_NAME { get; set; }
        public bool? PRCT_OPENED { get; set; }
        public string PRCT_NUMBER { get; set; }
        public long? PRCT_VOTERS { get; set; }
        public int CNCL_TYPE_ORDER { get; set; }
        public string CNCL_TYPE_NAME { get; set; }
        public long? BULLETIN_RECV { get; set; }
    }

    public class OnlineTurnoutPrecinct
    {
        public string AREA_NAME { get; set; }
        public string REG_NAME { get; set; }
        public string CMN_NAME { get; set; }
        public string CC_NAME { get; set; }
        public string PRCT_NUMBER { get; set; }
        public long? PRCT_VOTERS { get; set; }
        public TimeSpan TURNOUT_TIME { get; set; }
        public int? TURNOUT_VALUE { get; set; }
    }

    public class OnlineProtocolStatus
    {
        public int CNCL_ORDER { get; set; }
        public string CNCL_NAME { get; set; }
        public string AREA_NAME { get; set; }
        public string REG_NAME { get; set; }
        public string CMN_NAME { get; set; }
        public string CC_NAME { get; set; }
        public string PRCT_NUMBER { get; set; }
        public int PRTL_STATUS_ID { get; set; }
    }

    public class OnlineProtocolByCouncil
    {
        public string COUNCIL_NAME { get; set; }
        public List<OnlineProtocolStatusTotal> PRTL_STATUSES { get; set; }
        public List<OnlineProtocolItemTotal> PRTL_ITEMS { get; set; }
    }

    public class OnlineProtocolStatusTotal
    {
        public int PRTL_STATUS_ID { get; set; }
        public int PRTL_STATUS_COUNT { get; set; }
    }

    public class OnlineProtocolItemTotal
    {
        public int PRTL_ITEM_ORDER { get; set; }
        public int? CND_TYPE { get; set; }
        public string CND_NAME { get; set; }
        public string PARENT_NAME { get; set; }
        public long PRTL_ITEM_VALUE { get; set; }
    }
}
