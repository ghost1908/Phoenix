using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election.Online.Result
{
    public class ProtocolResultViewModel
    {
        public int Total { get; set; }
        public int NoData { get; set; }
        public int NotOperationalData { get; set; }
        public int OperationalData { get; set; }
        public int NotFullData { get; set; }
        public int FullData { get; set; }
        public int WithErrors { get; set; }
        public int GoodData { get; set; }
    }
}
