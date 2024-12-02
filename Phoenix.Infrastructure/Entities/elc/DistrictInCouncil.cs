using System;
using System.Collections.Generic;
using System.Text;

namespace Phoenix.Infrastructure.Entities.elc
{
    public class DistrictInCouncil
    {
        public Guid DIC_ID { get; set; }
        public Guid CNCL_ID { get; set; }
        public Guid DSTR_ID { get; set; }
        public string DSTR_NAME { get; set; }
    }
}
