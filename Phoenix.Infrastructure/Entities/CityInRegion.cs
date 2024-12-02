using System;

namespace Phoenix.Infrastructure.Entities
{
    public class CityInRegion
    {
        public Guid ID { get; set; }
        public Guid REG_ID { get; set; }
        public Guid CITY_ID { get; set; }

        public virtual string CITY_NAME { get; set; }
        public virtual Guid CITYT_ID { get; set; }
        public virtual string CITYT_NAME { get; set; }
    }
}
