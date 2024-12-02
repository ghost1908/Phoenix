namespace Phoenix.Web.Models.Territory
{
    public class CityInRegionViewModel
    {
        public string ID { get; set; }
        public string CityName { get; set; }
    }

    public class CityInCommunityViewModel
    {
        public string ID { get; set; }
        public string CityName { get; set; }
    }

    public class CityListModel
    {
        public string ID { get; set; }
        public string CityName { get; set; }
        public string CityInCommunityID { get; set; }
        public string CommunityID { get; set; }
        public string CommunityName { get; set; }
        public string CommunityInRegionID { get; set; }
        public string RegionID { get; set; }
        public string RegionName { get; set; }
        public string RegionInAreaID { get; set; }
        public string AreaID { get; set; }
        public string AreaName { get; set; }
        public int StreetCount { get; set; }
        //public int PrecinctCount { get; set; }
    }
}
