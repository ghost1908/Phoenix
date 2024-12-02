using System;

namespace Phoenix.Infrastructure.Entities
{
    public class Precinct
    {
        public Guid ID { get; set; }
        public string NUMBER { get; set; }
        public DateTime? DCLOSE { get; set; }
    }
}
