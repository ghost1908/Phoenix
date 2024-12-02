using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Web.Models.Territory
{
    public class CityEditModel
    {
        public Guid CityInCommunityID { get; set; }
        public Guid CityID { get; set; }
        public string CityName { get; set; }
        public Guid CityTypeID { get; set; }
        public string CityTypeName { get; set; }
        public string CityFullName { get { return this.CityTypeName + this.CityName; } }
        public Guid AreaId { get; set; }
        public Guid RegionInAreaID { get; set; }
        public Guid CommunityInRegionID { get; set; }

        public List<TerritoryDescriptionViewModel> Streets { get; set; }

        public List<SelectListItem> CityTypes { get; set; }
        public List<SelectListItem> PrecinctNumbers { get; set; }
        public List<SelectListItem> StreetNames { get; set; }

        public CityEditModel()
        {
            this.CityTypes = new List<SelectListItem>();
            this.PrecinctNumbers = new List<SelectListItem>();
            this.StreetNames = new List<SelectListItem>();
            this.Streets = new List<TerritoryDescriptionViewModel>();
        }
    }
}
