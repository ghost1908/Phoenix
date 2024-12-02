using Phoenix.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IEventRepository : IBaseRepository
    {
        Task<IEnumerable<Project>> GetProjects();
        Task<IEnumerable<Project>> GetProjects(int access);
        Task<Project> CreateProject(Project project);

        Task<IEnumerable<ProjectEvent>> GetProjectEvents(Guid projectId);
        Task<IEnumerable<ProjectEvent>> GetProjectEvents(Guid projectId, int access);
        Task<ProjectEvent> CreateProjectEvent(ProjectEvent projectEvent);

        Task<IEnumerable<PersonEventData>> GetPersonEvents(Guid personId);
        Task<byte?> GetPersonLastEventStatus(Guid personId);
        Task<IEnumerable<Guid>> CreateOrUpdatePersonEvents(Guid psnId, List<PersonEvent> events);
    }
}
