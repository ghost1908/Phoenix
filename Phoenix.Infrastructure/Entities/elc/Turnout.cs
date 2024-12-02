using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Infrastructure.Entities.elc
{
    public class TurnoutPrecinctList
    {
        public Guid ELC_PRCT_ID { get; set; }
        public string PRCT_NUMBER { get; set; }
        public string CMN_NAME { get; set; }
        public string REG_NAME { get; set; }
        public int PRCT_VOTERS { get; set; }
        public List<TurnoutPrecinctTimeList> TURNOUT_VALUES { get; set; }
    }

    public class TurnoutPrecinctListNK
    {
        public Guid ELC_PRCT_ID { get; set; }
        public string DSTR_NUMBER { get; set; }
        public string PRCT_NUMBER { get; set; }
        public string CMN_NAME { get; set; }
        public string REG_NAME { get; set; }
        public int PRCT_VOTERS { get; set; }
        public List<TurnoutPrecinctTimeList> TURNOUT_VALUES { get; set; }
    }

    public class TurnoutPrecinctTimeList
    {
        public Guid ELC_PRCT_ID { get; set; }
        public Guid ELC_TURNOUT_ID { get; set; }
        public TimeSpan TURNOUT_TIME { get; set; }
        public int? TURNOUT_VOTERS { get; set; }
    }

    public class TurnoutPrecinctEdit
    {
        public Guid ELC_TURNOUT_ID { get; set; }
        public TimeSpan TURNOUT_TIME { get; set; }
        public int? TURNOUT_VOTERS { get; set; }
    }
}
