using System;

namespace Phoenix.Infrastructure.Entities
{
    public class Comment
    {
        public Guid ID { get; set; }
        public Guid TASK_ID { get; set; }
        public Guid USER_ID { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string TEXT { get; set; }
        public Guid? COMMENT_ID { get; set; }
    }
}
