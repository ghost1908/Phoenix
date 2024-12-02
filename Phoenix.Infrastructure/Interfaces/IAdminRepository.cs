using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Phoenix.Infrastructure.Entities;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IAdminRepository : IBaseRepository
    {
        Task<IEnumerable<TableData>> GetTableDatas(string tableName);
        Task<IEnumerable<ObjectAccess>> GetObjectAccesses();
        Task<IEnumerable<UserAccess>> GetUserAccesses(string userId);
        Task<IEnumerable<string>> GetUserAccessNames(string userId);

        Task<int> AddUserAccess(Guid userId, Guid objectId, object objectValue);
    }
}
