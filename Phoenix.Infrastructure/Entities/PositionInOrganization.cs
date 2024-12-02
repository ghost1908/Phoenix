using System;

namespace Phoenix.Infrastructure.Entities
{
    public class PositionInOrganization
    {
        public Guid ID { get; set; }
        public Guid ORG_ID { get; set; }
        public string ORG_NAME { get; set; }
        public Guid PSN_ID { get; set; }
        public Guid POS_ID { get; set; }
        public string POS_NAME { get; set; }
        public DateTime? APPOINT_DATE { get; set; }
        public DateTime? DISMISS_DATE { get; set; }

        public virtual int ACTION { get; set; }
    }
}
