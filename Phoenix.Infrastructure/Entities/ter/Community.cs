using System;

namespace Phoenix.Infrastructure.Entities
{
    public class Community
    {
        public Guid ID { get; set; }
        public string NAME { get; set; }
    }

    public class CommunityList
    {
        public Guid ID { get; set; }
        public string CommunityName { get; set; }
        public Guid CommunityInRegionID { get; set; }
        public Guid RegionID { get; set; }
        public string RegionName { get; set; }
        public Guid RegionInAreaID { get; set; }
        public Guid AreaID { get; set; }
        public string AreaName { get; set; }
    }
}
