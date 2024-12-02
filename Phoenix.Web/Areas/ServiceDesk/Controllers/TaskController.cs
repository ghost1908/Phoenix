using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Identity.Entities;
using Phoenix.Infrastructure.Entities;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Areas.ServiceDesk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Areas.ServiceDesk.Controllers
{
    [Area("ServiceDesk")]
    public class TaskController : Controller
    {
        private readonly IServiceDeskRepository _serviceDeskRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskController(IServiceDeskRepository serviceDeskRepository, UserManager<ApplicationUser> userManager)
        {
            _serviceDeskRepository = serviceDeskRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            GetSetUserId();

            var tasks = await _serviceDeskRepository.GetTasks(HttpContext.User.IsInRole("administrators"));

            TaskListViewModel model = new TaskListViewModel();

            model.IsServiceDeskRole = HttpContext.User.IsInRole("ServiceDesk");

            // "созданные" задачи
            model.Created = tasks
                .Where(p => p.SEND_DATE.HasValue == false
                    && p.ACCEPT_DATE.HasValue == false
                    && p.DONE_DATE.HasValue == false
                    && p.CONFIRM_DATE.HasValue == false)
                .Select(s => new TaskViewModel
                {
                    Id = s.ID,
                    Number = s.NUMBER,
                    OwnerId = s.OWNER_ID,
                    OwnerName = GetDisplayName(s.OWNER_ID.ToString()),
                    PerformerId = s.PERFORMER_ID,
                    PerformerName = GetDisplayName(s.PERFORMER_ID.ToString()),
                    RequestText = s.REQUEST,
                    CreateDate = s.CREATE_DATE,
                    SendDate = s.SEND_DATE,
                    AcceptDate = s.ACCEPT_DATE,
                    DoneDate = s.DONE_DATE,
                    ConfirmDate = s.CONFIRM_DATE,
                    ResponseText = s.RESPONSE
                }).ToList();

            // "отправленные" задачи
            model.Sended = tasks
                .Where(p => p.SEND_DATE.HasValue == true
                    && p.ACCEPT_DATE.HasValue == false
                    && p.DONE_DATE.HasValue == false
                    && p.CONFIRM_DATE.HasValue == false)
                .Select(s => new TaskViewModel
                {
                    Id = s.ID,
                    Number = s.NUMBER,
                    OwnerId = s.OWNER_ID,
                    OwnerName = GetDisplayName(s.OWNER_ID.ToString()),
                    PerformerId = s.PERFORMER_ID,
                    PerformerName = GetDisplayName(s.PERFORMER_ID.ToString()),
                    RequestText = s.REQUEST,
                    CreateDate = s.CREATE_DATE,
                    SendDate = s.SEND_DATE,
                    AcceptDate = s.ACCEPT_DATE,
                    DoneDate = s.DONE_DATE,
                    ConfirmDate = s.CONFIRM_DATE,
                    ResponseText = s.RESPONSE
                }).ToList();

            // "в работе" задачи
            model.InWork = tasks
                .Where(p => p.SEND_DATE.HasValue == true
                    && p.ACCEPT_DATE.HasValue == true
                    && p.DONE_DATE.HasValue == false
                    && p.CONFIRM_DATE.HasValue == false)
                .Select(s => new TaskViewModel
                {
                    Id = s.ID,
                    Number = s.NUMBER,
                    OwnerId = s.OWNER_ID,
                    OwnerName = GetDisplayName(s.OWNER_ID.ToString()),
                    PerformerId = s.PERFORMER_ID,
                    PerformerName = GetDisplayName(s.PERFORMER_ID.ToString()),
                    RequestText = s.REQUEST,
                    CreateDate = s.CREATE_DATE,
                    SendDate = s.SEND_DATE,
                    AcceptDate = s.ACCEPT_DATE,
                    DoneDate = s.DONE_DATE,
                    ConfirmDate = s.CONFIRM_DATE,
                    ResponseText = s.RESPONSE
                }).ToList();

            // "выполненные" задачи
            model.Completed = tasks
                .Where(p => p.SEND_DATE.HasValue == true
                    && p.ACCEPT_DATE.HasValue == true
                    && p.DONE_DATE.HasValue == true
                    && p.CONFIRM_DATE.HasValue == false)
                .Select(s => new TaskViewModel
                {
                    Id = s.ID,
                    Number = s.NUMBER,
                    OwnerId = s.OWNER_ID,
                    OwnerName = GetDisplayName(s.OWNER_ID.ToString()),
                    PerformerId = s.PERFORMER_ID,
                    PerformerName = GetDisplayName(s.PERFORMER_ID.ToString()),
                    RequestText = s.REQUEST,
                    CreateDate = s.CREATE_DATE,
                    SendDate = s.SEND_DATE,
                    AcceptDate = s.ACCEPT_DATE,
                    DoneDate = s.DONE_DATE,
                    ConfirmDate = s.CONFIRM_DATE,
                    ResponseText = s.RESPONSE
                }).ToList();

            // "завершенные" задачи
            model.Confirmed = tasks
                .Where(p => p.SEND_DATE.HasValue == true
                    && p.ACCEPT_DATE.HasValue == true
                    && p.DONE_DATE.HasValue == true
                    && p.CONFIRM_DATE.HasValue == true)
                .Select(s => new TaskViewModel
                {
                    Id = s.ID,
                    Number = s.NUMBER,
                    OwnerId = s.OWNER_ID,
                    OwnerName = GetDisplayName(s.OWNER_ID.ToString()),
                    PerformerId = s.PERFORMER_ID,
                    PerformerName = GetDisplayName(s.PERFORMER_ID.ToString()),
                    RequestText = s.REQUEST,
                    CreateDate = s.CREATE_DATE,
                    SendDate = s.SEND_DATE,
                    AcceptDate = s.ACCEPT_DATE,
                    DoneDate = s.DONE_DATE,
                    ConfirmDate = s.CONFIRM_DATE,
                    ResponseText = s.RESPONSE
                }).ToList();

            return View(model);
        }

        public async Task<JsonResult> GetTask(string id)
        {
            GetSetUserId();

            TaskViewModel model = new TaskViewModel();

            if (string.IsNullOrWhiteSpace(id))
                return Json(new List<TaskViewModel>());

            var task = await _serviceDeskRepository.GetTask(id);

            if (task != null)
            {
                model = new TaskViewModel
                {
                    Id = task.ID,
                    Number = task.NUMBER,
                    OwnerId = task.OWNER_ID,
                    OwnerName = GetDisplayName(task.OWNER_ID.ToString()),
                    PerformerId = task.PERFORMER_ID,
                    PerformerName = GetDisplayName(task.PERFORMER_ID.ToString()),
                    RequestText = task.REQUEST,
                    CreateDate = task.CREATE_DATE,
                    SendDate = task.SEND_DATE,
                    AcceptDate = task.ACCEPT_DATE,
                    DoneDate = task.DONE_DATE,
                    ConfirmDate = task.CONFIRM_DATE,
                    ResponseText = task.RESPONSE
                };
            }
            return Json(model);
        }

        public async Task<JsonResult> EditTask([FromBody] TaskEditModel data)
        {
            GetSetUserId();

            TaskViewModel model;

            if (data.Type == "new")
            {
                var result = await _serviceDeskRepository.EditTaskState(new TaskObject
                {
                    ID = Guid.NewGuid(),
                    REQUEST = data.RequestText,
                    CREATE_DATE = DateTime.Now
                }, 0);
            }
            else if (data.Type == "edit")
            {
                var result = await _serviceDeskRepository.EditTaskState(new TaskObject
                {
                    ID = Guid.Parse(data.ID),
                    REQUEST = data.RequestText,
                    CREATE_DATE = DateTime.Now
                }, 1);
            }
            else if (data.Type == "send")
            {
                var result = await _serviceDeskRepository.EditTaskState(new TaskObject
                {
                    ID = Guid.Parse(data.ID),
                    REQUEST = data.RequestText,
                    SEND_DATE = DateTime.Now
                }, 2);
            }
            else if (data.Type == "delete")
            {
                var result = await _serviceDeskRepository.EditTaskState(new TaskObject
                {
                    ID = Guid.Parse(data.ID)
                }, 5);
            }

            return Json("OK");
        }

        [Authorize(Roles = "ServiceDesk")]
        [HttpPost]
        public async Task<JsonResult> AcceptTask(string taskId, string actionType)
        {
            GetSetUserId();

            TaskViewModel model;

            if (actionType == "accept")
            {
                var result = await _serviceDeskRepository.EditTaskState(new TaskObject
                {
                    ID = Guid.Parse(taskId),
                    ACCEPT_DATE = DateTime.Now
                }, 3);
            }
            else if (actionType=="decline")
            {
                var result = await _serviceDeskRepository.EditTaskState(new TaskObject
                {
                    ID = Guid.Parse(taskId),
                    SEND_DATE = (DateTime?)null
                }, 4);
            }

            return Json("OK");
        }

        private void GetSetUserId()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Guid userGuid;

                //var user = _appUserParser.Parse(HttpContext.User);

                if (Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value ?? "", out userGuid))
                {
                    _serviceDeskRepository.UserID = userGuid;
                }
            }
        }

        private string GetDisplayName(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return string.Empty;

            var user = _userManager.FindByIdAsync(userId).Result;
            if (user == null)
                return "Unknown UserId";

            var result = _userManager.GetClaimsAsync(user).Result;

            if (result.Count == 0)
                return user.Email;

            return result.FirstOrDefault(s => s.Type == "DisplayName").Value ?? user.Email;
        }
    }
}
