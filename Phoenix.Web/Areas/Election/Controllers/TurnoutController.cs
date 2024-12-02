using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Hubs;
using Phoenix.Web.Models.Election;

namespace Phoenix.Web.Areas.Election.Controllers
{
    [Authorize]
    [Area("Election")]
    public class TurnoutController : Controller
    {
        private readonly IElectionRepository _electionRepository;
        private readonly IHubContext<ElectionHub> _electionHubContext;

        public TurnoutController(IElectionRepository electionRepository, IHubContext<ElectionHub> electionHubContext)
        {
            _electionRepository = electionRepository;
            _electionHubContext = electionHubContext;
        }

        public async Task<IActionResult> Index()
        {
            GetSetUserId();

            TurnoutListViewModel model = new TurnoutListViewModel();

            model.TurnoutTimesCount = 4;
            model.TurnoutTimes = new List<TimeSpan>() { TimeSpan.FromHours(11), TimeSpan.FromHours(13), TimeSpan.FromHours(15), TimeSpan.FromHours(20) };

            var turnoutPrecincts = await _electionRepository.GetTurnoutList(Guid.Empty);
            model.PrecinctsTurnout = turnoutPrecincts.Select(s => new TurnoutViewModel()
            {
                Id = s.ELC_PRCT_ID.ToString(),
                RegionName = s.REG_NAME,
                CommunityName = s.CMN_NAME,
                PrecinctNumber = s.PRCT_NUMBER,
                Voters = s.PRCT_VOTERS,
                TurnoutValues = s.TURNOUT_VALUES.ToDictionary(p => p.ELC_TURNOUT_ID.ToString(), v => (int?)v.TURNOUT_VOTERS)
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> ForNK()
        {
            GetSetUserId();

            TurnoutListViewModel model = new TurnoutListViewModel();

            model.TurnoutTimesCount = 4;
            model.TurnoutTimes = new List<TimeSpan>() { TimeSpan.FromHours(11), TimeSpan.FromHours(13), TimeSpan.FromHours(15), TimeSpan.FromHours(20) };

            var turnoutPrecincts = await _electionRepository.GetTurnoutListNK(Guid.Empty);
            model.PrecinctsTurnout = turnoutPrecincts.Select(s => new TurnoutViewModel()
            {
                Id = s.ELC_PRCT_ID.ToString(),
                DistrictNumber = s.DSTR_NUMBER,
                RegionName = s.REG_NAME,
                CommunityName = s.CMN_NAME,
                PrecinctNumber = s.PRCT_NUMBER,
                Voters = s.PRCT_VOTERS,
                TurnoutValues = s.TURNOUT_VALUES.ToDictionary(p => p.ELC_TURNOUT_ID.ToString(), v => (int?)v.TURNOUT_VOTERS)
            }).OrderBy(o => o.PrecinctNumber).OrderBy(o => o.CommunityName).OrderBy(o => o.RegionName).OrderBy(o => o.DistrictNumber)
                .ToList();

            return View(model);
        }

        public async Task<JsonResult> GetTurnoutForEdit(string id)
        {
            GetSetUserId();

            var turnoutPrecinct = await _electionRepository.GetTurnoutPrecinctEdit(Guid.Parse(id));

            //HttpContext.Response.StatusCode = 501;
            TurnoutEditModel model = new TurnoutEditModel();
            model.Id = turnoutPrecinct.ELC_TURNOUT_ID.ToString();
            model.TurnoutTime = turnoutPrecinct.TURNOUT_TIME;
            model.TurnoutVoters = turnoutPrecinct.TURNOUT_VOTERS;

            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> EditTurnoutPrecinct([FromBody]TurnoutEditModel data)
        {
            var result = await _electionRepository.UpdateTurnoutPrecinct(Guid.Parse(data.Id), data.TurnoutVoters);

            await _electionHubContext.Clients.All.SendAsync("ReceiveOnlineTurnout");

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