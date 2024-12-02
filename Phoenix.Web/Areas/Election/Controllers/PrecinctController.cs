using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Phoenix.Identity.Entities;
using Phoenix.Infrastructure.Entities;
using Phoenix.Infrastructure.Entities.elc;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Hubs;
using Phoenix.Web.Models.Election;
using Phoenix.Web.Services;

namespace Phoenix.Web.Areas.Election.Controllers
{
    [Authorize]
    [Area("Election")]
    public class PrecinctController : Controller
    {
        private readonly ITestService _testService;
        private readonly IElectionRepository _electionRepository;
        private readonly IHubContext<ElectionHub> _electionHubContext;

        public PrecinctController(ITestService testService, IElectionRepository electionRepository, IHubContext<ElectionHub> electionHubContext)
        {
            _electionRepository = electionRepository;
            _electionHubContext = electionHubContext;
            _testService = testService;
        }

        public async Task<IActionResult> Index()
        {
            GetSetUserId();

            var precinctLists = await _electionRepository.GetElectionPrecinctOpenList(Guid.Empty);
            var result = precinctLists.Select(s => new PrecinctOpenViewModel()
            {
                Id = s.ELC_PRCT_ID.ToString(),
                Number = s.PRCT_NUMBER,
                RegionName = s.REG_NAME,
                CommunityName = s.CMN_NAME,
                IsOpened = s.PRCT_OPENED,
                NotOpenedCause = s.PRCT_NOTOPEN_CAUSE,
                Voters = s.PRCT_VOTERS
            });

            return View(result);
        }

        public async Task<JsonResult> GetForEdit(string id)
        {
            GetSetUserId();

            var precicnt = await _electionRepository.GetElectionPrecinctEdit(Guid.Parse(id));

            PrecinctOpenEditModel result = new PrecinctOpenEditModel();
            result.Id = precicnt.ELC_PRCT_ID.ToString();
            result.Number = precicnt.PRCT_NUMBER;
            result.RegionName = precicnt.REG_NAME;
            result.IsOpened = precicnt.PRCT_OPENED;
            result.NotOpenedCause = precicnt.PRCT_NOTOPEN_CAUSE;
            result.Voters = precicnt.PRCT_VOTERS ?? 0;
            result.Bulletins = precicnt.BLT_RECEIVED.Select(s => new PrecinctOpenCouncilEditModel()
            {
                Id = s.ELC_PRCT_INFO_ID.ToString(),
                CouncilName = s.CNCL_NAME,
                CouncilBulletins = s.BLT_RECV ?? 0
            }).ToList();

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> EditPrecinctOpen([FromBody]PrecinctOpenEditModel data)
        {
            ElectionPrecinctEdit electionPrecinct = new ElectionPrecinctEdit();
            electionPrecinct.ELC_PRCT_ID = Guid.Parse(data.Id);
            electionPrecinct.PRCT_OPENED = data.IsOpened;
            electionPrecinct.PRCT_NOTOPEN_CAUSE = data.IsOpened.Value == false ? data.NotOpenedCause : null;
            electionPrecinct.PRCT_VOTERS = data.IsOpened.Value == false ? null : (int?)data.Voters;
            electionPrecinct.BLT_RECEIVED = data.Bulletins.Select(s => new ElectionPrecinctInfoEdit() { ELC_PRCT_INFO_ID = Guid.Parse(s.Id), BLT_RECV = data.IsOpened.Value == false ? null : (int?)s.CouncilBulletins }).ToList();

            var result = await _electionRepository.UpdateElectionPrecinctOpen(electionPrecinct);

            await _electionHubContext.Clients.All.SendAsync("ReceiveOnlineOpen");

            return Json(result);
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