using System;

namespace Phoenix.Infrastructure.Entities
{
    public class Region
    {
        public Guid ID { get; set; }
        public Guid AREA_ID { get; set; }
        public Guid REG_ID { get; set; }
        public string NAME { get; set; }
        public bool IS_CITY_REGION { get; set; }
    }
}
