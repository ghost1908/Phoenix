using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phoenix.Web.Models.Territory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Admin
{
    public class UserEditModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }

        [BindProperty]
        public string Object { get; set; }

        [BindProperty]
        public string TableValues { get; set; }

        public IEnumerable<ObjectAccessViewModel> Objects { get; set; }
    }
}
