using System;

namespace Phoenix.Web.Models.Organization
{
    public class PersonPositionEditModel
    {
        public string Id { get; set; }
        public string OrganizationId { get; set; }
        public string PositionId { get; set; }
        public string AppointDate { get; set; }
        public string DismissDate { get; set; }
        public string Action { get; set; }
    }
}
