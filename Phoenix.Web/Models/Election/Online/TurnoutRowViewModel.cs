using System.Collections.Generic;

namespace Phoenix.Web.Models.Election.Online
{
    public class TurnoutRowViewModel
    {
        public string TerritoryName { get; set; }
        public long TerritoryVoters { get; set; }
        public int TerritoryPrecincts { get; set; }
        public List<TurnoutColumnViewModel> Columns { get; set; }
        public List<TurnoutRowViewModel> Children { get; set; }
    }
}
