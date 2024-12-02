using System;

namespace Phoenix.Infrastructure.Entities
{
    public class Address
    {
        public Guid ID { get; set; }
        public Guid SIC_ID { get; set; }
        public Guid BLD_ID { get; set; }
        public string BLD_ISSUE { get; set; }
    }

    public class FullAddress
    {
        public Guid ID { get; set; }
        public Guid AREA_ID { get; set; }
        public string AREA_NAME { get; set; }
        public Guid REG_ARIA_ID { get; set; }
        public Guid REG_ID { get; set; }
        public string REG_NAME { get; set; }
        //public bool IS_CITY_REGION { get; set; }
        public Guid CMN_RIA_ID { get; set; }
        public Guid CMN_ID { get; set; }
        public string CMN_NAME { get; set; }
        public Guid CITY_IN_CMN_ID { get; set; }
        public Guid CITY_TYPE_ID { get; set; }
        public string CITY_TYPE_NAME { get; set; }
        public Guid CITY_ID { get; set; }
        public string CITY_NAME { get; set; }
        public Guid STREET_IN_CITY_ID { get; set; }
        public Guid STREET_TYPE_ID { get; set; }
        public string STREET_TYPE_NAME { get; set; }
        public Guid STREET_ID { get; set; }
        public string STREET_NAME { get; set; }
        public Guid BLD_ID { get; set; }
        public string BLD_NUMBER { get; set; }
        public string BLD_ISSUE { get; set; }
    }
}
