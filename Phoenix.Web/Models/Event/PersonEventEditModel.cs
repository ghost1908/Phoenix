namespace Phoenix.Web.Models.Event
{
    public class PersonEventEditModel
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string EventId { get; set; }
        public string EventType { get; set; }
        public string EventDate { get; set; }
        public string PersonStatus { get; set; }
        public string Action { get; set; }
        public string EventComment { get; set; }
    }
}
