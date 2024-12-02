using System;

namespace Phoenix.Infrastructure.Entities
{
    public class PersonInfo
    {
        public Guid PSN_ID { get; set; }
        public string PASS_SERIES { get; set; }
        public string PASS_NUMBER { get; set; }
        public string PASS_ISSUE { get; set; }
        public string TAX_NUMBER { get; set; }
    }
}
