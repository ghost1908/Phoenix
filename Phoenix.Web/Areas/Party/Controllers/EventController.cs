using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phoenix.Web.Models.Event;

namespace Phoenix.Web.Areas.Party.Controllers
{
    [Authorize(Roles = "administrators")]
    [Area("Party")]
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [Route("Party/Event/Projects")]
        public async Task<IActionResult> ProjectList()
        {
            //List<ProjectViewModel> model = new List<ProjectViewModel>();

            var projects = await _eventRepository.GetProjects();
            var model = projects.Select(async s => new ProjectViewModel
            {
                ProjectId = s.ID,
                ProjectName = s.PRJT_NAME,
                Events = (await _eventRepository.GetProjectEvents(s.ID)).Select(se => new ProjectEventViewModel
                {
                    Id = se.ID,
                    EventName = se.EVT_NAME,
                    EventType = se.EVT_TYPE == false ? PROJECT_EVENT_TYPE.Touch : PROJECT_EVENT_TYPE.Event,
                    EventAccess = (PROJECT_EVENT_ACCESS)se.EVT_ACCESS,
                    EventStart = se.EVT_START,
                    EventEnd = se.EVT_END,
                    OrganizationId = se.ORG_ID,
                    OrganizationName = string.Empty
                }).ToList()
            }).Select(t => t.Result).ToList();

            return View(model);
        }
    }
}
