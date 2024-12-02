using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Areas.ServiceDesk.Models
{
    public class TaskEditModel
    {
        public string ID { get; set; }
        public string Type { get; set; }
        public string RequestText { get; set; }
    }
}
