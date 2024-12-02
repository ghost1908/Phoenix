using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Phoenix.Identity.Entities;
using Phoenix.Web.Models.AccountViewModel;
using System.Threading.Tasks;

namespace Phoenix.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();

            //CheckOrCreateDefaultUserAndRoles();
            //GenerateUsers();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");
                    return RedirectToLocal(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    
                }
                if (result.IsLockedOut)
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    
                    if (user != null)
                    {
                        if (user.LockoutEnd.HasValue)
                        {
                            ModelState.AddModelError(string.Empty, string.Format("Ваш аккаунт тимчасово заблоковано.\nБлокування буде знято {0:dd.MM.yyyy HH:mm:ss}", user.LockoutEnd.Value.LocalDateTime));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Ваш аккаунт заблоковано.");
                        }
                        ViewData["LockoutEndDateTime"] = user.LockoutEnd.HasValue ? user.LockoutEnd.Value.LocalDateTime.ToString("dd.MM.yyyy HH:mm") : string.Empty;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ваш аккаунт заблоковано.");
                        ViewData["LockoutEndDateTime"] = string.Empty;
                    }
                    //return View("Lockout");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Невірний логін або пароль.");
                    var user = await _userManager.FindByNameAsync(model.Email);
                    if (user != null)
                        await _userManager.AccessFailedAsync(user);
                    
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
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