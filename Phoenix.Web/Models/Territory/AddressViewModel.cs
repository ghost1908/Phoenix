using System.ComponentModel.DataAnnotations;

namespace Phoenix.Web.Models.Territory
{
    public class AddressViewModel
    {
        public string ID { get; set; }
        public string AreaID { get; set; }
        [Display(Name = "Область")]
        public string AreaName { get; set; }
        public string RegionInAreaID { get; set; }
        public string RegionID { get; set; }
        [Display(Name = "Район")]
        public string RegionName { get; set; }
        //public bool IsCityRegion { get; set; }
        public string CommunityInRegionID { get; set; }
        public string CommunityID { get; set; }
        [Display(Name ="Громада")]
        public string CommunityName { get; set; }
        public string CityInCommunityID { get; set; }
        public string CityTypeID { get; set; }
        public string CityTypeName { get; set; }
        public string CityID { get; set; }
        [Display(Name = "Город")]
        public string CityName { get; set; }
        public string StreetInCityID { get; set; }
        public string StreetTypeID { get; set; }
        public string StreetTypeName { get; set; }
        public string StreetID { get; set; }
        [Display(Name = "Улица")]
        public string StreetName { get; set; }
        public string BuildingID { get; set; }
        [Display(Name = "Дом")]
        public string BuildingNumber { get; set; }
        [Display(Name = "Описание")]
        public string BuildingIssue { get; set; }
        [Display(Name = "Квартира")]
        public string ApartmentNumber { get; set; }

        public override string ToString()
        {
            string result = string.Empty;

            result += this.AreaName + ", ";
            result += this.RegionName + ", ";
            result += this.CommunityName + ", ";
            result += this.CityTypeName + ((string.IsNullOrWhiteSpace(this.CityTypeName) || this.CityTypeName.EndsWith('.')) ? "" : " ") + this.CityName + ", ";
            result += this.StreetTypeName + ((string.IsNullOrWhiteSpace(this.StreetTypeName) || this.StreetTypeName.EndsWith('.')) ? "" : " ") + this.StreetName + ", ";
            result += "буд." + this.BuildingNumber + (string.IsNullOrWhiteSpace(this.BuildingIssue) ? "" : this.BuildingIssue);
            if (!string.IsNullOrWhiteSpace(this.ApartmentNumber))
                result += ", кв." + this.ApartmentNumber;

            return result;
        }
    }
}
