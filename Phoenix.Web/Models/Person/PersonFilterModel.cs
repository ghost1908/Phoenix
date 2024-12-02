namespace Phoenix.Web.Models.Person
{
    public class PersonFilterModel
    {
        public string AreaId { get; set; } = "null";
        public string RegionId { get; set; } = "null";
        public string CommunityId { get; set; } = "null";
        public string HasTelegram { get; set; } = "null";
        public string HasViber { get; set; } = "null";
        public string HasWhatsapp { get; set; } = "null";
        public string HasFacebook { get; set; } = "null";
        public string HasInstagram { get; set; } = "null";
        public string IsEmployee { get; set; } = "null";
        public string PositionId { get; set; } = "null";
        public string IsDeputy { get; set; } = "null";
        public string IsPartyMember { get; set; } = "null";
        public string IsDeleted { get; set; } = "null";

        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
