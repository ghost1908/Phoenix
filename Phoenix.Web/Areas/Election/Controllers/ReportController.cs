using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Phoenix.Identity.Entities;
using Phoenix.Infrastructure.Entities;
using Phoenix.Infrastructure.Entities.elc;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Models.Election;
using Phoenix.Web.Models.Election.Online;
using Phoenix.Web.Models.Election.Online.Result;
using Phoenix.Web.Models.Election.Protocol;
using Phoenix.Web.Services;

namespace Phoenix.Web.Areas.Election.Controllers
{
    [Authorize(Roles = "administrators, limited")]
    [Area("Election")]
    
    public class ReportController : Controller
    {
        private readonly IElectionRepository _electionRepository;
        private readonly ITerritoryRepository _territoryRepository;

        public ReportController(IElectionRepository electionRepository, ITerritoryRepository territoryRepository)
        {
            _electionRepository = electionRepository;
            _territoryRepository = territoryRepository;
        }

        public async Task<IActionResult> Index(string communityId = null, string precinctId = null)
        {
            CommunityReportViewModel model = new CommunityReportViewModel();

            var precincts = await _territoryRepository.GetPrecincts(null, null, null);

            ViewBag.communityId = precincts
                .GroupBy(g => new { g.CMN_REG_ID, g.CMN_NAME })
                .Select(s => new SelectListItem { Value = s.Key.CMN_REG_ID.ToString(), Text = s.Key.CMN_NAME })
                .OrderBy(o => o.Text).ToList();

            ViewBag.precinctId = new List<SelectListItem>();

            if (string.IsNullOrEmpty(communityId))
                return View(model);

            ViewBag.precinctId = precincts.Where(p => p.CMN_REG_ID == Guid.Parse(communityId))
                .GroupBy(g => new { g.ID, g.PRCT_NUMBER })
                .Select(s => new SelectListItem { Value = s.Key.ID.ToString(), Text = s.Key.PRCT_NUMBER })
                .OrderBy(o => o.Text).ToList();

            var candidates = await _electionRepository.GetPartyResult(Guid.Empty, Guid.Parse(communityId), string.IsNullOrEmpty(precinctId) ? (Guid?)null : Guid.Parse(precinctId));
            List<CandidatePosition> positions = new List<CandidatePosition>();

            model.Columns = candidates.GroupBy(g => new { g.WATCH_ORDER, g.CNCL_NAME })
                .Select(s => new CommunityReportColumnItemViewModel { Order = s.Key.WATCH_ORDER, Name = s.Key.CNCL_NAME })
                .OrderBy(o => o.Order).ToList();

            foreach (var col in model.Columns)
            {
                var cnds = candidates.Where(w => w.WATCH_ORDER == col.Order).OrderByDescending(o => o.ITEM_VALUE)
                    .Select((s, i) => new CandidatePosition
                    {
                        CandidateName = s.CND_FULLNAME,
                        Council = s.WATCH_ORDER,
                        Position = i + 1
                    }).ToList();

                positions.AddRange(cnds);
            }

            if (model.Columns.Count>2)
            {
                model.Columns.Insert(0, model.Columns[2]);
                model.Columns.RemoveAt(3);
            }

            model.Rows = candidates.Where(p => p.WATCH_ORDER == 0)
                .Select(s => new CommunityReportRowItemViewModel
                {
                    Order = s.CND_ORDER,
                    Name = s.CND_FULLNAME,
                    Values = model.Columns.Select(c => new CommunityReportRowValueViewModel
                    {
                        ColumnOrder = c.Order
                    }).ToList()
                }).OrderBy(o => o.Order).ToList();

            foreach (var item in candidates)
            {
                var row = model.Rows.Find(p => p.Name == item.CND_FULLNAME);
                long total = candidates.Where(p => p.WATCH_ORDER == item.WATCH_ORDER).Sum(s => s.ITEM_VALUE);

                if (row != null)
                {
                    var value = row.Values.Find(p => p.ColumnOrder == item.WATCH_ORDER);
                    if (value != null)
                    {
                        value.Order = item.CND_ORDER;
                        value.Value = item.ITEM_VALUE;
                        value.Percent = (double)item.ITEM_VALUE / (double)total * 100;
                    }
                }
                else
                {
                    row = new CommunityReportRowItemViewModel
                    {
                        Order = 1000,
                        Name = item.CND_FULLNAME,
                        Values = model.Columns.Select(c => new CommunityReportRowValueViewModel
                        {
                            ColumnOrder = c.Order
                        }).ToList()
                    };
                    var value = row.Values.Find(p => p.ColumnOrder == item.WATCH_ORDER);
                    if (value != null)
                    {
                        value.Order = item.CND_ORDER;
                        value.Value = item.ITEM_VALUE;
                        value.Percent = (double)item.ITEM_VALUE / (double)total * 100;
                    }
                    model.Rows.Add(row);
                }
            }

            model.Rows = model.Rows.OrderByDescending(o => o.Values[0].Value).ToList();

            foreach(var row in model.Rows)
            {
                foreach(var col in row.Values)
                {
                    var pos = positions.Where(p => p.CandidateName == row.Name && p.Council == col.ColumnOrder).Select(s => s.Position).FirstOrDefault();
                    if (pos > 0)
                        col.Position = pos;
                }
            }
                        
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