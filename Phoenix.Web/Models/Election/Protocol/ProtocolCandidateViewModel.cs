using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election
{
    public class ProtocolCandidateViewModel
    {
        public string Id { get; set; }
        public int Order { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public long? Votes { get; set; }
    }
}
