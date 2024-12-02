using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Phoenix.Identity.Entities;
using Phoenix.Infrastructure.Entities;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Models;
using Phoenix.Web.Models.Organization;
using Phoenix.Web.Models.Person;
using Phoenix.Web.Models.Party;
using Phoenix.Web.Models.Territory;
using Phoenix.Web.Services;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Net;
using Phoenix.Web.Models.Event;
using Microsoft.AspNetCore.Identity;

namespace Phoenix.Web.Controllers
{
    [Authorize]
    [Area("Party")]
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ITerritoryRepository _territoryRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IPartyRepository _partyRepository;
        //private readonly IIdentityParser<ApplicationUser> _appUserParser;
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonController(IPersonRepository personRepository, 
            IOrganizationRepository organizationRepository, 
            ITerritoryRepository territoryRepository, 
            IEventRepository eventRepository,
            IPartyRepository partyRepository,
            //IIdentityParser<ApplicationUser> appUserParser,
            UserManager<ApplicationUser> userManager)
        {
            _personRepository = personRepository;
            _organizationRepository = organizationRepository;
            _territoryRepository = territoryRepository;
            _eventRepository = eventRepository;
            _partyRepository = partyRepository;
            //_appUserParser = appUserParser;
            _userManager = userManager;
        }

        public IActionResult List()
        {
            return View();
        }

        public async Task<JsonResult> FilterList([FromBody]PersonFilterModel filter)
        {
            PersonListViewModel model = new PersonListViewModel();

            GetSetUserId();

            if (filter != null)
                model.Filter = filter;

            //model.ItemsPerPage = ItemsPerPage;

            model.PaginationInfo.ActualPage = filter.Page;
            model.PaginationInfo.ItemsPerPage = filter.ItemsPerPage;

            var people = await _personRepository.GetPeopleList(new PersonFilter()
            {
                AreaId = (string.IsNullOrWhiteSpace(filter.AreaId) || filter.AreaId == "null") ? new Nullable<Guid>() : Guid.Parse(filter.AreaId),
                RegionId = (string.IsNullOrWhiteSpace(filter.RegionId) || filter.RegionId == "null") ? new Nullable<Guid>() : Guid.Parse(filter.RegionId),
                CommunityId = (string.IsNullOrWhiteSpace(filter.CommunityId) || filter.CommunityId == "null") ? new Nullable<Guid>() : Guid.Parse(filter.CommunityId),
                HasTelegram = filter.HasTelegram == "null" ? new Nullable<bool>() : filter.HasTelegram == "1" ? true : false,
                HasViber = filter.HasViber == "null" ? new Nullable<bool>() : filter.HasViber == "1" ? true : false,
                HasWhatsapp = filter.HasWhatsapp == "null" ? new Nullable<bool>() : filter.HasWhatsapp == "1" ? true : false,
                HasFacebook = filter.HasFacebook == "null" ? new Nullable<bool>() : filter.HasFacebook == "1" ? true : false,
                HasInstagram = filter.HasInstagram == "null" ? new Nullable<bool>() : filter.HasInstagram == "1" ? true : false,
                IsEmployee = filter.IsEmployee == "null" ? new Nullable<bool>() : filter.IsEmployee == "1" ? true : false,
                PositionId = filter.PositionId == "null" ? new Nullable<Guid>() : Guid.Parse(filter.PositionId),
                IsDeputy = filter.IsDeputy == "null" ? new Nullable<bool>() : filter.IsDeputy == "1" ? true : false,
                IsPartyMember = filter.IsPartyMember == "null" ? new Nullable<bool>() : filter.IsPartyMember == "1" ? true : false,
                IsDeleted = filter.IsDeleted == "null" ? new Nullable<bool>() : filter.IsDeleted == "1" ? true : false
            },
                filter.ItemsPerPage * filter.Page, filter.ItemsPerPage);

            model.PaginationInfo.TotalItems = (int)people.TotalCount;
            model.PaginationInfo.TotalPages = (int)Math.Ceiling((decimal)model.PaginationInfo.TotalItems / (filter.ItemsPerPage == 0 ? (int)people.TotalCount : filter.ItemsPerPage));
            model.PaginationInfo.Next = (model.PaginationInfo.ActualPage == model.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            model.PaginationInfo.Previous = (model.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            model.Persons = people.Persons
                .Select(async s => new PersonViewModel
                {
                    PersonId = s.ID,
                    LastName = s.LNAME,
                    FirstName = s.FNAME,
                    MiddleName = s.MNAME,
                    Gender = s.GENDER.Value ? GENDER.Male : GENDER.Female,
                    BirthDate = s.BDATE,
                    AddressRegistration = await GetFullAddress(s.ADDR_REG ?? Guid.Empty, s.ADDR_REG_ROOM),
                    Phone = s.PHONE,
                    HasTelegram = s.HAS_TELEGRAM,
                    HasViber = s.HAS_VIBER,
                    HasWhatsapp = s.HAS_WHATSAPP,
                    Email = s.EMAIL,
                    HasFacebook = s.HAS_FACEBOOK,
                    HasInstagram = s.HAS_INSTAGRAM,
                    IsEmployee = s.IS_EMPLOYEE,
                    IsDeputy = s.IS_DEPUTY,
                    IsPartyMember = s.IS_PARTY_MEMBER,
                    IsDeleted = s.IS_DELETED,
                    CreateDate = s.DCREATE,
                    LastStatus = await _eventRepository.GetPersonLastEventStatus(s.ID),
                    LastFormStatus = await _personRepository.GetLastFormStatus(s.ID)
                })
                .Select(t => t.Result).ToList();

            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            GetSetUserId();

            if (string.IsNullOrWhiteSpace(id))
                return View(new PersonViewModel());

            var person = await _personRepository.GetPerson(Guid.Parse(id));

            PersonViewModel model = new PersonViewModel
            {
                PersonId = person.ID,
                LastName = person.LNAME,
                FirstName = person.FNAME,
                MiddleName = person.MNAME,
                Gender = person.GENDER.Value == true ? GENDER.Male : GENDER.Female,
                BirthDate = person.BDATE,
                Vote = person.VOTE,
                AddressRegistration = await GetFullAddress(person.ADDR_REG ?? Guid.Empty, person.ADDR_REG_ROOM),
                AddressLiving = await GetFullAddress(person.ADDR_HOME ?? Guid.Empty, person.ADDR_HOME_ROOM),
                Phone = person.PHONE,
                HasTelegram = person.HAS_TELEGRAM,
                HasViber = person.HAS_VIBER,
                HasWhatsapp = person.HAS_WHATSAPP,
                Email = person.EMAIL,
                HasFacebook = person.HAS_FACEBOOK,
                HasInstagram = person.HAS_INSTAGRAM,
                IsEmployee = person.IS_EMPLOYEE,
                IsPartyMember = person.IS_PARTY_MEMBER,
                IsDeputy = person.IS_DEPUTY,
                IsDeleted = person.IS_DELETED,
                LastFormStatus = await _personRepository.GetLastFormStatus(person.ID)
            };

            var personFormStatuses = await _personRepository.GetPersonFormStatuses(model.PersonId);
            model.FormStatuses = personFormStatuses.Select(s => new PersonFormStatusViewModel()
            {
                Id = s.ID,
                FormStatus = s.STATUS,
                CreateDate = s.CREATE_DATE,
                UserId = s.USER_ID,
                UserName = GetUserDisplayName(s.USER_ID),
                Comment = s.COMMENT,
                Action = s.USER_ID == _userManager.FindByNameAsync(User.Identity.Name).Result.Id ? TABLE_ROW_ACTION.None : TABLE_ROW_ACTION.Disbaled
            }).ToList();

            var personInfo = await _personRepository.GetPersonInfo(model.PersonId);

            model.PersonInfo = new PersonInfoViewModel
            {
                Passport = new Passport
                {
                    Series = personInfo.PASS_SERIES,
                    Number = personInfo.PASS_NUMBER,
                    Issue = personInfo.PASS_ISSUE
                },
                TaxNumber = personInfo.TAX_NUMBER
            };

            var personPositions = await _organizationRepository.GetPersonPositions(Guid.Parse(id));
            model.PersonPositions = personPositions.Select(s => new PersonPositionViewModel
            {
                Id = s.ID.ToString(),
                OrganizationId = s.ORG_ID.ToString(),
                OrganizationName = s.ORG_NAME,
                PositionId = s.POS_ID.ToString(),
                PositionName = s.POS_NAME,
                AppointDate = s.APPOINT_DATE,
                DismissDate = s.DISMISS_DATE,
                Action = TABLE_ROW_ACTION.Update
            }).ToList();

            var personParty = await _partyRepository.GetPersonPartyInfo(Guid.Parse(id));
            model.PersonParties = personParty.Select(s => new PersonPartyViewModel
            {
                Id = s.ID,
                PersonId = s.PSN_ID,
                EntryDate = s.DATE_ENTRY,
                AdoptionDate = s.DATE_ADOPTION,
                AdoptionNumber = s.ADOPTION_NUMBER,
                AdoptionComment = s.ADOPTION_COMMENT,
                DisposalDate = s.DATE_DISPOSAL,
                DisposalNumber = s.DISPOSAL_NUMBER,
                DisposalCause = (PARTY_DISPOSAL_CAUSE?)s.DISPOSAL_CAUSE,
                DisposalComment = s.DISPOSAL_COMMENT,
                TicketNumber = s.TICKET_NUMBER,
                Action = TABLE_ROW_ACTION.None
            }).ToList();

            var personEvents = await _eventRepository.GetPersonEvents(Guid.Parse(id));
            model.PersonEvents = personEvents.Select(s => new PersonEventViewModel
            {
                Id = s.ID,
                ProjectId = s.PRJT_ID,
                ProjectName = s.PRJT_NAME,
                EventId = s.EVT_ID,
                EventName = s.EVT_NAME,
                EventType = s.EVT_TYPE ? PROJECT_EVENT_TYPE.Event : PROJECT_EVENT_TYPE.Touch,
                EventDate = s.REG_DATE,
                EventComment = s.EVT_COMMENT,
                PersonStatus = (PERSON_EVENT_STATUS)s.PSN_STATUS,
                Action = TABLE_ROW_ACTION.None,
                UserName = GetUserDisplayName(s.USER_ID),
                EventCreate = s.EVT_CREATE.HasValue ? s.EVT_CREATE.Value : DateTime.MinValue
            }).ToList();

            ViewBag.UserId = _personRepository.UserID;

            return View(model);
        }

        public async Task<JsonResult> EditPerson([FromBody] PersonEditModel data)
        {
            Person resultPerson;

            try
            {
                GetSetUserId();

                // проверка даты касаний
                if (data.Events.Where(p=> DateTime.ParseExact(p.EventDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture).Date > DateTime.Now.Date).Count() > 0)
                {
                    throw new Exception("Дата дотика не може бути більше ніж сьогоднішня.");
                }
                foreach (var evt in data.Events.Where(p => string.IsNullOrWhiteSpace(p.Action) || p.Action == "0" || p.Action == "1"))
                {
                    DateTime checkDate;
                    if(DateTime.TryParseExact(evt.EventDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out checkDate))
                    {
                        if(checkDate.Month!=DateTime.Now.Month || checkDate.Date<DateTime.Now.AddDays(-7).Date)
                        {
                            throw new Exception("Дата дотика не може відрізнятися від сьогоднішньої більше 7 днів.");
                        }
                    }
                }

                // проверка guid адресов: если не парсятся - null
                Guid addrRegId, addrLivId;
                if (!Guid.TryParse(data.RegistrationID, out addrRegId))
                    data.RegistrationID = null;
                if (!Guid.TryParse(data.LivingID, out addrLivId))
                    data.LivingID = null;

                // если нет id - создаем анкету
                if (string.IsNullOrWhiteSpace(data.Id) || Guid.Empty.ToString() == data.Id)
                {
                    resultPerson = await _personRepository.CreatePerson(new Person
                    {
                        LNAME = data.LastName,
                        FNAME = data.FirstName,
                        MNAME = data.MiddleName,
                        GENDER = data.Gender == 0 ? false : true,
                        BDATE = data.BirthDate,
                        VOTE = false,
                        ADDR_REG = string.IsNullOrWhiteSpace(data.RegistrationID) ? new Nullable<Guid>() : addrRegId,
                        ADDR_REG_ROOM = data.RegistrationApartment,
                        ADDR_HOME = string.IsNullOrWhiteSpace(data.LivingID) ? new Nullable<Guid>() : addrLivId,
                        ADDR_HOME_ROOM = data.LivingApartment,
                        PHONE = (data.Phone == "+380" ? null : data.Phone),
                        HAS_TELEGRAM = data.HasTelegram,
                        HAS_VIBER = data.HasViber,
                        HAS_WHATSAPP = data.HasWhatsapp,
                        EMAIL = data.Email,
                        HAS_FACEBOOK = data.HasFacebook,
                        HAS_INSTAGRAM = data.HasInstagram,
                        IS_EMPLOYEE = data.IsEmployee,
                        IS_DEPUTY = data.IsDeputy,
                        IS_PARTY_MEMBER = data.IsPartyMember,
                        IS_DELETED = data.IsDeleted
                    });
                    // TODO: доп.информация и т.д.
                }
                else
                {
                    resultPerson = await _personRepository.UpdatePerson(new Person
                    {
                        ID = Guid.Parse(data.Id),
                        LNAME = data.LastName,
                        FNAME = data.FirstName,
                        MNAME = data.MiddleName,
                        GENDER = data.Gender == 0 ? false : true,
                        BDATE = data.BirthDate,
                        VOTE = false,
                        ADDR_REG = string.IsNullOrWhiteSpace(data.RegistrationID) ? new Nullable<Guid>() : addrRegId,
                        ADDR_REG_ROOM = data.RegistrationApartment,
                        ADDR_HOME = string.IsNullOrWhiteSpace(data.LivingID) ? new Nullable<Guid>() : addrLivId,
                        ADDR_HOME_ROOM = data.LivingApartment,
                        PHONE = (data.Phone == "+380" ? null : data.Phone),
                        HAS_TELEGRAM = data.HasTelegram,
                        HAS_VIBER = data.HasViber,
                        HAS_WHATSAPP = data.HasWhatsapp,
                        EMAIL = data.Email,
                        HAS_FACEBOOK = data.HasFacebook,
                        HAS_INSTAGRAM = data.HasInstagram,
                        IS_EMPLOYEE = data.IsEmployee,
                        IS_DEPUTY = data.IsDeputy,
                        IS_PARTY_MEMBER = data.IsPartyMember,
                        IS_DELETED = data.IsDeleted
                    });
                    // TODO: доп.информация и т.д.
                }

                // создание или обновление статусов анкет
                var resultFormStatuses = await _personRepository.CreateOrUpdateFormStatuses(resultPerson.ID,
                                        data.FormStatuses.Select(s => new PersonFormStatus
                                        {
                                            ID = string.IsNullOrWhiteSpace(s.Id) ? Guid.Empty : Guid.Parse(s.Id),
                                            STATUS = byte.Parse(s.FormStatus),
                                            USER_ID = Guid.Parse(s.UserId),
                                            CREATE_DATE = DateTime.ParseExact(s.CreateDate, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                                            COMMENT = s.Comment,
                                            ACTION = string.IsNullOrWhiteSpace(s.Action) ? 0 : int.Parse(s.Action)
                                        }).ToList());

                // обновление доп.информации
                var resultPersonInfo = await _personRepository.UpdatePersonInfo(new PersonInfo
                {
                    PSN_ID = resultPerson.ID,
                    PASS_SERIES = data.PersonInfo.PassportSeries,
                    PASS_NUMBER = data.PersonInfo.PassportNumber,
                    PASS_ISSUE = data.PersonInfo.PassportIssue,
                    TAX_NUMBER = data.PersonInfo.TaxNumber
                });

                // создание или обновление должностей
                var resultPositions = await _organizationRepository.CreateOrUpdatePersonPositions(resultPerson.ID,
                                        data.Positions.Select(s => new PositionInOrganization
                                        {
                                            ID = string.IsNullOrWhiteSpace(s.Id) ? Guid.Empty : Guid.Parse(s.Id),
                                            ORG_ID = Guid.Parse(s.OrganizationId),
                                            POS_ID = Guid.Parse(s.PositionId),
                                            APPOINT_DATE = string.IsNullOrWhiteSpace(s.AppointDate) ? (DateTime?)null : DateTime.ParseExact(s.AppointDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                                            DISMISS_DATE = string.IsNullOrWhiteSpace(s.DismissDate) ? (DateTime?)null : DateTime.ParseExact(s.DismissDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                                            ACTION = string.IsNullOrWhiteSpace(s.Action) ? 0 : int.Parse(s.Action)
                                        }).ToList());

                // создание или обновление событий (касаний, мероприятий)
                var resultEvents = await _eventRepository.CreateOrUpdatePersonEvents(resultPerson.ID,
                                        data.Events.Select(s => new PersonEvent
                                        {
                                            ID = string.IsNullOrWhiteSpace(s.Id) ? Guid.Empty : Guid.Parse(s.Id),
                                            PRE_ID = Guid.Parse(s.EventId),
                                            REG_DATE = DateTime.ParseExact(s.EventDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                                            PSN_STATUS = byte.Parse(s.PersonStatus),
                                            ACTION = string.IsNullOrWhiteSpace(s.Action) ? 0 : int.Parse(s.Action),
                                            EVT_COMMENT = s.EventComment
                                        }).ToList());

                // создание или обновление партийной информации
                var resultParties = await _partyRepository.CreateOrUpdatePersonPartyInfo(resultPerson.ID,
                                        data.Parties.Select(s => new PersonParty
                                        {
                                            ID = string.IsNullOrWhiteSpace(s.Id) ? Guid.Empty : Guid.Parse(s.Id),
                                            TICKET_NUMBER = s.TicketNumber,
                                            DATE_ENTRY = DateTime.ParseExact(s.EntryDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                                            DATE_ADOPTION = DateTime.ParseExact(s.AdoptionDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                                            ADOPTION_NUMBER = s.AdoptionNumber,
                                            ADOPTION_COMMENT = s.AdoptionComment,
                                            DATE_DISPOSAL = DateTime.ParseExact(s.DisposalDate, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                                            DISPOSAL_NUMBER = s.DisposalNumber,
                                            DISPOSAL_CAUSE = string.IsNullOrWhiteSpace(s.DisposalCause) ? (byte?)null : byte.Parse(s.DisposalCause),
                                            DISPOSAL_COMMENT = s.DisposalComment,
                                            ACTION = string.IsNullOrWhiteSpace(s.Action) ? (byte)0 : byte.Parse(s.Action)
                                        }).ToList());

                return Json("OK");
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(e.Message);
            }
            
        }

        public async Task<JsonResult> FindDublicates(string lastName,string firstName)
        {
            // если Фамилия или Имя - пустые значения или длина менее 3-х символов
            if (string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(firstName)
                || lastName.Length < 3 || firstName.Length < 3)
                return Json(new List<PersonDublicateViewModel>());

            var people = await _personRepository.FindDublicates(lastName, firstName);

            List<PersonDublicateViewModel> model = people.Select(async s => new PersonDublicateViewModel
            {
                Id = s.ID.ToString(),
                LastName = s.LNAME,
                FirstName = s.FNAME,
                MiddleName = s.MNAME,
                BirthDate = s.BDATE.HasValue ? s.BDATE.Value.ToString("dd.MM.yyyy") : "-",
                AddressRegistration = (await GetFullAddress(s.ADDR_REG.Value, s.ADDR_REG_ROOM)).ToString()
            }).Select(t => t.Result).ToList();

            return Json(model);
        }

        public async Task<JsonResult> GetOrganizations()
        {
            GetSetUserId();

            var organization = await _organizationRepository.GetOrganizations();

            List<OrganizationViewModel> model = organization.Select(s => new OrganizationViewModel
            {
                Id = s.ID.ToString(),
                Level = s.LEVEL,
                OrganizationName = s.ORG_NAME
            }).ToList();

            return Json(model);
        }

        public async Task<JsonResult> GetPositions()
        {
            GetSetUserId();

            var positions = await _organizationRepository.GetPositions();

            List<PositionViewModel> model = positions.Select(s => new PositionViewModel
            {
                Id = s.ID.ToString(),
                PositionName = s.POS_NAME
            }).ToList();

            return Json(model);
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

        public async Task<JsonResult> GetRegionInArea(string areaId)
        {
            GetSetUserId();

            if (string.IsNullOrWhiteSpace(areaId))
                return Json(new List<RegionViewModel>());

            var regions = await _territoryRepository.GetRegions(areaId);
            var model = regions.Select(s => new RegionViewModel
            {
                ID = s.ID.ToString(),
                RegionName = s.NAME
            });

            return Json(model);
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

        public async Task<JsonResult> GetCityInCommunity(string cmnId)
        {
            GetSetUserId();

            if (string.IsNullOrWhiteSpace(cmnId))
                return Json(new List<CityInCommunityViewModel>());

            var cities = await _territoryRepository.GetCities(null, null, cmnId);
            var model = cities.Select(s => new CityInCommunityViewModel
            {
                ID = s.ID.ToString(),
                CityName = (s.CityTypeName.EndsWith('.') ? s.CityTypeName : s.CityTypeName + " ") + s.CityName
            })
                .OrderBy(p => p.CityName);

            return Json(model);
        }

        public async Task<JsonResult> GetStreetInCity(string cityId)
        {
            GetSetUserId();

            if (string.IsNullOrWhiteSpace(cityId))
                return Json(new List<StreetInCityViewModel>());

            var streets = await _territoryRepository.GetStreetInCity(Guid.Parse(cityId));
            var model = streets.Select(s => new StreetInCityViewModel
            {
                ID = s.ID.ToString(),
                StreetName = (s.STRT_NAME.EndsWith('.') ? s.STRT_NAME : s.STRT_NAME + " ") + s.STR_NAME
            })
                .OrderBy(p => p.StreetName);

            return Json(model);
        }

        public async Task<JsonResult> GetFullAddress(string id, string appartment)
        {
            var model = await GetFullAddress(Guid.Parse(id), appartment);

            return Json(model);
        }

        public async Task<JsonResult> GetAddressIdByStreet(string streetId, string buildingNumber)
        {
            Guid street;

            if (!Guid.TryParse(streetId, out street))
                return new JsonResult("Bad id.") { StatusCode = 400 };

            var id = await _territoryRepository.GetSetAddressByStreet(Guid.Parse(streetId), buildingNumber.Trim());

            return Json(id);
        }

        #region <--    EVENTS    -->

        public async Task<JsonResult> GetProjects(int personMask)
        {
            GetSetUserId();

            var projects = await _eventRepository.GetProjects(personMask);

            List<ProjectViewModel> model = projects.Select(s => new ProjectViewModel
            {
                ProjectId = s.ID,
                ProjectName = s.PRJT_NAME
            }).ToList();

            return Json(model);
        }

        public async Task<JsonResult> GetEvents(string projectId, int personMask)
        {
            GetSetUserId();
            Guid prjtId;

            if (!Guid.TryParse(projectId, out prjtId))
                return Json(new List<ProjectEventViewModel>());

            var events = await _eventRepository.GetProjectEvents(prjtId, personMask);

            List<ProjectEventViewModel> model = events.Select(s => new ProjectEventViewModel
            {
                Id = s.ID,
                EventName = s.EVT_NAME,
                EventType = s.EVT_TYPE ? PROJECT_EVENT_TYPE.Event : PROJECT_EVENT_TYPE.Touch
            }).ToList();

            return Json(model);
        }

        #endregion

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
                CommunityInRegionID=fa.CMN_RIA_ID.ToString(),
                CommunityID=fa.CMN_ID.ToString(),
                CommunityName=fa.CMN_NAME,
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

        private string GetUserDisplayName(Guid userId)
        {
            var user = _userManager.FindByIdAsync(userId.ToString()).Result;

            if (user == null)
                return "Невідомо";

            var claims = _userManager.GetClaimsAsync(user).Result;

            if (claims.Count == 0)
                return user.UserName;

            return claims.FirstOrDefault(s => s.Type == "DisplayName").Value ?? user.UserName;
        }

        private void GetSetUserId()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Guid userGuid;

                //var user = _appUserParser.Parse(HttpContext.User);

                if (Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value ?? "", out userGuid))
                {
                    _personRepository.UserID = userGuid;
                    _territoryRepository.UserID = userGuid;
                    _eventRepository.UserID = userGuid;
                    _partyRepository.UserID = userGuid;
                    //_organizationRepository.UserID = userGuid;
                }
            }
        }
    }
}