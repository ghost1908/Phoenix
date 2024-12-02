using System;

namespace Phoenix.Infrastructure.Entities
{
    public class AddressInPrecinct
    {
        public Guid ADDR_ID { get; set; }
        public Guid PRCT_ID { get; set; }
        public DateTime? DCLOSE { get; set; }
    }
}
