using System;

namespace Phoenix.Infrastructure.Entities
{
    public class PersonFilter
    {
        public Guid? AreaId { get; set; }
        public Guid? RegionId { get; set; }
        public Guid? CommunityId { get; set; }
        public bool? HasTelegram { get; set; } 
        public bool? HasViber { get; set; }
        public bool? HasWhatsapp { get; set; }
        public bool? HasFacebook { get; set; }
        public bool? HasInstagram { get; set; }
        public bool? IsEmployee { get; set; }
        public Guid? PositionId { get; set; }
        public bool? IsDeputy { get; set; }
        public bool? IsPartyMember { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
