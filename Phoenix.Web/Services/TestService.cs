using Phoenix.Web.Models.Election;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Services
{
    public class TestService : ITestService
    {
        private List<PrecinctOpenViewModel> precincts;

        public TestService()
        {
            precincts = new List<PrecinctOpenViewModel>();

            for (int i = 0; i < 10; i++)
            {
                precincts.Add(new PrecinctOpenViewModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Number = (230100 + i).ToString(),
                    RegionName = "Запорізький район",
                    IsOpened = null,
                    NotOpenedCause = null
                });
            }

            precincts[3].IsOpened = true;
            precincts[3].Voters = 345;

            precincts[7].IsOpened = false;
            precincts[7].NotOpenedCause = "bla bla bla";
        }

        public async Task<IEnumerable<PrecinctOpenViewModel>> GetPrecincts()
        {
            return precincts;
        }

        public async Task<PrecinctOpenEditModel> GetPrecinctForEdit(string id)
        {
            PrecinctOpenEditModel model = new PrecinctOpenEditModel();

            var item = precincts.Where(p => p.Id == id).FirstOrDefault();

            model.Id = item.Id;
            model.Number = item.Number;
            model.RegionName = item.RegionName;
            model.IsOpened = item.IsOpened;
            model.NotOpenedCause = item.NotOpenedCause;
            model.Voters = item.Voters ?? 0;
            model.Bulletins = GetCouncils(new Random().Next(1, 4));

            return model;
        }

        private List<PrecinctOpenCouncilEditModel> GetCouncils(int count)
        {
            List<PrecinctOpenCouncilEditModel> result = new List<PrecinctOpenCouncilEditModel>();

            for(int i=0;i<count;i++)
            {
                result.Add(new PrecinctOpenCouncilEditModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    CouncilName = string.Format("Выборы {0}", i + 1),
                    CouncilBulletins = 0
                });
            }

            return result;
        }
    }
}
