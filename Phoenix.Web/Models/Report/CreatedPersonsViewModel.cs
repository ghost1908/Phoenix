using System.ComponentModel.DataAnnotations;

namespace Phoenix.Web.Models.Report
{
    public class CreatedPersonsViewModel
    {
        [Display(Name = "Район")]
        public string RegionName { get; set; }
        [Display(Name = "Громада")]
        public string CommunityName { get; set; }
        [Display(Name = "Користувач")]
        public string UserName { get; set; }
        [Display(Name = "Кількість")]
        public long TotalCount { get; set; }
    }
}
