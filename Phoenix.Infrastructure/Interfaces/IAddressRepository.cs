using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Phoenix.Infrastructure.Entities;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface IAddressRepository : IBaseRepository
    {
        Task<FullAddress> GetFullAddress(Guid id);

        Task<IEnumerable<Area>> GetAreas(int page, int take);
        Task<Area> GetArea(Guid id);

        Task<IEnumerable<Region>> GetRegions(string areaID = null);
        Task<Region> GetRegion(Guid id);

        Task<IEnumerable<CityInRegion>> GetCityInRegion(Guid regionID);
        Task<City> GetCity(Guid id);

        Task<IEnumerable<StreetInCity>> GetStreetInCity(Guid cityID);
        Task<Street> GetStreet(Guid id);

        Task<Building> GetBuilding(Guid id);
    }
}
