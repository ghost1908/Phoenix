using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election
{
    public class OpenPrecinctListViewModel
    {
        public string Id { get; set; }
        public string PrecinctNumber { get; set; }
        public string RegionName { get; set; }
        public bool IsOpened { get; set; }
        public string NotOpenedCause { get; set; }
    }
}
