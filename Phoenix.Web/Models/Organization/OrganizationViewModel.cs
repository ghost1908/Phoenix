using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Organization
{
    public class OrganizationViewModel
    {
        public string Id { get; set; }
        public string Level { get; set; }
        public string OrganizationName { get; set; }
    }

    public class OrganizationListViewModel
    {
        public string Id { get; set; }
        public string Level { get; set; }
        public string OrganizationName { get; set; }
        public int CountPerson { get; set; }
    }
}
