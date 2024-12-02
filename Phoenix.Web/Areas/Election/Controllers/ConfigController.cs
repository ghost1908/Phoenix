using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Phoenix.Web.Areas.Election.Config.Controllers
{
    [Area("Election")]
    public class ConfigController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}