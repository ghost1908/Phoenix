namespace Phoenix.Web.Models.Territory
{
    public class PrecinctViewModel
    {
        public string ID { get; set; }
        public string PrecinctNumber { get; set; }
        public string PrecinctInCommunityID { get; set; }
        public string CommunityID { get; set; }
        public string CommunityName { get; set; }
        public string CommunityInRegionID { get; set; }
        public string RegionID { get; set; }
        public string RegionName { get; set; }
        public string RegionInAreaID { get; set; }
        public string AreaID { get; set; }
        public string AreaName { get; set; }
    }
}
