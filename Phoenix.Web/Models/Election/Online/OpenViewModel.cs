using System.Collections.Generic;

namespace Phoenix.Web.Models.Election.Online
{
    public class OpenViewModel
    {
        public List<OpenItemViewModel> Items { get; set; }
        public List<OpenCouncilViewModel> Councils { get; set; }
        public int MaxCouncils { get; set; }
    }
}
