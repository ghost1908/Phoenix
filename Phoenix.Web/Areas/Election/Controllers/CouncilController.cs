using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Models.Election;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Areas.Election.Controllers
{
    [Authorize]
    [Area("Election")]
    public class CouncilController : Controller
    {
        private readonly IElectionRepository _electionRepository;

        public CouncilController(IElectionRepository electionRepository)
        {
            _electionRepository = electionRepository;
        }

        public async Task<IActionResult> Index()
        {
            GetSetUserId();

            var councils = await _electionRepository.GetCouncilListForEdit();
            List<CouncilEditModel> model = councils.GroupBy(g => new { g.CNCL_ID, g.CNCL_NAME, g.CNCL_TYPE_ID, g.CNCL_TYPE_NAME, g.DSTR_TYPE_ID, g.DSTR_TYPE_NAME })
                .Select(s => new CouncilEditModel
                {
                    Id = s.Key.CNCL_ID.ToString(),
                    CouncilName = s.Key.CNCL_NAME,
                    CouncilTypeId = s.Key.CNCL_TYPE_ID.ToString(),
                    CouncilTypeName = s.Key.CNCL_TYPE_NAME,
                    DistrictTypeId = s.Key.DSTR_TYPE_ID.ToString(),
                    DistrictTypeName = s.Key.DSTR_TYPE_NAME,
                    DistrictCount = s.Count()
                }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            GetSetUserId();

            var councils = await _electionRepository.GetCouncilListForEdit();
            CouncilEditModel model = councils.Where(p => p.CNCL_ID == Guid.Parse(id))
                .GroupBy(g => new { g.CNCL_ID, g.CNCL_NAME, g.CNCL_TYPE_ID, g.CNCL_TYPE_NAME, g.DSTR_TYPE_ID, g.DSTR_TYPE_NAME })
                .Select(s => new CouncilEditModel
                {
                    Id = s.Key.CNCL_ID.ToString(),
                    CouncilName = s.Key.CNCL_NAME,
                    CouncilTypeId = s.Key.CNCL_TYPE_ID.ToString(),
                    CouncilTypeName = s.Key.CNCL_TYPE_NAME,
                    DistrictTypeId = s.Key.DSTR_TYPE_ID.ToString(),
                    DistrictTypeName = s.Key.DSTR_TYPE_NAME,
                    DistrictCount = s.Count()
                }).First();

            return View(model);
        }

        private void GetSetUserId()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Guid userGuid;

                //var user = _appUserParser.Parse(HttpContext.User);

                if (Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value ?? "", out userGuid))
                    _electionRepository.UserID = userGuid;
            }
        }
    }
}
