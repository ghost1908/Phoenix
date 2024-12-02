using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election
{
    public class TurnoutListViewModel
    {
        public int TurnoutTimesCount { get; set; }

        public List<TimeSpan> TurnoutTimes { get; set; }

        public List<TurnoutViewModel> PrecinctsTurnout { get; set; }
    }

    public class TurnoutViewModel
    {
        public string Id { get; set; }

        public string DistrictNumber { get; set; }

        public string RegionName { get; set; }

        public string CommunityName { get; set; }

        public string PrecinctNumber { get; set; }

        public int Voters { get; set; }

        public Dictionary<string, int?> TurnoutValues { get; set; }
    }

    public class TurnoutEditModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        public TimeSpan TurnoutTime { get; set; }

        [JsonPropertyName("turnoutVoters")]
        public int? TurnoutVoters { get; set; }
    }
}
