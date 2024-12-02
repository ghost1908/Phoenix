using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phoenix.Identity.Entities;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Models.Admin;
using Phoenix.Web.Models.Territory;

namespace Phoenix.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "administrators")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAdminRepository _adminRepository;
        private readonly ITerritoryRepository _territoryRepository;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IAdminRepository adminRepository, ITerritoryRepository territoryRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _adminRepository = adminRepository;
            _territoryRepository = territoryRepository;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.Select(s => new UserViewModel
            {
                Id = s.Id,
                UserName = s.UserName,
                Email = s.Email,
                //DisplayName = GetClaimValue(s, "DisplayName"),
                Accesses = string.Join("; ", _adminRepository.GetUserAccessNames(s.Id.ToString()).Result)
            }).ToList();

            foreach(var user in users)
            {
                user.DisplayName = GetClaimValue(user.Email, "DisplayName");
            }
            
            return View(users);
        }

        public async Task<IActionResult> CreateUser()
        {
            UserCreateModel model = new UserCreateModel();

            var objects = await _adminRepository.GetObjectAccesses();

            model.Objects = objects.Select(s => new ObjectAccessViewModel
            {
                ObjectId = s.Id.ToString(),
                ObjectName = s.ObjectName,
                ObjectDisplayName = s.ObjectDisplayName,
                ObjectType = s.ObjectType
            });

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateModel model)
        {
            IPasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            var userModel = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };
            userModel.PasswordHash = passwordHasher.HashPassword(userModel, model.Password);

            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                _userManager.CreateAsync(userModel).Wait();

            // добавить Claim
            user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("DisplayName", model.DisplayName));
                _userManager.AddClaimsAsync(user, claims).Wait();
            }

            // добавить уровень доступа
            var access = await _adminRepository.AddUserAccess(user.Id, Guid.Parse(model.Object), model.TableValues);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            UserEditModel model = new UserEditModel();

            var user = await _userManager.FindByIdAsync(id);

            model.Id = user.Id;
            model.UserName = user.UserName;
            model.DisplayName = GetClaimValue(user.Email, "DisplayName");

            return View(model);
        }


        public async Task<JsonResult> GetTableData(string tableName)
        {
            var data = await _adminRepository.GetTableDatas(tableName);

            return Json(data);
        }

        private string GetClaimValue(string userName, string claimName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;
            if (user == null)
                return string.Empty;

            var result = _userManager.GetClaimsAsync(user).Result;

            if (result.Count == 0)
                return string.Empty;

            return result.FirstOrDefault(s => s.Type == claimName).Value ?? string.Empty;
        }
    }
}
