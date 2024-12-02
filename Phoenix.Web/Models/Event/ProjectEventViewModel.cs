using System;
using System.ComponentModel.DataAnnotations;

namespace Phoenix.Web.Models.Event
{
    public enum PROJECT_EVENT_TYPE
    {
        [Display(Name = "Дотик")]
        Touch,
        [Display(Name = "Захід")]
        Event
    }

    [Flags]
    public enum PROJECT_EVENT_ACCESS : byte
    {
        None = 0,
        Any = 1,
        Supporter = 2, // сторонник
        Employee = 4, // сотрудник
        PartyMember = 8, // член партии
        Deputy = 16, // депутат
    }

    public class ProjectEventViewModel
    {
        public Guid Id { get; set; }
        public string EventName { get; set; }
        public PROJECT_EVENT_TYPE EventType { get; set; }
        public PROJECT_EVENT_ACCESS EventAccess { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
    }
}
