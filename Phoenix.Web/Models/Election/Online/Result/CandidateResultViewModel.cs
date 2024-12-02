namespace Phoenix.Web.Models.Election.Online
{
    public class CandidateResultViewModel
    {
        public int Position { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public long Votes { get; set; }
        public double Percent { get; set; }
        public bool OurCandidate { get; set; }
        public string Nomination { get; set; }

        public CandidateResultViewModel()
        {
            Percent = 0;
            Votes = 0;
        }
    }
}
