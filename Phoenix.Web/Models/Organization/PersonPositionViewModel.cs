using System;

namespace Phoenix.Web.Models.Organization
{
    public class PersonPositionViewModel
    {
        public string Id { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string PositionId { get; set; }
        public string PositionName { get; set; }
        public DateTime? AppointDate { get; set; }
        public DateTime? DismissDate { get; set; }

        public TABLE_ROW_ACTION Action { get; set; }
    }
}
