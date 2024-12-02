using System;

namespace Phoenix.Infrastructure.Entities
{
    public class CityInCommunity
    {
        public Guid ID { get; set; }
        public Guid CIR_ID { get; set; }
        public Guid CITY_ID { get; set; }
        public string CITY_NAME { get; set; }
        public Guid CITYT_ID { get; set; }
        public string CITYT_NAME { get; set; }
    }

    public class CityInCommunityList
    {
        public Guid ID { get; set; }
        public Guid CityID { get; set; }
        public string CityName { get; set; }
        public Guid CityTypeID { get; set; }
        public string CityTypeName { get; set; }
        public Guid CommunityID { get; set; }
        public string CommunityName { get; set; }
        public Guid CommunityInRegionID { get; set; }
        public Guid RegionID { get; set; }
        public string RegionName { get; set; }
        public Guid RegionInAreaID { get; set; }
        public Guid AreaID { get; set; }
        public string AreaName { get; set; }
    }
}
