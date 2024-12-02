using System;

namespace Phoenix.Infrastructure.Entities
{
    public class StreetInCity
    {
        public Guid ID { get; set; }
        public Guid CIR_ID { get; set; }
        public Guid STR_ID { get; set; }

        public virtual string STR_NAME { get; set; }
        public virtual Guid STRT_ID { get; set; }
        public virtual string STRT_NAME { get; set; }
    }
}
