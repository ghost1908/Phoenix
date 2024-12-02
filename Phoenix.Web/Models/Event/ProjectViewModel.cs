using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Event
{
    public class ProjectViewModel
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }

        public List<ProjectEventViewModel> Events { get; set; }
    }
}
