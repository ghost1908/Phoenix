using Phoenix.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Phoenix.Infrastructure.Entities;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _connectionString;

        public Guid UserID { get; set; }

        public AddressRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Area> GetArea(Guid id)
        {
            Area result = new Area();

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryFirstOrDefaultAsync<Area>("SELECT * FROM dbo.Area WHERE ID = @ID", new { ID = id.ToString() });
            }

            return result;
        }

        public async Task<IEnumerable<Area>> GetAreas(int page, int take)
        {
            IEnumerable<Area> areaList;

            using (var connection = new SqlConnection(_connectionString))
            {
                areaList = await connection.QueryAsync<Area>("sp_getarealist", param: new { UserID = UserID }, commandType: CommandType.StoredProcedure);
            }

            return areaList.ToList();
        }

        public async Task<FullAddress> GetFullAddress(Guid id)
        {
            FullAddress fullAddress;

            using (var connection = new SqlConnection(_connectionString))
            {
                fullAddress = await connection.QueryFirstOrDefaultAsync<FullAddress>("sp_getfulladdress", param: new { ID = id.ToString(), UserID = UserID }, commandType: CommandType.StoredProcedure);
            }

            return fullAddress;
        }

        public Region GetRegion(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Region>> GetRegions(string areaID = null)
        {
            IEnumerable<Region> regionList;

            using (var connection = new SqlConnection(_connectionString))
            {
                regionList = await connection.QueryAsync<Region>("sp_getregionlist", param: new { UserID = UserID, AreaID = areaID }, commandType: CommandType.StoredProcedure);
            }

            return regionList.ToList();
        }

        Task<Region> IAddressRepository.GetRegion(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CityInRegion>> GetCityInRegion(Guid regionID)
        {
            IEnumerable<CityInRegion> cityInRegionList;

            using (var connection = new SqlConnection(_connectionString))
            {
                cityInRegionList = await connection.QueryAsync<CityInRegion>("sp_getcityinregion", param: new { UserID = UserID, RegionID = regionID }, commandType: CommandType.StoredProcedure);
            }

            return cityInRegionList.ToList();
        }

        public async Task<IEnumerable<StreetInCity>> GetStreetInCity(Guid cityID)
        {
            IEnumerable<StreetInCity> streetInCityList;

            using (var connection = new SqlConnection(_connectionString))
            {
                streetInCityList = await connection.QueryAsync<StreetInCity>("sp_getstreetincity", param: new { UserID = UserID, CityID = cityID }, commandType: CommandType.StoredProcedure);
            }

            return streetInCityList.ToList();
        }

        public Task<City> GetCity(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Street> GetStreet(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Building> GetBuilding(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
