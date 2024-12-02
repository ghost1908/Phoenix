using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Phoenix.Infrastructure.Entities;

namespace Phoenix.Infrastructure.Interfaces
{
    public interface ITerritoryRepository : IBaseRepository
    {
        Task<IEnumerable<Area>> GetAreas();
        Task<Area> GetArea(Guid id);
        Task<Area> GetAreaFromRegion(string regId);
        Task<int> GetRegionCountInArea(Guid id);
        Task<int> GetCommunityCountInArea(Guid id);
        Task<int> GetCityCountInArea(Guid id);
        Task<int> GetStreetCountInArea(Guid id);
        Task<int> GetPrecinctCountInArea(Guid id);

        Task<IEnumerable<RegionInArea>> GetRegionInArea(string areaID);
        Task<RegionInArea> GetRegionInAreaById(Guid id);
        Task<IEnumerable<Region>> GetRegions(string areaID = null);
        Task<Region> GetRegion(Guid id);
        Task<int> GetCommunityCountInRegion(Guid id);
        Task<int> GetCityCountInRegion(Guid id);
        Task<int> GetStreetCountInRegion(Guid id);
        Task<int> GetPrecinctCountInRegion(Guid id);

        Task<IEnumerable<CommunityInRegion>> GetCommunityInRegion(Guid regID);
        Task<IEnumerable<CommunityList>> GetCommunities(string areaId, string regId, string riaId = null);
        Task<CommunityInRegion> GetCommunityInRegionById(Guid id);
        Task<int> GetCityCountInCommunity(Guid id);
        Task<int> GetStreetCountInCommunity(Guid id);
        Task<int> GetPrecinctCountInCommunity(Guid id);
        Task<string> CreateCommunity(Guid riaID, string communityName);

        Task<IEnumerable<CityInCommunityList>> GetCities(string areaId, string riaId, string cirId);
        Task<CityInCommunity> GetCity(Guid id);
        Task<int> GetStreetCountInCity(Guid id);
        //Task<int> GetPrecinctCountInCity(Guid id);

        Task<IEnumerable<CityType>> GetCityTypes();

        Task<IEnumerable<StreetInCity>> GetStreetInCity(Guid cityID);
        Task<IEnumerable<StreetList>> GetStreetNames();

        Task<IEnumerable<PrecinctList>> GetPrecincts(string areaId, string regId, string communityId);

        Task<IEnumerable<TerritoryDescription>> GetTerritoryDescription(Guid[] sicID);

        Task<FullAddress> GetFullAddress(Guid id);
        Task<string> GetSetAddressByStreet(Guid sicId, string buildingNumber);
    }
}
