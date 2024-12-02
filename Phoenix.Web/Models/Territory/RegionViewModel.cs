namespace Phoenix.Web.Models.Territory
{
    public class RegionViewModel
    {
        public string ID { get; set; }
        public string RegionName { get; set; }
        public bool IsCityRegion { get; set; }
        public string AreaID { get; set; }
        public string AreaName { get; set; }
    }

    public class RegionListModel
    {
        public string ID { get; set; }
        public string RegionName { get; set; }
        public string AreaID { get; set; }
        public string AreaName { get; set; }
        public int CommunityCount { get; set; }
        public int CityCount { get; set; }
        public int StreetCount { get; set; }
        public int PrecinctCount { get; set; }
    }

    public class RegionInAreaViewModel
    {
        public string ID { get; set; }
        public string RegionID { get; set; }
        public string RegionName { get; set; }
        public bool IsCityRegion { get; set; }
    }
}
