using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election.Online.Result
{
    public class OblZpViewModel
    {
        public int Order { get; set; }
        public string CandidateName { get; set; }
        public long District1 { get; set; }
        public double District1P { get; set; }
        public long District2 { get; set; }
        public double District2P { get; set; }
        public long District3 { get; set; }
        public double District3P { get; set; }
        public long District4 { get; set; }
        public double District4P { get; set; }
        public long District5 { get; set; }
        public double District5P { get; set; }
        public long District6 { get; set; }
        public double District6P { get; set; }
        public long District7 { get; set; }
        public double District7P { get; set; }
        public long District8 { get; set; }
        public double District8P { get; set; }
        public long Total { get; set; }
        public double TotalP { get; set; }
    }
}
