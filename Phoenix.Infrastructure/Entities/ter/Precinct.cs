using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Infrastructure.Entities
{
    public class PrecinctList
    {
        public Guid ID { get; set; }
        public string PRCT_NUMBER { get; set; }
        public Guid PRCT_CMN_ID { get; set; }
        public Guid CMN_ID { get; set; }
        public string CMN_NAME { get; set; }
        public Guid CMN_REG_ID { get; set; }
        public Guid REG_ID { get; set; }
        public string REG_NAME { get; set; }
        public Guid REG_AREA_ID { get; set; }
        public Guid AREA_ID { get; set; }
        public string AREA_NAME { get; set; }
    }
}
