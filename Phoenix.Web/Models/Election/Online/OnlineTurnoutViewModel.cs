using System;
using System.Collections.Generic;

namespace Phoenix.Web.Models.Election.Online
{
    public class OnlineTurnoutViewModel
    {
        public List<TimeSpan> TurnoutTimes { get; set; }
        public List<TurnoutRowViewModel> Items { get; set; }
        public int ColumnCount { get { return this.TurnoutTimes.Count * 3 + 1; } }
    }
}
