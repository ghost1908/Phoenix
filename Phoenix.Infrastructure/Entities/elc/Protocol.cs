using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Infrastructure.Entities.elc
{
    public class ProtocolList
    {
        public Guid ELC_PRCT_ID { get; set; }
        public int? PRTL_STATUS_ID { get; set; }
        public Guid? PRTL_ID { get; set; }
        public string PRCT_NUMBER { get; set; }
        public string CNCL_NAME { get; set; }
        public int CNCL_TYPE_ORDER { get; set; }
        public string CNCL_TYPE_NAME { get; set; }
        public string CMN_NAME { get; set; }
        public string REG_NAME { get; set; }
        public string AREA_NAME { get; set; }
    }

    public class Protocol
    {
        public Guid PRTL_ID { get; set; }
        public string PRTL_NAME { get; set; }
        public string PRCT_NUMBER { get; set; }
        public string CNCL_NAME { get; set; }
        public string CNCL_TYPE_NAME { get; set; }
        public string REG_NAME { get; set; }
        public string AREA_NAME { get; set; }
        public string DSTR_NUMBER { get; set; }
        public string PRTL_ISSUE { get; set; }

        public List<ProtocolItem> PROTOCOL_ITEMS { get; set; }
    }

    public class ProtocolItem
    {
        public Guid PRTL_DTL_ID { get; set; }
        public int PRTL_ITEM_ORDER { get; set; }
        public string PRTL_ITEM_NAME { get; set; }
        public long? PRTL_ITEM_VALUE { get; set; }
        public bool IS_MULTIPLE_VALUE { get; set; }
        public string CND_FULLNAME { get; set; }
        public string CND_SHORTNAME { get; set; }
        public bool? CND_TYPE { get; set; }
        public int? CND_ORDER { get; set; }
        public int? CND_WATCH_ORDER { get; set; }
        public int? CND_LIST_ORDER { get; set; }
        public bool PARENT_GROUPED { get; set; }
        public string PARENT_FULLNAME { get; set; }
        public int? PARENT_ORDER { get; set; }
    }

    public class ProtocolResultItem
    {
        public string DSTR_NAME { get; set; }
        public int DSTR_NUMBER { get; set; }
        public string PRCT_NUMBER { get; set; }
        public int PRTL_ITEM_ORDER { get; set; }
        public string PRTL_ITEM_NAME { get; set; }
        public long PRTL_ITEM_VALUE { get; set; }
        public bool IS_MULTIPLE_VALUE { get; set; }
        public string CND_NAME { get; set; }
        public bool? CND_TYPE { get; set; }
        public int? CND_ORDER { get; set; }
        public int? CND_WATCH_ORDER { get; set; }
        public int? CND_LIST_ORDER { get; set; }
        public bool PARENT_GROUPED { get; set; }
        public string PARENT_FULLNAME { get; set; }
        public int? PARENT_ORDER { get; set; }
    }

    public class ProtocolResultNK
    {
        public string DSTR_NUMBER { get; set; }
        public string REG_NAME { get; set; }
        public string CMN_NAME { get; set; }
        public string PRCT_NUMBER { get; set; }
        public int ITEM_ORDER { get; set; }
        public string CND_FULLNAME { get; set; }
        public long? PRTL_ITEM_VALUE { get; set; }
    }
}
