using System.ComponentModel.DataAnnotations;

namespace Phoenix.Web.Models.Report
{
    public class CommunityPlansViewModel
    {
        [Display(Name = "Район")]
        public string RegionName { get; set; }
        [Display(Name = "Громада")]
        public string CommunityName { get; set; }
        [Display(Name = "Користувач")]
        public string UserName { get; set; }
        [Display(Name = "За минулий тиждень")]
        public long WeekCount { get; set; }
        [Display(Name = "Кількість всього")]
        public long TotalCount { get; set; }
        [Display(Name = "План")]
        public long PlanCount { get; set; }
        [Display(Name = "Вик. плану")]
        public float? PlanPercent { get; set; }

        public string CssStyle { get; set; }
        public int Order { get; set; }
    }
}
