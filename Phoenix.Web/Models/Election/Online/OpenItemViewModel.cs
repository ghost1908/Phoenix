using System.Collections.Generic;

namespace Phoenix.Web.Models.Election.Online
{
    public class OpenItemViewModel
    {
        public string TerritoryName { get; set; }
        public int TotalPrecincts { get; set; }
        public int TotalOpened { get; set; }
        public int TotalNotOpened { get; set; }
        public long TotalVoters { get; set; }
        public List<OpenCouncilViewModel> Councils { get; set; }
        
        public List<OpenItemViewModel> Children { get; set; }
    }
}
