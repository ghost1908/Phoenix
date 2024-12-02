using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Identity.Entities;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Services;
using Phoenix.Web.Models.Territory;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Phoenix.Web.Controllers
{
    [Authorize(Roles = "administrators")]
    [Area("Party")]
    public class TerritoryController : Controller
    {
        private readonly ITerritoryRepository _territoryRepository;

        public TerritoryController(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }

        #region <---    AREA    --->

        public async Task<IActionResult> Area()
        {
            GetSetUserId();

            var areas = await _territoryRepository.GetAreas();

            IEnumerable<AreaListModel> model = areas.Select(s => new AreaListModel
            {
                ID = s.ID.ToString(),
                AreaName = s.NAME,
                RegionCount = _territoryRepository.GetRegionCountInArea(s.ID).Result,
                CommunityCount = _territoryRepository.GetCommunityCountInArea(s.ID).Result,
                CityCount = _territoryRepository.GetCityCountInArea(s.ID).Result,
                StreetCount = _territoryRepository.GetStreetCountInArea(s.ID).Result,
                PrecinctCount = _territoryRepository.GetPrecinctCountInArea(s.ID).Result
            });

            return View(model);
        }

        [Route("Territory/Area/Edit")]
        public IActionResult AreaEdit(string id)
        {
            return View();
        }

        public async Task<JsonResult> GetAreas()
        {
            GetSetUserId();

            var areas = await _territoryRepository.GetAreas();
            var model = areas.Select(s => new AreaViewModel
            {
                ID = s.ID.ToString(),
                AreaName = s.NAME
            });

            return Json(model);
        }

        #endregion

        #region <---    REGION    --->

        public async Task<IActionResult> Region(string areaId = null)
        {
            GetSetUserId();

            var areas = await _territoryRepository.GetRegions(areaId);

            IEnumerable<RegionListModel> model = areas.Select(s => new RegionListModel
            {
                ID = s.REG_ID.ToString(),
                RegionName = s.NAME,
                CommunityCount = _territoryRepository.GetCommunityCountInRegion(s.REG_ID).Result,
                CityCount = _territoryRepository.GetCityCountInRegion(s.REG_ID).Result,
                StreetCount = _territoryRepository.GetStreetCountInRegion(s.REG_ID).Result,
                PrecinctCount = _territoryRepository.GetPrecinctCountInRegion(s.REG_ID).Result
            }).ToList();

            foreach(var item in model)
            {
                var area = _territoryRepository.GetAreaFromRegion(item.ID).Result;
                item.AreaID = area.ID.ToString();
                item.AreaName = area.NAME;
            }

            return View(model);
        }

        public async Task<JsonResult> GetRegionInArea(string areaId)
        {
            GetSetUserId();

            if (string.IsNullOrWhiteSpace(areaId))
                return Json(new List<RegionViewModel>());

            var regions = await _territoryRepository.GetRegionInArea(areaId);
            var model = regions.Select(s => new RegionInAreaViewModel
            {
                ID = s.ID.ToString(),
                RegionID = s.REG_ID.ToString(),
                RegionName = _territoryRepository.GetRegion(s.REG_ID).Result.NAME,
                IsCityRegion = _territoryRepository.GetRegion(s.REG_ID).Result.IS_CITY_REGION
            }).OrderBy(o => o.RegionName);

            return Json(model);
        }

        #endregion

        #region <---    COMMUNITY    --->

        public async Task<IActionResult> Community(string areaId = null, string regId = null)
        {
            GetSetUserId();

            var communities = await _territoryRepository.GetCommunities(areaId, regId);

            IEnumerable<CommunityListModel> model = communities.Select(s => new CommunityListModel
            {
                ID = s.ID.ToString(),
                CommunityName = s.CommunityName,
                CommunityInRegionID = s.CommunityInRegionID.ToString(),
                RegionID = s.RegionID.ToString(),
                RegionName = s.RegionName,
                RegionInAreaID = s.RegionInAreaID.ToString(),
                AreaID = s.AreaID.ToString(),
                AreaName = s.AreaName,
                CityCount = _territoryRepository.GetCityCountInCommunity(s.ID).Result,
                StreetCount = _territoryRepository.GetStreetCountInCommunity(s.ID).Result,
                PrecinctCount = _territoryRepository.GetPrecinctCountInCommunity(s.ID).Result
            });

            return View(model);
        }

        public async Task<JsonResult> GetCommunityInRegion(string riaId)
        {
            GetSetUserId();

            if (string.IsNullOrWhiteSpace(riaId))
                return Json(new List<CommunityViewModel>());

            var communities = await _territoryRepository.GetCommunities(null, null, riaId);
            var model = communities.Select(s => new CommunityViewModel
            {
                ID = s.CommunityInRegionID.ToString(),
                CommunityName = s.CommunityName
            })
                .OrderBy(p => p.CommunityName);

            return Json(model);
        }

        [Route("Territory/Community/Create")]
        public IActionResult CommunityCreate(string riaId, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (Url.IsLocalUrl(returnUrl))
            {
                int iqs = returnUrl.IndexOf('?');
                if (iqs >= 0)
                {
                    string queryString = (iqs < returnUrl.Length - 1) ? returnUrl.Substring(iqs + 1) : string.Empty;
                    ViewData["AreaId"] = HttpUtility.ParseQueryString(queryString).Get("areaId");
                    ViewData["RegId"] = HttpUtility.ParseQueryString(queryString).Get("regId");
                }
            }
            return View();
        }

        [HttpPost]
        [Route("Territory/Community/Create")]
        public async Task<IActionResult> CommunityCreate(CommunityEditModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _territoryRepository.CreateCommunity(Guid.Parse(model.RegionInAreaID), model.CommunityName);

                if (result == "OK")
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result);
                    return View(model);
                }
            }

            return View(model);
        }

        #endregion

        #region <---    CITY    --->

        public async Task<IActionResult> City(string areaId = null, string regId = null, string cmnId = null)
        {
            GetSetUserId();

            var cities = await _territoryRepository.GetCities(areaId, regId, cmnId);

            IEnumerable<CityListModel> model = cities.Select(s => new CityListModel
            {
                ID = s.CityID.ToString(),
                CityName = s.CityTypeName +s.CityName,
                CityInCommunityID = s.ID.ToString(),
                CommunityID = s.CommunityID.ToString(),
                CommunityName = s.CommunityName,
                CommunityInRegionID = s.CommunityInRegionID.ToString(),
                RegionID = s.RegionID.ToString(),
                RegionName = s.RegionName,
                RegionInAreaID = s.RegionInAreaID.ToString(),
                AreaID = s.AreaID.ToString(),
                AreaName = s.AreaName,
                StreetCount = _territoryRepository.GetStreetCountInCity(s.ID).Result
                //PrecinctCount = _territoryRepository.GetPrecinctCountInCity(s.CityID).Result
            });

            return View(model);
        }

        [Route("Territory/City/Edit")]
        public async Task<IActionResult> CityEdit(string cicId)
        {
            CityEditModel model = new CityEditModel();

            if (string.IsNullOrWhiteSpace(cicId))
                return BadRequest();

            var city = await _territoryRepository.GetCity(Guid.Parse(cicId));
            var cir = await _territoryRepository.GetCommunityInRegionById(city.CIR_ID);
            var ria = await _territoryRepository.GetRegionInAreaById(cir.RIA_ID);

            model.CityInCommunityID = city.ID;
            model.CityID = city.CITY_ID;
            model.CityName = city.CITY_NAME;
            model.CityTypeName = city.CITYT_NAME;
            model.CityTypeID = city.CITYT_ID;
            model.CommunityInRegionID = cir.ID;
            model.RegionInAreaID = cir.RIA_ID;
            model.AreaId = ria.AREA_ID;

            var cityTypes = await _territoryRepository.GetCityTypes();
            model.CityTypes = cityTypes.Select(s => new SelectListItem()
            {
                Value = s.ID.ToString(),
                Text = s.NAME
            }).ToList();

            var streets = await _territoryRepository.GetStreetInCity(city.ID);
            var terDescription = await _territoryRepository.GetTerritoryDescription(streets.Select(s => s.ID).ToArray());
            var precincts = await _territoryRepository.GetPrecincts(null, null, null);

            var territotyDescription = from td in terDescription
                                       join str in streets on td.SIC_ID equals str.ID
                                       join prct in precincts on td.PRCT_ID equals prct.ID
                                       orderby prct.PRCT_NUMBER
                                       select new TerritoryDescriptionViewModel()
                                       {
                                           PrecinctId = td.PRCT_ID,
                                           PrecinctNumber = prct.PRCT_NUMBER,
                                           StreetInCityID = str.ID,
                                           StreetID = str.STR_ID,
                                           StreetName = str.STRT_NAME + str.STR_NAME,
                                           BuildingStart = td.BLD_START,
                                           BuildingEnd = td.BLD_END
                                       };
            model.Streets = territotyDescription.ToList();

            model.PrecinctNumbers = precincts.Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.PRCT_NUMBER
            }).OrderBy(o => o.Text).ToList();
            model.PrecinctNumbers.Insert(0, new SelectListItem { Value = "", Text = "Виберіть дільницю", Disabled = true, Selected = true });

            var streetNames = await _territoryRepository.GetStreetNames();
            model.StreetNames = streetNames.Select(s => new SelectListItem
            {
                Value = s.ID.ToString(),
                Text = s.STRT_NAME + s.STR_NAME
            }).OrderBy(o => o.Text).ToList();
            model.StreetNames.Insert(0, new SelectListItem { Value = "", Text = "Виберіть вулицю", Disabled = true, Selected = true });

            return View(model);
        }

        #endregion

        #region <---    STREET    --->

        public async Task<IActionResult> Street()
        {
            GetSetUserId();

            var streets = await _territoryRepository.GetStreetNames();

            

            return View();
        }

        #endregion

        private void GetSetUserId()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Guid userGuid;

                if (Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value ?? "", out userGuid))
                    _territoryRepository.UserID = userGuid;
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}