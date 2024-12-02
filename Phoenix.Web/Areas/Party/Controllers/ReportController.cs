using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Models.Report;
using Phoenix.Web.Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phoenix.Web.Models.Territory;

namespace Phoenix.Web.Areas.Party.Controllers
{
    [Authorize]
    [Area("Party")]
    public class ReportController : Controller
    {
        private readonly IReportRepository _reportRepository;
        private readonly ITerritoryRepository _territoryRepository;
        private readonly IEventRepository _eventRepository;

        public ReportController(IReportRepository reportRepository, ITerritoryRepository territoryRepository, IEventRepository eventRepository)
        {
            _reportRepository = reportRepository;
            _territoryRepository = territoryRepository;
            _eventRepository = eventRepository;
        }

        public async Task<IActionResult> CreatedByPeriod(string startDate, string endDate)
        {
            DateTime _startDate;
            DateTime _endDate;
            IEnumerable<CreatedPersonsViewModel> model;

            GetSetUserId();

            if (!DateTime.TryParse(endDate, out _endDate))
                _endDate = DateTime.Now.Date;

            if (!DateTime.TryParse(startDate, out _startDate))
                _startDate = _endDate.Subtract(TimeSpan.FromDays(7));

            var persons = await _reportRepository.GetCreatedPersons(_startDate, _endDate);

            model = persons.Select(s => new CreatedPersonsViewModel
            {
                RegionName = s.REG_NAME,
                CommunityName = s.CMN_NAME,
                UserName = s.USER_NAME,
                TotalCount = s.PSN_COUNT
            }).ToList();

            ViewData["sDate"] = string.Format("{0:yyyy-MM-dd}", _startDate);
            ViewData["eDate"] = string.Format("{0:yyyy-MM-dd}", _endDate);
            ViewData["Total"] = model.Sum(s => s.TotalCount);

            return View(model);
        }

        [Authorize(Roles = "administrators, limited")]
        public async Task<IActionResult> PlanResult()
        {
            List<CommunityPlansViewModel> model = new List<CommunityPlansViewModel>();
            DateTime sDate, eDate;

            eDate = DateTime.Now.Date;
            sDate = eDate.AddDays(-7);

            ViewData["StartDate"] = sDate;
            ViewData["EndDate"] = eDate.AddDays(-1);

            GetSetUserId();

            var plans = await _reportRepository.GetCommunityPlans(sDate, eDate);

            var grpByUser = plans.GroupBy(g => new { g.REG_NAME, g.CMN_NAME, g.USER_NAME }).Select(s => new CommunityPlansViewModel
            {
                RegionName = s.Key.REG_NAME,
                CommunityName = s.Key.CMN_NAME,
                UserName = s.Key.USER_NAME,
                WeekCount = s.Sum(wc => wc.PSN_WEEK),
                TotalCount = s.Sum(tc => tc.PSN_COUNT),
                PlanCount = 0,
                Order = 2
            });

            var grpByCommunity = plans.GroupBy(g => new { g.REG_NAME, g.CMN_NAME, g.PSN_PLAN }).Select(s => new CommunityPlansViewModel
            {
                RegionName = s.Key.REG_NAME,
                CommunityName = s.Key.CMN_NAME,
                UserName = string.Empty,
                WeekCount = s.Sum(wc => wc.PSN_WEEK),
                TotalCount = s.Sum(tc => tc.PSN_COUNT),
                PlanCount = s.Key.PSN_PLAN,
                CssStyle= "background-color: #2196f3; font-weight: bold; font-style: italic",
                Order = 1
            });

            var grpByRegion = grpByCommunity.GroupBy(g => g.RegionName).Select(s => new CommunityPlansViewModel
            {
                RegionName = s.Key,
                CommunityName = string.Empty,
                UserName = string.Empty,
                WeekCount = s.Sum(wc => wc.WeekCount),
                TotalCount = s.Sum(tc => tc.TotalCount),
                PlanCount = s.Sum(pc => pc.PlanCount),
                CssStyle = "background-color: #7fff79; font-weight: bold",
                Order = 0
            });

            model.AddRange(grpByRegion);
            model.AddRange(grpByCommunity);
            model.AddRange(grpByUser);

            foreach(var item in model)
            {
                item.PlanPercent = item.PlanCount == 0 ? (float?)null : (float)item.TotalCount / (float)item.PlanCount * 100f;
            }

            ViewData["Total"] = grpByRegion.GroupBy(g => g.CommunityName).Select(s => new CommunityPlansViewModel
            {
                RegionName = "Всього по області",
                CommunityName = string.Empty,
                UserName = string.Empty,
                WeekCount = s.Sum(wc => wc.WeekCount),
                TotalCount = s.Sum(tc => tc.TotalCount),
                PlanCount = s.Sum(pc => pc.PlanCount),
                PlanPercent = (float?)s.Sum(tc => tc.TotalCount) / (float?)s.Sum(pc => pc.PlanCount) * 100f
            }).FirstOrDefault();

            return View(model.OrderBy(o => o.RegionName).ThenBy(o => o.CommunityName).ThenBy(o => o.Order).ThenBy(o => o.UserName));
        }

        [Authorize(Roles = "administrators, limited")]
        public async Task<IActionResult> PersonState(string areaId = null, string regionId = null, string communityId = null)
        {
            GetSetUserId();

            var areas = await _territoryRepository.GetAreas();
            ViewBag.Areas = areas
                .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.NAME })
                .OrderBy(o => o.Text)
                .ToList();

            var regions = await _territoryRepository.GetRegions(areaId);
            ViewBag.Regions = regions
                .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = s.NAME })
                .OrderBy(o => o.Text)
                .ToList();

            var communities = await _territoryRepository.GetCommunities(areaId, null, regionId);
            ViewBag.Communities = communities
                .Select(s => new SelectListItem { Value = s.CommunityInRegionID.ToString(), Text = s.CommunityName })
                .OrderBy(o => o.Text)
                .ToList();

            List<PersonStateViewModel> model = new List<PersonStateViewModel>();

            var states = await _reportRepository.GetPersonState(
                string.IsNullOrWhiteSpace(areaId) ? (Guid?)null : Guid.Parse(areaId),
                string.IsNullOrWhiteSpace(regionId) ? (Guid?)null : Guid.Parse(regionId),
                string.IsNullOrWhiteSpace(communityId) ? (Guid?)null : Guid.Parse(communityId)
                );

            model = states.Select(s => new PersonStateViewModel
            {
                Level = string.Empty,
                Name = s.Name,
                Count = s.Count,
                Style = s.Style
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "administrators, limited")]
        public async Task<IActionResult> GetPersonBirthday(string birthDay)
        {
            GetSetUserId();

            DateTime _birthDay;
            if (!DateTime.TryParse(birthDay, out _birthDay))
                _birthDay = DateTime.Now;
            
            var people = await _reportRepository.GetPersonByBirthday(_birthDay);
            var model = people.Select(async s => new PersonBirthdayViewModel
            {
                PersonId = s.ID,
                LastName = s.LNAME,
                FirstName = s.FNAME,
                MiddleName = s.MNAME,
                Gender = s.GENDER.Value ? GENDER.Male : GENDER.Female,
                BirthDate = s.BDATE,
                AddressRegistration = await GetFullAddress(s.ADDR_REG ?? Guid.Empty, s.ADDR_REG_ROOM),
                IsEmployee = s.IS_EMPLOYEE,
                IsDeputy = s.IS_DEPUTY,
                IsPartyMember = s.IS_PARTY_MEMBER,
                IsCongratulated = await HasBirthdayEvent(s.ID),
            }).Select(t => t.Result);

            return View(model);
        }

        [Authorize(Roles = "administrators, limited")]
        public async Task<IActionResult> PersonEvent(string eventId = null)
        {
            var model = new object();

            return View(model);
        }

        private async Task<AddressViewModel> GetFullAddress(Guid addressID, string apartmentNumber)
        {
            var fa = await _territoryRepository.GetFullAddress(addressID);

            if (fa == null)
                return new AddressViewModel();

            return new AddressViewModel()
            {
                ID = fa.ID.ToString(),
                AreaID = fa.AREA_ID.ToString(),
                AreaName = fa.AREA_NAME,
                RegionInAreaID = fa.REG_ARIA_ID.ToString(),
                RegionID = fa.REG_ID.ToString(),
                RegionName = fa.REG_NAME,
                CommunityInRegionID = fa.CMN_RIA_ID.ToString(),
                CommunityID = fa.CMN_ID.ToString(),
                CommunityName = fa.CMN_NAME,
                CityInCommunityID = fa.CITY_IN_CMN_ID.ToString(),
                CityID = fa.CITY_ID.ToString(),
                CityName = fa.CITY_NAME,
                CityTypeID = fa.CITY_TYPE_ID.ToString(),
                CityTypeName = fa.CITY_TYPE_NAME,
                StreetInCityID = fa.STREET_IN_CITY_ID.ToString(),
                StreetID = fa.STREET_ID.ToString(),
                StreetName = fa.STREET_NAME,
                StreetTypeID = fa.STREET_TYPE_ID.ToString(),
                StreetTypeName = fa.STREET_TYPE_NAME,
                BuildingID = fa.BLD_ID.ToString(),
                BuildingNumber = fa.BLD_NUMBER,
                BuildingIssue = fa.BLD_ISSUE,
                ApartmentNumber = apartmentNumber
            };
        }
        
        private async Task<bool> HasBirthdayEvent(Guid personId)
        {
            var personEvents = await _eventRepository.GetPersonEvents(personId);

            return personEvents.Where(p => p.EVT_NAME == "Привітання з днем народження").Count() > 0;
        }

        private void GetSetUserId()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Guid userGuid;

                //var user = _appUserParser.Parse(HttpContext.User);

                if (Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value ?? "", out userGuid))
                {
                    _reportRepository.UserID = userGuid;
                    _territoryRepository.UserID = userGuid;
                }
            }
        }
    }
}
