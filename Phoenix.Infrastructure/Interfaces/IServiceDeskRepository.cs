using Phoenix.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IServiceDeskRepository : IBaseRepository
    {
        Task<IEnumerable<TaskObject>> GetTasks(bool isAdmin = false);
        Task<TaskObject> GetTask(string id);
        Task<TaskObject> EditTaskState(TaskObject item, byte type);
    }
}
