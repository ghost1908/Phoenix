using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election
{
    public class ProtocolItemViewModel
    {
        public string Id { get; set; }
        public int Order { get; set; }
        public int IsOperational { get; set; }
        public string Name { get; set; }
        public long? Value { get; set; }
        public bool IsMultiple { get; set; }
        public bool NoValue { get; set; }
        public bool ParentGrouped { get; set; }
        public List<ProtocolItemViewModel> ChildItems { get; set; }
    }
}
