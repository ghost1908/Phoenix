using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Territory
{
    public class TerritoryDescriptionViewModel
    {
        public Guid ID { get; set; }
        public Guid PrecinctId { get; set; }
        public string PrecinctNumber { get; set; }
        public Guid StreetInCityID { get; set; }
        public Guid StreetID { get; set; }
        public string StreetName { get; set; }
        public Guid CityID { get; set; }
        public string CityName { get; set; }
        public string BuildingStart { get; set; }
        public string BuildingEnd { get; set; }
    }
}
