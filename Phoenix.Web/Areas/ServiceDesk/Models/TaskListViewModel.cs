using System.Collections.Generic;

namespace Phoenix.Web.Areas.ServiceDesk.Models
{
    public class TaskListViewModel
    {
        public List<TaskViewModel> Created { get; set; }
        public List<TaskViewModel> Sended { get; set; }
        public List<TaskViewModel> InWork { get; set; }
        public List<TaskViewModel> Completed { get; set; }
        public List<TaskViewModel> Confirmed { get; set; }

        public bool IsServiceDeskRole { get; set; }
    }
}
