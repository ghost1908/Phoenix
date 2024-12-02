using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Areas.ServiceDesk.Models
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public long Number { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }
        public Guid? PerformerId { get; set; }
        public string PerformerName { get; set; }
        public string RequestText { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? AcceptDate { get; set; }
        public DateTime? DoneDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public string ResponseText { get; set; }
    }
}
