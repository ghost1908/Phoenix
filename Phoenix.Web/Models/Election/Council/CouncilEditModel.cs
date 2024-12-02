using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Election
{
    public class CouncilEditModel
    {
        public string Id { get; set; }
        public string CouncilName { get; set; }
        public string CouncilTypeId { get; set; }
        public string CouncilTypeName { get; set; }
        public string DistrictTypeId { get; set; }
        public string DistrictTypeName { get; set; }
        public int DistrictCount { get; set; }
        public List<DistrictInCouncilEditModel> Districts { get; set; }
    }

    public class DistrictInCouncilEditModel
    {
        public string Id { get; set; }
        public string DistrictId { get; set; }
        public string DistrictNumber { get; set; }
        public List<ElectionPrecinctInDistrictEditModel> ElectionPrecincts { get; set; }
    }

    public class ElectionPrecinctInDistrictEditModel
    {
        public string Id { get; set; }
        public string ElectionPrecinctId { get; set; }
        public string ElectionPrecinctNumber { get; set; }
    }
}
