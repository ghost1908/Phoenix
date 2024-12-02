using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using Phoenix.Web.Models.Election.Online.Protocol;
using Phoenix.Web.Models.Election.Protocol;
using Phoenix.Web.Services;

namespace Phoenix.Web.Areas.Election.Controllers
{
    [Authorize]
    [Area("Election")]
    public class ProtocolController : Controller
    {
        private readonly IElectionRepository _electionRepository;
        private readonly IHubContext<ElectionHub> _electionHubContext;
        
        public ProtocolController(IElectionRepository electionRepository, IHubContext<ElectionHub> electionHubContext)
        {
            _electionRepository = electionRepository;
            _electionHubContext = electionHubContext;
        }

        public async Task<IActionResult> Index()
        {
            GetSetUserId();

            var protocols = await _electionRepository.GetProtocolList(Guid.Empty);
            var protocolGroup = protocols.GroupBy(g => new { g.PRCT_NUMBER, g.REG_NAME, g.CMN_NAME })
                .Select(p => new
                {
                    RegionName = p.Key.REG_NAME,
                    CommunityName = p.Key.CMN_NAME,
                    PrecinctNumber = p.Key.PRCT_NUMBER,
                    Protocols = p.Select(s => new { s.PRTL_ID, s.PRTL_STATUS_ID, s.CNCL_TYPE_NAME }).ToDictionary(d => d.CNCL_TYPE_NAME, v => new Tuple<Guid?, int?>(v.PRTL_ID, v.PRTL_STATUS_ID))
                });

            ProtocolListViewModel model = new ProtocolListViewModel();

            model.Councils = protocols.GroupBy(p => p.CNCL_TYPE_NAME).Select(p => p.Key).ToList();
            model.Precincts = protocolGroup.Select(s => new ProtocolItemListViewModel()
            {
                RegionName = s.RegionName,
                CommunityName = s.CommunityName,
                PrecinctNumber = s.PrecinctNumber,
                Protocols = InsertProtocolItemValue(model.Councils, s.Protocols)
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Edit(string id, string returnUrl = null)
        {
            GetSetUserId();

            var protocol = await _electionRepository.GetProtocolEdit(Guid.Parse(id));

            ProtocolViewModel result = new ProtocolViewModel()
            {
                Id = protocol.PRTL_ID.ToString(),
                Name = string.Format(protocol.PRTL_NAME, protocol.PRCT_NUMBER, protocol.CNCL_NAME + " " + protocol.CNCL_TYPE_NAME, protocol.REG_NAME, protocol.DSTR_NUMBER),
                Issue = protocol.PRTL_ISSUE,
                ProtocolItems = protocol.PROTOCOL_ITEMS
                    .Where(p => p.IS_MULTIPLE_VALUE == false)
                    .Select(s => new ProtocolItemViewModel()
                    {
                        Id = s.PRTL_DTL_ID.ToString(),
                        Order = s.PRTL_ITEM_ORDER,
                        IsOperational = s.PRTL_ITEM_ORDER == 1
                            || s.PRTL_ITEM_ORDER == 3
                            || s.PRTL_ITEM_ORDER == 7
                            || s.PRTL_ITEM_ORDER == 9
                            || s.PRTL_ITEM_ORDER == 11 ? 1 : 0,
                        Name = s.PRTL_ITEM_NAME,
                        Value = s.PRTL_ITEM_VALUE,
                        IsMultiple = s.IS_MULTIPLE_VALUE,
                        NoValue = false,
                        ParentGrouped = s.PARENT_GROUPED
                    }).OrderBy(o => o.Order).ToList()
            };

            var multiItemsNoParent = protocol.PROTOCOL_ITEMS
                .Where(p => p.IS_MULTIPLE_VALUE == true && p.PARENT_GROUPED == false)
                .GroupBy(g => new { g.PRTL_ITEM_ORDER, g.PRTL_ITEM_NAME })
                .Select(s => new ProtocolItemViewModel()
                {
                    Id = null,
                    Order = s.Key.PRTL_ITEM_ORDER,
                    IsOperational = s.Key.PRTL_ITEM_ORDER == 11
                            || s.Key.PRTL_ITEM_ORDER == 12 ? 1 : 0,
                    Name = s.Key.PRTL_ITEM_NAME,
                    Value = null,
                    IsMultiple = true,
                    NoValue = true,
                    ParentGrouped = false,
                    ChildItems = s.Select(c => new ProtocolItemViewModel()
                    {
                        Id = c.PRTL_DTL_ID.ToString(),
                        Order = c.CND_ORDER.HasValue ? c.CND_ORDER.Value : 0,
                        IsOperational = s.Key.PRTL_ITEM_ORDER == 11
                            || s.Key.PRTL_ITEM_ORDER == 12 ? 1 : 0,
                        Name = c.CND_FULLNAME,
                        Value = c.PRTL_ITEM_VALUE,
                        IsMultiple = false,
                        NoValue = false,
                        ParentGrouped = false
                    }).OrderBy(o => o.Order).ToList()
                }).ToList();

            result.ProtocolItems.AddRange(multiItemsNoParent);

            var multiItemsWithParent = protocol.PROTOCOL_ITEMS
                .Where(p => p.IS_MULTIPLE_VALUE == true && p.PARENT_GROUPED == true)
                .GroupBy(g => new { g.PRTL_ITEM_ORDER, g.PRTL_ITEM_NAME })
                .Select(s => new ProtocolItemViewModel()
                {
                    Id = null,
                    Order = s.Key.PRTL_ITEM_ORDER,
                    IsOperational = s.Key.PRTL_ITEM_ORDER == 11
                            || s.Key.PRTL_ITEM_ORDER == 12 ? 1 : 0,
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
                        IsOperational = s.Key.PRTL_ITEM_ORDER == 11
                            || s.Key.PRTL_ITEM_ORDER == 12 ? 1 : 0,
                        Name = c.Key.PARENT_FULLNAME,
                        Value = null,
                        IsMultiple = true,
                        NoValue = true,
                        ParentGrouped = false,
                        ChildItems = c.Select(t => new ProtocolItemViewModel()
                        {
                            Id = t.PRTL_DTL_ID.ToString(),
                            Order = t.CND_ORDER.HasValue ? t.CND_ORDER.Value : 0,
                            Name = t.CND_FULLNAME,
                            Value = t.PRTL_ITEM_VALUE,
                            IsMultiple = false,
                            NoValue = false,
                            ParentGrouped = false
                        }).OrderBy(o => o.Order).ThenBy(o => o.Name).ToList()
                    })
                    .OrderBy(o => o.Order).ToList()
                }).ToList();

            result.ProtocolItems.AddRange(multiItemsWithParent);

            ViewData["ReturnUrl"] = returnUrl;

            return View(result);
        }

        public async Task<JsonResult> EditProtocol([FromBody] ProtocolEditModel data)
        {
            GetSetUserId();

            Protocol protocol = new Protocol();
            protocol.PRTL_ID = Guid.Parse(data.Id);
            protocol.PRTL_ISSUE = data.Issue;
            protocol.PROTOCOL_ITEMS = data.Items.Select(s => new ProtocolItem()
            {
                PRTL_DTL_ID = Guid.Parse(s.ItemId),
                PRTL_ITEM_VALUE = s.ItemValue
            }).ToList();

            var result = await _electionRepository.UpdateProtocol(protocol);

            await _electionHubContext.Clients.All.SendAsync("ReceiveOnlineProtocol");

            return Json(result);
        }

        public async Task<IActionResult> ForNK()
        {
            var protocols = await _electionRepository.GetProtocolResultNK();

            var model = protocols.GroupBy(g => new { g.DSTR_NUMBER, g.REG_NAME, g.CMN_NAME, g.PRCT_NUMBER })
                .Select(s => new
                {
                    DistrictNumber = s.Key.DSTR_NUMBER,
                    RegionName = s.Key.REG_NAME,
                    CommunityName = s.Key.CMN_NAME,
                    PrecinctNumber = s.Key.PRCT_NUMBER,
                    Values = s.Select(ss => new
                    {
                        Order = ss.ITEM_ORDER,
                        Party = ss.CND_FULLNAME,
                        Value = ss.PRTL_ITEM_VALUE
                    }).ToList()
                }).Select(s => new ProtocolNKViewModel
                {
                    DistrictNumber=s.DistrictNumber,
                    RegionName = s.RegionName,
                    CommunityName = s.CommunityName,
                    PrecinctNumber = s.PrecinctNumber,
                    Item11 = s.Values.Where(w => w.Order == 11).FirstOrDefault().Value,
                    P1 = s.Values.Where(w => w.Party == "Політична партія \"ОПОЗИЦІЙНИЙ БЛОК\"").FirstOrDefault().Value,
                    P2 = s.Values.Where(w => w.Party == "ПОЛІТИЧНА ПАРТІЯ \"СЛУГА НАРОДУ\"").FirstOrDefault().Value,
                    P3 = s.Values.Where(w => w.Party == "Партія Зелених України").FirstOrDefault().Value,
                    P4 = s.Values.Where(w => w.Party == "Політична партія \"НАШ КРАЙ\"").FirstOrDefault().Value,
                    P5 = s.Values.Where(w => w.Party == "Політична партія \"НОВА ПОЛІТИКА\"").FirstOrDefault().Value,
                    P6 = s.Values.Where(w => w.Party == "політична партія Всеукраїнське об’єднання \"Батьківщина\"").FirstOrDefault().Value,
                    P7 = s.Values.Where(w => w.Party == "ПОЛІТИЧНА ПАРТІЯ \"ОПОЗИЦІЙНА ПЛАТФОРМА – ЗА ЖИТТЯ\"").FirstOrDefault().Value,
                    P8 = s.Values.Where(w => w.Party == "Політична партія \"Радикальна Партія Олега Ляшка\"").FirstOrDefault().Value,
                    P9 = s.Values.Where(w => w.Party == "Політична партія \"За Майбутнє\"").FirstOrDefault().Value,
                    P10 = s.Values.Where(w => w.Party == "ПОЛІТИЧНА ПАРТІЯ \"ЄВРОПЕЙСЬКА СОЛІДАРНІСТЬ\"").FirstOrDefault().Value,
                    P11 = s.Values.Where(w => w.Party == "ПОЛІТИЧНА ПАРТІЯ \"ПАРТІЯ ШАРІЯ\"").FirstOrDefault().Value,
                    P12 = s.Values.Where(w => w.Party == "ПОЛІТИЧНА ПАРТІЯ \"ПАРТІЯ ВОЛОДИМИРА БУРЯКА \"ЄДНАННЯ\"").FirstOrDefault().Value,
                    P13 = s.Values.Where(w => w.Party == "ПОЛІТИЧНА ПАРТІЯ \"ПОРЯДОК\"").FirstOrDefault().Value,

                }).OrderBy(o=>o.DistrictNumber).ThenBy(o=>o.RegionName).ThenBy(o=>o.CommunityName).ThenBy(o=>o.PrecinctNumber).ToList();
                //.OrderBy(o => o.RegionName).ThenBy(o => o.CommunityName).ThenBy(o => o.PrecinctNumber).ToList();
            //P1 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "Політична партія \"ОПОЗИЦІЙНИЙ БЛОК\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P2 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "ПОЛІТИЧНА ПАРТІЯ \"СЛУГА НАРОДУ\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P3 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "Партія Зелених України").FirstOrDefault().PRTL_ITEM_VALUE,
            //P4 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "Політична партія \"НАШ КРАЙ\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P5 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "Політична партія \"НОВА ПОЛІТИКА\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P6 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "політична партія Всеукраїнське об’єднання \"Батьківщина\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P7 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "ПОЛІТИЧНА ПАРТІЯ \"ОПОЗИЦІЙНА ПЛАТФОРМА – ЗА ЖИТТЯ\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P8 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "Політична партія \"Радикальна Партія Олега Ляшка\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P9 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "Політична партія \"За Майбутнє\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P10 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "ПОЛІТИЧНА ПАРТІЯ \"ЄВРОПЕЙСЬКА СОЛІДАРНІСТЬ\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P11 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "ПОЛІТИЧНА ПАРТІЯ \"ПАРТІЯ ШАРІЯ\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P12 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "ПОЛІТИЧНА ПАРТІЯ \"ПАРТІЯ ВОЛОДИМИРА БУРЯКА \"ЄДНАННЯ\"").FirstOrDefault().PRTL_ITEM_VALUE,
            //P13 = s.Where(w => s.Key.ITEM_ORDER == 12 && w.CND_FULLNAME == "ПОЛІТИЧНА ПАРТІЯ \"ПОРЯДОК\"").FirstOrDefault().PRTL_ITEM_VALUE,


            return View(model);
        }

        private List<ProtocolItemValueViewModel> InsertProtocolItemValue(List<string> list, Dictionary<string, Tuple<Guid?, int?>> data)
        {
            ProtocolItemValueViewModel[] result = new ProtocolItemValueViewModel[list.Count];

            foreach (var itemData in data.Keys)
            {
                int idx = list.IndexOf(itemData);
                if (idx >= 0)
                {
                    if (data[itemData].Item1.HasValue)
                        result[idx] = new ProtocolItemValueViewModel()
                        {
                            ProtocolId = data[itemData].Item1.Value.ToString(),
                            ProtocolStatus = data[itemData].Item2.Value
                        };
                    else
                        result[idx] = new ProtocolItemValueViewModel()
                        {
                            ProtocolId = string.Empty,
                            ProtocolStatus = -1
                        };
                }
            }

            for (int i = 0; i < result.Length; i++)
                if (result[i] == null)
                    result[i] = new ProtocolItemValueViewModel()
                    {
                        ProtocolId = string.Empty,
                        ProtocolStatus = -1
                    };

            return result.ToList();
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