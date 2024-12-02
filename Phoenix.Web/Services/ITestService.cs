using Phoenix.Web.Models.Election;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Services
{
    public interface ITestService
    {
        Task<IEnumerable<PrecinctOpenViewModel>> GetPrecincts();
        Task<PrecinctOpenEditModel> GetPrecinctForEdit(string id);
    }
}
