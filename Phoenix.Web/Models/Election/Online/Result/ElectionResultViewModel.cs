using Phoenix.Web.Models.Election.Online.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election.Online
{
    public class ElectionResultViewModel
    {
        public double ProcessedProtocols { get; set; }
        public List<CandidateResultViewModel> CandidateResults { get; set; }
        public ProtocolResultViewModel Protocols { get; set; }
        public long TotalVoters { get; set; }
        public long TotalVote { get; set; }
        public double Turnout
        {
            get
            {
                return this.TotalVoters == 0 ? 0 : (double)this.TotalVote / (double)this.TotalVoters * 100;
            }
        }

        public ElectionResultViewModel()
        {
            this.ProcessedProtocols = 0;
            this.CandidateResults = new List<CandidateResultViewModel>();
            this.Protocols = new ProtocolResultViewModel();
        }
    }
}
