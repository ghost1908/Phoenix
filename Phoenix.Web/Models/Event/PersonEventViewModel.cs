using Phoenix.Web.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Phoenix.Web.Models.Event
{
    public enum PERSON_EVENT_STATUS : byte
    {
        [Display(Name = "Свій")]
        Ours = 0,

        [Display(Name = "Потенціальний")]
        Potential = 1,

        [Display(Name = "Нейтральний")]
        Neutral = 2,

        [Display(Name = "Чужий")]
        Alien = 3
    }

    public class PersonEventViewModel
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public PROJECT_EVENT_TYPE EventType { get; set; }
        public DateTime EventDate { get; set; }
        public PERSON_EVENT_STATUS PersonStatus { get; set; }
        public TABLE_ROW_ACTION Action { get; set; }
        public string EventComment { get; set; }
        public string UserName { get; set; }
        public DateTime EventCreate { get; set; }

        public string DisplayEventType()
        {
            var eventTypeName = this.EventType.GetAttribute<DisplayAttribute>();

            if (eventTypeName != null)
                return eventTypeName.Name;
            else
                return this.EventType.ToString();
        }

        public string DisplayPersonStatus()
        {
            var personStatusName = this.PersonStatus.GetAttribute<DisplayAttribute>();

            if (personStatusName != null)
                return personStatusName.Name;
            else
                return this.PersonStatus.ToString();
        }
    }
}
