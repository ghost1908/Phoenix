using System.Collections.Generic;

namespace Phoenix.Web.Models.Election
{
    public class ProtocolItemListViewModel
    {
        public string RegionName { get; set; }
        public string CommunityName { get; set; }
        public string PrecinctNumber { get; set; }
        public List<ProtocolItemValueViewModel> Protocols { get; set; }
    }
}
