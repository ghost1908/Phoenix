using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election
{
    public class ProtocolViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Issue { get; set; }
        public List<ProtocolItemViewModel> ProtocolItems { get; set; }
        //public Dictionary<string, ProtocolCandidateViewModel> ProtocolCandidates { get; set; }
    }
}
