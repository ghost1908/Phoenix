using System.Collections.Generic;

namespace Phoenix.Web.Models.Election.Online.Result
{
    public class CommunityReportViewModel
    {
        public List<CommunityReportColumnItemViewModel> Columns { get; set; }
        public List<CommunityReportRowItemViewModel> Rows { get; set; }

        public CommunityReportViewModel()
        {
            this.Columns = new List<CommunityReportColumnItemViewModel>();
            this.Rows = new List<CommunityReportRowItemViewModel>();
        }
    }

    public class CommunityReportRowItemViewModel
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public List<CommunityReportRowValueViewModel> Values { get; set; }
    }

    public class CommunityReportRowValueViewModel
    {
        public int ColumnOrder { get; set; } = -1;
        public int Order { get; set; } = -1;
        public long Value { get; set; }
        public double Percent { get; set; }
        public int Position { get; set; }
    }

    public class CommunityReportColumnItemViewModel
    {
        public int Order { get; set; }
        public string Name { get; set; }
    }

    public class CandidatePosition
    {
        public string CandidateName { get; set; }
        public int Position { get; set; }
        public int Council { get; set; }
    }
}
