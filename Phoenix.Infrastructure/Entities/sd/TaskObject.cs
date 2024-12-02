using System;

namespace Phoenix.Infrastructure.Entities
{
    public class TaskObject
    {
        public Guid ID { get; set; }
        public long NUMBER { get; set; }
        public Guid OWNER_ID { get; set; }
        public Guid? PERFORMER_ID { get; set; }
        public string REQUEST { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime? SEND_DATE { get; set; }
        public DateTime? ACCEPT_DATE { get; set; }
        public DateTime? DONE_DATE { get; set; }
        public DateTime? CONFIRM_DATE { get; set; }
        public string RESPONSE { get; set; }
    }
}
