using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Infrastructure.Interfaces;
using Phoenix.Web.Models.Organization;
using Phoenix.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Areas.Party.Controllers
{
    [Authorize(Roles = "administrators,limited")]
    [Area("Party")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<IActionResult> Index()
        {
            GetSetUserId();

            var organization = await _organizationRepository.GetOrganizations();

            List<OrganizationListViewModel> model = organization.Select(async s => new OrganizationListViewModel
            {
                Id = s.ID.ToString(),
                Level = s.LEVEL,
                OrganizationName = s.ORG_NAME,
                CountPerson = await _organizationRepository.CountPersonInOrganization(s.ID),
            }).Select(t => t.Result).ToList();

            //List<OrganizationViewModel> model = organization.Select(s => new OrganizationViewModel
            //{
            //    Id = s.ID.ToString(),
            //    Level = s.LEVEL,
            //    OrganizationName = s.ORG_NAME,
            //}).ToList();

            //Dictionary<string, string[]> dic = model.ToDictionary(s => s.Level, s => s.Level.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries));
            //var root = dic.Where(p => p.Key == "/").Select(p => p.Key).FirstOrDefault();

            //TreeNode<OrganizationViewModel> tree = new TreeNode<OrganizationViewModel>(model.Where(p => p.Level == root).FirstOrDefault());

            //var maxLen = dic.Max(p => p.Value.Length);
            //for (var i = 1; i <= maxLen; i++)
            //{
            //    var nodes = model.Where(p => dic.Where(p => p.Value.Length == i).Select(p => p.Key).Contains(p.Level));
            //    foreach(var node in nodes)
            //    {
            //        string parentValue = node.Level.Substring(0, node.Level.Substring(0, node.Level.LastIndexOf("/")).LastIndexOf("/") + 1);
            //        var parent = tree.FindTreeNode(p => p.Data.Level == parentValue);
            //        if (parent == null)
            //            tree.AddChild(node);
            //        else
            //            parent.AddChild(node);
            //    }
            //}

            return View(model);
        }

        private void GetSetUserId()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Guid userGuid;

                //var user = _appUserParser.Parse(HttpContext.User);

                if (Guid.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))?.Value ?? "", out userGuid))
                    _organizationRepository.UserID = userGuid;
            }
        }
    }
}
