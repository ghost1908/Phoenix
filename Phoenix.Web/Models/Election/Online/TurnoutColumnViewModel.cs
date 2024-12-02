using System;

namespace Phoenix.Web.Models.Election.Online
{
    public class TurnoutColumnViewModel
    {
        public TimeSpan TurnoutTime { get; set; }
        public int TurnoutPrecinctCount { get; set; }
        public long TurnoutValue { get; set; }
        public float TurnoutPercent { get; set; }
    }
}
