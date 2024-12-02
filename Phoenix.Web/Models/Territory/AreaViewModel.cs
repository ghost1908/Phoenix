using System.Collections.Generic;

namespace Phoenix.Web.Models.Territory
{
    public class AreaViewModel
    {
        public string ID { get; set; }
        public string AreaName { get; set; }
    }

    public class AreaListModel
    {
        public string ID { get; set; }
        public string AreaName { get; set; }
        public int RegionCount { get; set; }
        public int CommunityCount { get; set; }
        public int CityCount { get; set; }
        public int StreetCount { get; set; }
        public int PrecinctCount { get; set; }
    }

    public class AreaListViewModel
    {
        public IEnumerable<AreaViewModel> AreaItems { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
