using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election
{
    public class ProtocolListViewModel
    {
        public List<string> Councils { get; set; }
        public List<ProtocolItemListViewModel> Precincts { get; set; }
    }
}
