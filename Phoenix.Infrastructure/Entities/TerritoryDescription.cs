using System;

namespace Phoenix.Infrastructure.Entities
{
    public class TerritoryDescription
    {
        public Guid SIC_ID { get; set; }
        public Guid PRCT_ID { get; set; }
        public string BLD_START { get; set; }
        public int? BLD_START_1 { get; set; }
        public string BLD_START_M { get; set; }
        public string BLD_START_2 { get; set; }
        public string BLD_END { get; set; }
        public int? BLD_END_1 { get; set; }
        public string BLD_END_M { get; set; }
        public string BLD_END_2 { get; set; }
        public DateTime? DCLOSE { get; set; }
    }
}
