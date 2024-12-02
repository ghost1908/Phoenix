namespace Phoenix.Web.Models.Territory
{
    public class CommunityViewModel
    {
        public string ID { get; set; }
        public string CommunityName { get; set; }
        public string CommunityInRegionID { get; set; }
        public string RegionID { get; set; }
        public string RegionName { get; set; }
        public string RegionInAreaID { get; set; }
        public string AreaID { get; set; }
        public string AreaName { get; set; }
    }

    public class CommunityListModel
    {
        public string ID { get; set; }
        public string CommunityName { get; set; }
        public string CommunityInRegionID { get; set; }
        public string RegionID { get; set; }
        public string RegionName { get; set; }
        public string RegionInAreaID { get; set; }
        public string AreaID { get; set; }
        public string AreaName { get; set; }
        public int CityCount { get; set; }
        public int StreetCount { get; set; }
        public int PrecinctCount { get; set; }
    }

    public class CommunityEditModel
    {
        public string CommunityID { get; set; }
        public string CommunityName { get; set; }
        public string RegionInAreaID { get; set; }
    }
}
