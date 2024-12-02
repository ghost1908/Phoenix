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
    [Authorize]
    [Area("Election")]
    public class OnlineController : Controller
    {
        private readonly IElectionRepository _electionRepository;
        //private readonly IHubContext<PrecinctHub> _precinctHubContext;
        
        public OnlineController(IElectionRepository electionRepository)//, IHubContext<PrecinctHub> precinctHubContext)
        {
            _electionRepository = electionRepository;
            //_precinctHubContext = precinctHubContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Open()
        {
            return View();
        }

        public async Task<IActionResult> OpenUpdate()
        {
            GetSetUserId();

            var precincts = await _electionRepository.OnlineOpenPrecincts(Guid.Empty);

            OpenViewModel model = new OpenViewModel();
            model.Councils = precincts.GroupBy(gp => new { gp.CNCL_TYPE_ORDER, gp.CNCL_TYPE_NAME })
                .Select(s => new OpenCouncilViewModel()
                {
                    CouncilTypeOrder = s.Key.CNCL_TYPE_ORDER,
                    CouncilTypeName = s.Key.CNCL_TYPE_NAME
                }).OrderBy(p => p.CouncilTypeOrder).ToList();
            model.MaxCouncils = model.Councils.Count;
            model.Items = precincts.GroupBy(g => g.AREA_NAME)
                .Select(s => new OpenItemViewModel()
                {
                    TerritoryName = s.Key,
                    TotalPrecincts = s.GroupBy(gp => gp.PRCT_NUMBER).Count(),
                    TotalOpened = s.GroupBy(gp => new { gp.PRCT_OPENED, gp.PRCT_NUMBER }).Where(w => w.Key.PRCT_OPENED == true).Count(),
                    TotalNotOpened = s.GroupBy(gp => new { gp.PRCT_OPENED, gp.PRCT_NUMBER }).Where(w => w.Key.PRCT_OPENED == false).Count(),
                    TotalVoters = s.GroupBy(gp => new { gp.PRCT_VOTERS, gp.PRCT_OPENED, gp.PRCT_NUMBER }).Where(w => w.Key.PRCT_OPENED == true).Sum(p => p.Key.PRCT_VOTERS.HasValue ? p.Key.PRCT_VOTERS.Value : 0),
                    Councils = s.GroupBy(gp => new { gp.CNCL_TYPE_ORDER, gp.CNCL_TYPE_NAME }).Select(p => new OpenCouncilViewModel()
                    {
                        CouncilTypeOrder = p.Key.CNCL_TYPE_ORDER,
                        CouncilTypeName = p.Key.CNCL_TYPE_NAME,
                        BulletinsReceived = p.Sum(ps => ps.BULLETIN_RECV)
                    }).ToList(),
                    Children = s.GroupBy(gp => gp.REG_NAME)
                            .Select(s => new OpenItemViewModel()
                            {
                                TerritoryName = s.Key,
                                TotalPrecincts = s.GroupBy(gp => gp.PRCT_NUMBER).Count(),
                                TotalOpened = s.GroupBy(gp => new { gp.PRCT_OPENED, gp.PRCT_NUMBER }).Where(w => w.Key.PRCT_OPENED == true).Count(),
                                TotalNotOpened = s.GroupBy(gp => new { gp.PRCT_OPENED, gp.PRCT_NUMBER }).Where(w => w.Key.PRCT_OPENED == false).Count(),
                                TotalVoters = s.GroupBy(gp => new { gp.PRCT_VOTERS, gp.PRCT_OPENED, gp.PRCT_NUMBER }).Where(w => w.Key.PRCT_OPENED == true).Sum(p => p.Key.PRCT_VOTERS.HasValue ? p.Key.PRCT_VOTERS.Value : 0),
                                Councils = s.GroupBy(gp => new { gp.CNCL_TYPE_ORDER, gp.CNCL_TYPE_NAME })
                                    .Select(p => new OpenCouncilViewModel()
                                    {
                                        CouncilTypeOrder = p.Key.CNCL_TYPE_ORDER,
                                        CouncilTypeName = p.Key.CNCL_TYPE_NAME,
                                        BulletinsReceived = p.Sum(ps => ps.BULLETIN_RECV)
                                    }).ToList(),
                                Children = s.GroupBy(gp => gp.CMN_NAME)
                                    .Select(s => new OpenItemViewModel()
                                    {
                                        TerritoryName = s.Key,
                                        TotalPrecincts = s.GroupBy(gp => gp.PRCT_NUMBER).Count(),
                                        TotalOpened = s.GroupBy(gp => new { gp.PRCT_OPENED, gp.PRCT_NUMBER }).Where(w => w.Key.PRCT_OPENED == true).Count(),
                                        TotalNotOpened = s.GroupBy(gp => new { gp.PRCT_OPENED, gp.PRCT_NUMBER }).Where(w => w.Key.PRCT_OPENED == false).Count(),
                                        TotalVoters = s.GroupBy(gp => new { gp.PRCT_VOTERS, gp.PRCT_OPENED, gp.PRCT_NUMBER }).Where(w => w.Key.PRCT_OPENED == true).Sum(p => p.Key.PRCT_VOTERS.HasValue ? p.Key.PRCT_VOTERS.Value : 0),
                                        Councils = s.GroupBy(gp => new { gp.CNCL_TYPE_ORDER, gp.CNCL_TYPE_NAME }).Select(p => new OpenCouncilViewModel()
                                        {
                                            CouncilTypeOrder = p.Key.CNCL_TYPE_ORDER,
                                            CouncilTypeName = p.Key.CNCL_TYPE_NAME,
                                            BulletinsReceived = p.Sum(ps => ps.BULLETIN_RECV)
                                        }).ToList()
                                    }).ToList()
                            }).ToList()
                }).ToList();

            return PartialView("_OnlineOpenPartial", model);
        }

        public ActionResult Turnout()
        {
            return View();
        }

        public async Task<IActionResult> TurnoutUpdate()
        {
            GetSetUserId();

            var precincts = await _electionRepository.OnlineTurnoutPrecincts(Guid.Empty);

            OnlineTurnoutViewModel model = new OnlineTurnoutViewModel();

            model.TurnoutTimes = precincts.GroupBy(g => g.TURNOUT_TIME).Select(s => s.Key).OrderBy(p => p).ToList();
            model.Items = precincts.GroupBy(g => g.AREA_NAME)
                .Select(s => new TurnoutRowViewModel()
                {
                    TerritoryName = s.Key,
                    TerritoryVoters = s.GroupBy(gp => new { gp.PRCT_NUMBER, gp.PRCT_VOTERS }).Sum(tv => tv.Key.PRCT_VOTERS.HasValue ? tv.Key.PRCT_VOTERS.Value : 0),
                    TerritoryPrecincts= s.GroupBy(gp => new { gp.PRCT_NUMBER, gp.PRCT_VOTERS }).Count(),
                    Columns = s.GroupBy(gp => gp.TURNOUT_TIME).Select(p => new TurnoutColumnViewModel()
                    {
                        TurnoutTime = p.Key,
                        TurnoutPrecinctCount=p.Count(tv=>tv.TURNOUT_VALUE.HasValue),
                        TurnoutValue = p.Sum(tv => tv.TURNOUT_VALUE.HasValue ? tv.TURNOUT_VALUE.Value:0)
                    }).OrderBy(o => o.TurnoutTime).ToList(),
                    Children = s.GroupBy(gp => gp.REG_NAME)
                            .Select(s => new TurnoutRowViewModel()
                            {
                                TerritoryName = s.Key,
                                TerritoryVoters = s.GroupBy(gp => new { gp.PRCT_NUMBER, gp.PRCT_VOTERS }).Sum(tv => tv.Key.PRCT_VOTERS.HasValue ? tv.Key.PRCT_VOTERS.Value : 0),
                                TerritoryPrecincts = s.GroupBy(gp => new { gp.PRCT_NUMBER, gp.PRCT_VOTERS }).Count(),
                                Columns = s.GroupBy(gp => gp.TURNOUT_TIME).Select(p => new TurnoutColumnViewModel()
                                {
                                    TurnoutTime = p.Key,
                                    TurnoutPrecinctCount = p.Count(tv => tv.TURNOUT_VALUE.HasValue),
                                    TurnoutValue = p.Sum(tv => tv.TURNOUT_VALUE.HasValue ? tv.TURNOUT_VALUE.Value : 0)
                                }).OrderBy(o => o.TurnoutTime).ToList(),
                                Children = s.GroupBy(gp => gp.CMN_NAME)
                                    .Select(s => new TurnoutRowViewModel()
                                    {
                                        TerritoryName = s.Key,
                                        TerritoryVoters = s.GroupBy(gp => new { gp.PRCT_NUMBER, gp.PRCT_VOTERS }).Sum(tv => tv.Key.PRCT_VOTERS.HasValue ? tv.Key.PRCT_VOTERS.Value : 0),
                                        TerritoryPrecincts = s.GroupBy(gp => new { gp.PRCT_NUMBER, gp.PRCT_VOTERS }).Count(),
                                        Columns = s.GroupBy(gp => gp.TURNOUT_TIME).Select(p => new TurnoutColumnViewModel()
                                        {
                                            TurnoutTime = p.Key,
                                            TurnoutPrecinctCount = p.Count(tv => tv.TURNOUT_VALUE.HasValue),
                                            TurnoutValue = p.Sum(tv => tv.TURNOUT_VALUE.HasValue ? tv.TURNOUT_VALUE.Value : 0)
                                        }).OrderBy(o => o.TurnoutTime).ToList(),
                                        Children=s.Where(w=>!string.IsNullOrEmpty(w.CC_NAME)).GroupBy(gp=>gp.CC_NAME)
                                        .Select(s=> new TurnoutRowViewModel()
                                        {
                                            TerritoryName = s.Key,
                                            TerritoryVoters = s.GroupBy(gp => new { gp.PRCT_NUMBER, gp.PRCT_VOTERS }).Sum(tv => tv.Key.PRCT_VOTERS.HasValue ? tv.Key.PRCT_VOTERS.Value : 0),
                                            TerritoryPrecincts = s.GroupBy(gp => new { gp.PRCT_NUMBER, gp.PRCT_VOTERS }).Count(),
                                            Columns = s.GroupBy(gp => gp.TURNOUT_TIME).Select(p => new TurnoutColumnViewModel()
                                            {
                                                TurnoutTime = p.Key,
                                                TurnoutPrecinctCount = p.Count(tv => tv.TURNOUT_VALUE.HasValue),
                                                TurnoutValue = p.Sum(tv => tv.TURNOUT_VALUE.HasValue ? tv.TURNOUT_VALUE.Value : 0)
                                            }).OrderBy(o => o.TurnoutTime).ToList()
                                        }).ToList()
                                    }).ToList()
                            }).ToList()
                }).ToList();

            CalculatePercents(model.Items);

            return PartialView("_OnlineTurnoutPartial", model);
        }

        public async Task<IActionResult> Protocol(string councilId = null, string districtId = null, string precinctId = null)
        {
            GetSetUserId();

            List<ProtocolItemViewModel> model = new List<ProtocolItemViewModel>();

            var councils = await _electionRepository.GetCouncilList();
            ViewBag.councilId = councils.Select(s => new SelectListItem { Value = s.CNCL_ID.ToString(), Text = s.CNCL_NAME, Selected = (s.WATCH_ORDER == 0) }).ToList();
            ViewBag.districtId= new List<SelectListItem>();
            ViewBag.precinctId = new List<SelectListItem>();

            if (string.IsNullOrEmpty(councilId))
            {
                return View(model);
            }

            var districts = await _electionRepository.GetDistrictsByCouncil(Guid.Empty, Guid.Parse(councilId));
            ViewBag.districtId = districts.Select(s => new SelectListItem { Value = s.DIC_ID.ToString(), Text = s.DSTR_NAME }).ToList();

            if (!string.IsNullOrEmpty(districtId))
            {
                var precincts = await _electionRepository.GetPrecinctByDistricts(Guid.Empty, Guid.Parse(districtId));
                ViewBag.precinctId = precincts.Select(s => new SelectListItem { Value = s.ELC_PRCT_ID.ToString(), Text = s.PRCT_NUMBER }).ToList();

            }

            var protocols = await _electionRepository.GetProtocolResult(Guid.Empty, Guid.Parse(councilId), string.IsNullOrEmpty(districtId) ? (Guid?)null : Guid.Parse(districtId), string.IsNullOrEmpty(precinctId) ? (Guid?)null : Guid.Parse(precinctId));

            var protocolSingleItems = protocols
                    .Where(p => p.IS_MULTIPLE_VALUE == false)
                    .GroupBy(g => new { g.PRTL_ITEM_ORDER, g.PRTL_ITEM_NAME, g.IS_MULTIPLE_VALUE, g.PARENT_GROUPED })
                    .Select(s => new ProtocolItemViewModel()
                    {
                        Id = null,
                        Order = s.Key.PRTL_ITEM_ORDER,
                        Name = s.Key.PRTL_ITEM_NAME,
                        Value = s.Sum(ss => ss.PRTL_ITEM_VALUE),
                        IsMultiple = s.Key.IS_MULTIPLE_VALUE,
                        NoValue = false,
                        ParentGrouped = s.Key.PARENT_GROUPED
                    }).ToList();
            model.AddRange(protocolSingleItems);

            var multiItemsNoParent = protocols
                .Where(p => p.IS_MULTIPLE_VALUE == true && p.PARENT_GROUPED == false)
                .GroupBy(g => new { g.PRTL_ITEM_ORDER, g.PRTL_ITEM_NAME })
                .Select(s => new ProtocolItemViewModel()
                {
                    Id = null,
                    Order = s.Key.PRTL_ITEM_ORDER,
                    Name = s.Key.PRTL_ITEM_NAME,
                    Value = null,
                    IsMultiple = true,
                    NoValue = true,
                    ParentGrouped = false,
                    ChildItems = s.GroupBy(gc => new { gc.CND_ORDER, gc.CND_NAME })
                    .Select(c => new ProtocolItemViewModel()
                    {
                        Id = null,
                        Order = c.Key.CND_ORDER.HasValue ? c.Key.CND_ORDER.Value : 0,
                        Name = c.Key.CND_NAME,
                        Value = c.Sum(cs => cs.PRTL_ITEM_VALUE),
                        IsMultiple = false,
                        NoValue = false,
                        ParentGrouped = false
                    }).OrderBy(o => o.Order).ToList()
                }).ToList();
            model.AddRange(multiItemsNoParent);

            var multiItemsWithParent = protocols
                .Where(p => p.IS_MULTIPLE_VALUE == true && p.PARENT_GROUPED == true)
                .GroupBy(g => new { g.PRTL_ITEM_ORDER, g.PRTL_ITEM_NAME })
                .Select(s => new ProtocolItemViewModel()
                {
                    Id = null,
                    Order = s.Key.PRTL_ITEM_ORDER,
                    Name = s.Key.PRTL_ITEM_NAME,
                    Value = null,
                    IsMultiple = true,
                    NoValue = true,
                    ParentGrouped = true,
                    ChildItems = s.GroupBy(g => new { g.PARENT_FULLNAME, g.PARENT_ORDER })
                    .Select(c => new ProtocolItemViewModel()
                    {
                        Id = string.Empty,
                        Order = c.Key.PARENT_ORDER.HasValue ? c.Key.PARENT_ORDER.Value : 0,
                        Name = c.Key.PARENT_FULLNAME,
                        Value = null,
                        IsMultiple = true,
                        NoValue = true,
                        ParentGrouped = false,
                        ChildItems = c.GroupBy(gc => new { gc.CND_ORDER, gc.CND_NAME })
                        .Select(t => new ProtocolItemViewModel()
                        {
                            Id = null,
                            Order = t.Key.CND_ORDER.HasValue ? t.Key.CND_ORDER.Value : 0,
                            Name = t.Key.CND_NAME,
                            Value = t.Sum(ts => ts.PRTL_ITEM_VALUE),
                            IsMultiple = false,
                            NoValue = false,
                            ParentGrouped = false
                        }).OrderBy(o => o.Order).ToList()
                    })
                    .OrderBy(o => o.Order).ToList()
                }).ToList();
            model.AddRange(multiItemsWithParent);

            return View(model);
        }

        //public IActionResult Protocol()
        //{
        //    List<TotalProtocolByCouncil> councils = new List<TotalProtocolByCouncil>();
        //    councils.Add(new TotalProtocolByCouncil() { CouncilId = "A49932CE-97E6-4C3B-B7A5-402937E9EEDE" });
        //    councils.Add(new TotalProtocolByCouncil() { CouncilId = "FBC9D713-5126-44EC-9AFC-F0D005660077" });
        //    councils.Add(new TotalProtocolByCouncil() { CouncilId = "CBA6D03D-912B-41AC-B76A-80941A774707" });

        //    return View(councils);
        //}

        public async Task<JsonResult> GetTotalProtocolByCouncil(string id)
        {
            GetSetUserId();

            var protocolTotal = await _electionRepository.GetOnlineProtocolByCouncil(Guid.Empty, Guid.Parse(id));

            TotalProtocolByCouncil model = new TotalProtocolByCouncil()
            {
                CouncilId = id,
                CouncilName = protocolTotal.COUNCIL_NAME,
                Statuses = protocolTotal.PRTL_STATUSES.Select(s => new TotalProtocolStatus()
                {
                    ProtocolStatusId = s.PRTL_STATUS_ID,
                    ProtocolStatusCount = s.PRTL_STATUS_COUNT
                }).ToList(),
                Candidates = protocolTotal.PRTL_ITEMS.Select(s => new TotalProtocolCandidateItem()
                {
                    WatchOrder = s.PRTL_ITEM_ORDER,
                    CandidateName = s.CND_NAME,
                    CandidateValue = s.PRTL_ITEM_VALUE
                }).ToList(),
                TotalStatuses = protocolTotal.PRTL_STATUSES.Sum(s => s.PRTL_STATUS_COUNT)
            };

            foreach(var itemStatus in model.Statuses)
            {
                itemStatus.ProtocolStatusPercent = model.TotalStatuses != 0 ? Math.Round(((double)itemStatus.ProtocolStatusCount / (double)model.TotalStatuses) * 100, 1) : 0;
            }

            return Json(model);
        }

        

        public IActionResult ProtocolStatuses()
        {
            return View();
        }

        public async Task<IActionResult> ElectionResult()
        {
            var councils = await _electionRepository.GetCouncilList();

            ViewBag.councils = councils.Select(s => new SelectListItem { Value = s.CNCL_ID.ToString(), Text = s.CNCL_NAME, Selected = (s.WATCH_ORDER == 0) }).ToList();
            
            return View();
        }

        public async Task<IActionResult> UpdateElectionResult(string councilId = null)
        {
            //councilId = @"A49932CE-97E6-4C3B-B7A5-402937E9EEDE";
            var protocolTotal = await _electionRepository.GetOnlineProtocolByCouncil(Guid.Empty, Guid.Parse(councilId));

            ElectionResultViewModel model = new ElectionResultViewModel();
            model.ProcessedProtocols = (double)protocolTotal.PRTL_STATUSES.Where(p => p.PRTL_STATUS_ID >= 2).Sum(s => s.PRTL_STATUS_COUNT) / (double)protocolTotal.PRTL_STATUSES.Sum(s => s.PRTL_STATUS_COUNT) * 100;
            model.TotalVoters = protocolTotal.PRTL_ITEMS.Where(w => w.PRTL_ITEM_ORDER == 3).Sum(s => s.PRTL_ITEM_VALUE);

            bool has12 = (protocolTotal.PRTL_ITEMS.Where(p => p.PRTL_ITEM_ORDER > 11).Count() > 0);
            long totalCandidateVotes = has12 ?
                    protocolTotal.PRTL_ITEMS.Where(w => w.PRTL_ITEM_ORDER == 12).Sum(s => s.PRTL_ITEM_VALUE) :
                    protocolTotal.PRTL_ITEMS.Where(w => w.PRTL_ITEM_ORDER == 11).Sum(s => s.PRTL_ITEM_VALUE);
            model.TotalVote = totalCandidateVotes;

            if (has12)
            {
                model.CandidateResults = protocolTotal.PRTL_ITEMS
                        .Where(p => p.PRTL_ITEM_ORDER == 12)
                        .OrderByDescending(o => o.PRTL_ITEM_VALUE)
                        .Select((s, index) => new CandidateResultViewModel
                        {
                            Position = index + 1,
                            Type = s.CND_TYPE.HasValue ? s.CND_TYPE.Value : -1,
                            Name = s.CND_NAME,
                            Votes = s.PRTL_ITEM_VALUE,
                            Nomination = s.CND_TYPE == 1 ? (s.PARENT_NAME ?? "Самовисування") : string.Empty,
                            Percent = (double)s.PRTL_ITEM_VALUE / (totalCandidateVotes == 0 ? 100 : (double)totalCandidateVotes) * 100
                        }).ToList();
            }
            else
            {
                model.CandidateResults = protocolTotal.PRTL_ITEMS
                        .Where(p => p.PRTL_ITEM_ORDER == 11)
                        .OrderByDescending(o => o.PRTL_ITEM_VALUE)
                        .Select((s, index) => new CandidateResultViewModel
                        {
                            Position = index + 1,
                            Type = s.CND_TYPE.HasValue ? s.CND_TYPE.Value : -1,
                            Name = s.CND_NAME,
                            Votes = s.PRTL_ITEM_VALUE,
                            Nomination = s.CND_TYPE == 1 ? (s.PARENT_NAME ?? "Самовисування") : string.Empty,
                            Percent = (double)s.PRTL_ITEM_VALUE / (totalCandidateVotes == 0 ? 100 : (double)totalCandidateVotes) * 100
                        }).ToList();
            }

            model.Protocols.Total = protocolTotal.PRTL_STATUSES.Sum(s => s.PRTL_STATUS_COUNT);
            model.Protocols.NoData = protocolTotal.PRTL_STATUSES.Where(s => s.PRTL_STATUS_ID == 0).Sum(s => s.PRTL_STATUS_COUNT);
            model.Protocols.NotOperationalData = protocolTotal.PRTL_STATUSES.Where(s => s.PRTL_STATUS_ID == 1).Sum(s => s.PRTL_STATUS_COUNT);
            model.Protocols.OperationalData = protocolTotal.PRTL_STATUSES.Where(s => s.PRTL_STATUS_ID == 2).Sum(s => s.PRTL_STATUS_COUNT);
            model.Protocols.NotFullData = protocolTotal.PRTL_STATUSES.Where(s => s.PRTL_STATUS_ID == 3).Sum(s => s.PRTL_STATUS_COUNT);
            model.Protocols.FullData = protocolTotal.PRTL_STATUSES.Where(s => s.PRTL_STATUS_ID == 4 || s.PRTL_STATUS_ID == 5).Sum(s => s.PRTL_STATUS_COUNT);
            model.Protocols.WithErrors = protocolTotal.PRTL_STATUSES.Where(s => s.PRTL_STATUS_ID == 4).Sum(s => s.PRTL_STATUS_COUNT);
            model.Protocols.GoodData = protocolTotal.PRTL_STATUSES.Where(s => s.PRTL_STATUS_ID == 5).Sum(s => s.PRTL_STATUS_COUNT);

            return PartialView("_OnlineElectionResultPartial", model);
        }
        //public async Task<IActionResult> GetElectionResult(string councilId)
        //{

        //}

        public async Task<IActionResult> GetProtocolStatuses()
        {
            GetSetUserId();

            var precincts = await _electionRepository.OnlineProtocolStatus(Guid.Empty);

            ProtocolStatusViewModel model = new ProtocolStatusViewModel();

            model.Columns = precincts.GroupBy(g => g.PRTL_STATUS_ID)
                        .Select(s => new ProtocolStatusTypeViewModel { StatusID = s.Key }).ToList();

            //var statuses = Enum.GetValues(typeof(PROTOCOL_STATUS));
            //for (int i = 0; i < statuses.Length; i++)
            //{
            //    if ((int)statuses.GetValue(i) >= 0)
            //    {
            //        model.Columns.Add(new ProtocolStatusTypeViewModel { StatusID=(int)statuses.GetValue(i), StatusName=Enum.})
            //    }
            //}
            model.Rows = precincts.GroupBy(g1 => new { g1.CNCL_ORDER, g1.CNCL_NAME })
                .Select(s1 => new ProtocolStatusItemViewModel
                {
                    WatchOrder = s1.Key.CNCL_ORDER,
                    CouncilName = s1.Key.CNCL_NAME,
                    Statuses = s1.GroupBy(gs => gs.PRTL_STATUS_ID).Select(s2 => new ProtocolStatusTypeViewModel
                    {
                        StatusID = s2.Key,
                        StatusCount = s2.Count()
                    }).ToList(),
                    Children = s1.Where(w => !string.IsNullOrEmpty(w.CC_NAME)).GroupBy(g2 => new { g2.CNCL_ORDER, g2.CNCL_NAME, g2.CC_NAME })
                    .Select(s3 => new ProtocolStatusItemViewModel
                    {
                        WatchOrder = s3.Key.CNCL_ORDER,
                        CouncilName = s3.Key.CC_NAME,
                        Statuses = s3.GroupBy(gs => gs.PRTL_STATUS_ID).Select(s4 => new ProtocolStatusTypeViewModel
                        {
                            StatusID = s4.Key,
                            StatusCount = s4.Count()
                        }).ToList()
                    }).ToList()
                }).ToList();

            return PartialView("_OnlineProtocolStatusPartial", model);
        }

        public async Task<IActionResult> OblZp()
        {
            var obl = await _electionRepository.GetAreaDistrictResult();

            List<OblZpViewModel> model = new List<OblZpViewModel>();
            model = obl.Select(s => new OblZpViewModel
            {
                Order = s.PRTL_ITEM_ORDER,
                CandidateName = s.CND_NAME,
                District1 = s.DST_1,
                District2 = s.DST_2,
                District3 = s.DST_3,
                District4 = s.DST_4,
                District5 = s.DST_5,
                District6 = s.DST_6,
                District7 = s.DST_7,
                District8 = s.DST_8,
                Total = s.DST_1 + s.DST_2 + s.DST_3 + s.DST_4 + s.DST_5 + s.DST_6 + s.DST_7 + s.DST_8
            }).ToList();

            model.Add(new OblZpViewModel
            {
                Order = 10,
                CandidateName = "Явка",
                District1P = ((double)model.Where(w => w.Order == 9).Select(s => s.District1).FirstOrDefault() / (double)model.Where(w => w.Order == 3).Select(s => s.District1).FirstOrDefault()) * 100,
                District2P = ((double)model.Where(w => w.Order == 9).Select(s => s.District2).FirstOrDefault() / (double)model.Where(w => w.Order == 3).Select(s => s.District2).FirstOrDefault()) * 100,
                District3P = ((double)model.Where(w => w.Order == 9).Select(s => s.District3).FirstOrDefault() / (double)model.Where(w => w.Order == 3).Select(s => s.District3).FirstOrDefault()) * 100,
                District4P = ((double)model.Where(w => w.Order == 9).Select(s => s.District4).FirstOrDefault() / (double)model.Where(w => w.Order == 3).Select(s => s.District4).FirstOrDefault()) * 100,
                District5P = ((double)model.Where(w => w.Order == 9).Select(s => s.District5).FirstOrDefault() / (double)model.Where(w => w.Order == 3).Select(s => s.District5).FirstOrDefault()) * 100,
                District6P = ((double)model.Where(w => w.Order == 9).Select(s => s.District6).FirstOrDefault() / (double)model.Where(w => w.Order == 3).Select(s => s.District6).FirstOrDefault()) * 100,
                District7P = ((double)model.Where(w => w.Order == 9).Select(s => s.District7).FirstOrDefault() / (double)model.Where(w => w.Order == 3).Select(s => s.District7).FirstOrDefault()) * 100,
                District8P = ((double)model.Where(w => w.Order == 9).Select(s => s.District8).FirstOrDefault() / (double)model.Where(w => w.Order == 3).Select(s => s.District8).FirstOrDefault()) * 100,
                TotalP = ((double)model.Where(w => w.Order == 9).Select(s => s.Total).FirstOrDefault() / (double)model.Where(w => w.Order == 3).Select(s => s.Total).FirstOrDefault()) * 100
            });

            model = model.OrderBy(o => o.Order).ToList();

            return View(model);
        }

        private void CalculatePercents(List<TurnoutRowViewModel> data)
        {
            foreach(var item in data)
            {
                foreach(var column in item.Columns)
                {
                    column.TurnoutPercent = item.TerritoryVoters == 0 ? 0 : ((float)column.TurnoutValue / (float)item.TerritoryVoters) * 100;
                }
                if (null != item.Children && item.Children.Count > 0)
                    CalculatePercents(item.Children);
            }
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