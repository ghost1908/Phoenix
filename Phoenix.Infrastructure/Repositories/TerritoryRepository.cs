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
    public class TerritoryRepository : ITerritoryRepository
    {
        private readonly string _connectionString;

        public Guid UserID { get; set; }

        public TerritoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Area> GetArea(Guid id)
        {
            Area result = new Area();

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryFirstOrDefaultAsync<Area>("SELECT * FROM ter.Area WHERE ID = @ID", new { ID = id.ToString() });
            }

            return result;
        }

        public async Task<IEnumerable<Area>> GetAreas()
        {
            IEnumerable<Area> areaList;

            using (var connection = new SqlConnection(_connectionString))
            {
                areaList = await connection.QueryAsync<Area>("ter.sp_getarealist", param: new { UserID = UserID }, commandType: CommandType.StoredProcedure);
            }

            return areaList.ToList();
        }

        public async Task<Area> GetAreaFromRegion(string regId)
        {
            Area result = new Area();

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryFirstOrDefaultAsync<Area>("SELECT area.* FROM ter.Area area JOIN ter.RegionInArea ria ON ria.AREA_ID = area.ID WHERE ria.REG_ID = @ID", new { ID = regId });
            }

            return result;
        }

        /// <summary>
        /// Количество районов в области
        /// </summary>
        /// <param name="id">ID области</param>
        /// <returns></returns>
        public async Task<int> GetRegionCountInArea(Guid id)
        {
            int regionCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                regionCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.RegionInArea ria WHERE ria.AREA_ID = @areaID", new { areaID = id });
            }

            return regionCount;
        }

        /// <summary>
        /// Количество ОТГ в области
        /// </summary>
        /// <param name="id">ID области</param>
        /// <returns></returns>
        public async Task<int> GetCommunityCountInArea(Guid id)
        {
            int communityCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                communityCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.CommunityInRegion cmir JOIN ter.RegionInArea ria ON ria.ID = cmir.RIA_ID WHERE ria.AREA_ID = @areaID", new { areaID = id });
            }

            return communityCount;
        }

        /// <summary>
        /// Количество городов в области
        /// </summary>
        /// <param name="id">ID области</param>
        /// <returns></returns>
        public async Task<int> GetCityCountInArea(Guid id)
        {
            int cityCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                cityCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.CityInCommunity cic JOIN ter.CommunityInRegion cmir ON cic.CIR_ID = cmir.ID JOIN ter.RegionInArea ria ON ria.ID = cmir.RIA_ID WHERE ria.AREA_ID = @areaID", new { areaID = id });
            }

            return cityCount;
        }

        /// <summary>
        /// Количество улиц в области
        /// </summary>
        /// <param name="id">ID области</param>
        /// <returns></returns>
        public async Task<int> GetStreetCountInArea(Guid id)
        {
            int streetCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                streetCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.StreetInCity sic JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID JOIN ter.CommunityInRegion cmir ON cic.CIR_ID = cmir.ID JOIN ter.RegionInArea ria ON ria.ID = cmir.RIA_ID WHERE ria.AREA_ID = @areaID", new { areaID = id });
            }

            return streetCount;
        }

        /// <summary>
        /// Количество участков в области
        /// </summary>
        /// <param name="id">ID области</param>
        /// <returns></returns>
        public async Task<int> GetPrecinctCountInArea(Guid id)
        {
            int precinctCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                precinctCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.PrecinctInCommunity pic JOIN ter.CommunityInRegion cmir ON cmir.ID = pic.CMIR_ID JOIN ter.RegionInArea ria ON ria.ID = cmir.RIA_ID WHERE ria.AREA_ID = @areaID", new { areaID = id });
            }

            return precinctCount;
        }

        public async Task<IEnumerable<RegionInArea>> GetRegionInArea(string areaID)
        {
            IEnumerable<RegionInArea> regionInAreaList;

            using (SqlConnection connection =new SqlConnection(_connectionString))
            {
                regionInAreaList = await connection.QueryAsync<RegionInArea>("SELECT * FROM ter.RegionInArea WHERE AREA_ID = @areaID", new { areaID = areaID.ToString() });
            }

            return regionInAreaList.ToList();
        }

        public async Task<RegionInArea> GetRegionInAreaById(Guid id)
        {
            RegionInArea ria;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                ria = await connection.QueryFirstOrDefaultAsync<RegionInArea>("SELECT * FROM ter.RegionInArea WHERE ID = @id", new { id = id });
            }

            return ria;
        }

        public async Task<Region> GetRegion(Guid id)
        {
            Region result = new Region();

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryFirstOrDefaultAsync<Region>("SELECT * FROM ter.Region WHERE ID = @ID", new { ID = id });
            }

            return result;
        }

        public async Task<IEnumerable<Region>> GetRegions(string areaID = null)
        {
            IEnumerable<Region> regionList;

            using (var connection = new SqlConnection(_connectionString))
            {
                regionList = await connection.QueryAsync<Region>("ter.sp_getregionlist", param: new { UserID = UserID, AreaID = areaID }, commandType: CommandType.StoredProcedure);
            }

            return regionList.ToList();
        }

        /// <summary>
        /// Количество ОТГ в районе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> GetCommunityCountInRegion(Guid id)
        {
            int communityCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                communityCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.CommunityInRegion cmir JOIN ter.RegionInArea ria ON ria.ID = cmir.RIA_ID WHERE ria.REG_ID = @regID", new { regID = id });
            }

            return communityCount;
        }

        /// <summary>
        /// Количество городов в районе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> GetCityCountInRegion(Guid id)
        {
            int cityCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                cityCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.CityInCommunity cic JOIN ter.CommunityInRegion cmir ON cmir.ID = cic.CIR_ID JOIN ter.RegionInArea ria ON ria.ID = cmir.RIA_ID WHERE ria.REG_ID = @regID", new { regID = id });
            }

            return cityCount;
        }

        /// <summary>
        /// Количество улиц в районе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> GetStreetCountInRegion(Guid id)
        {
            int streetCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                streetCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.StreetInCity sic JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID JOIN ter.CommunityInRegion cmir ON cmir.ID = cic.CIR_ID JOIN ter.RegionInArea ria ON ria.ID = cmir.RIA_ID WHERE ria.REG_ID = @regID", new { regID = id });
            }

            return streetCount;
        }

        /// <summary>
        /// Количество участков в районе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> GetPrecinctCountInRegion(Guid id)
        {
            int precinctCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                precinctCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.PrecinctInCommunity pic JOIN ter.CommunityInRegion cmir ON cmir.ID = pic.CMIR_ID JOIN ter.RegionInArea ria ON ria.ID = cmir.RIA_ID WHERE ria.REG_ID = @regID", new { regID = id });
            }

            return precinctCount;
        }

        public async Task<IEnumerable<CommunityInRegion>> GetCommunityInRegion(Guid regID)
        {
            IEnumerable<CommunityInRegion> communityInRegionList;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                communityInRegionList = await connection.QueryAsync<CommunityInRegion>("SELECT * FROM ter.CommunityInRegion WHERE RIA_ID = @regID", new { regID = regID.ToString() });
            }

            return communityInRegionList.ToList();
        }

        public async Task<IEnumerable<CommunityList>> GetCommunities(string areaId, string regId, string riaId = null)
        {
            IEnumerable<CommunityList> communities;

            using (SqlConnection connection=new SqlConnection(_connectionString))
            {
                communities = await connection.QueryAsync<CommunityList>("ter.sp_getcommunitylist", param: new { areaId = areaId, regId = regId, riaId = riaId, UserID = UserID }, commandType: CommandType.StoredProcedure);
            }

            return communities.ToList();
        }

        public async Task<CommunityInRegion> GetCommunityInRegionById(Guid id)
        {
            CommunityInRegion cir = new CommunityInRegion();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                cir = await connection.QueryFirstOrDefaultAsync<CommunityInRegion>(@"SELECT * FROM ter.CommunityInRegion WHERE ID = @id", param: new { id = id });
            }

            return cir;
        }

        /// <summary>
        /// Количество городов в громаде
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> GetCityCountInCommunity(Guid id)
        {
            int cityCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                cityCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.CityInCommunity cic JOIN ter.CommunityInRegion cmir ON cmir.ID = cic.CIR_ID WHERE cmir.CMN_ID = @cmnID", new { cmnID = id });
            }

            return cityCount;
        }

        /// <summary>
        /// Количество улиц в громаде
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> GetStreetCountInCommunity(Guid id)
        {
            int streetCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                streetCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.StreetInCity sic JOIN ter.CityInCommunity cic ON cic.ID = sic.CIC_ID JOIN ter.CommunityInRegion cmir ON cmir.ID = cic.CIR_ID WHERE cmir.CMN_ID = @cmnID", new { cmnID = id });
            }

            return streetCount;
        }

        /// <summary>
        /// Количество участков в громаде
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> GetPrecinctCountInCommunity(Guid id)
        {
            int precinctCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                precinctCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.PrecinctInCommunity pic JOIN ter.CommunityInRegion cmir ON cmir.ID = pic.CMIR_ID WHERE cmir.CMN_ID = @cmnID", new { cmnID = id });
            }

            return precinctCount;
        }

        public async Task<string> CreateCommunity(Guid riaID, string communityName)
        {
            string result = string.Empty;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                result = await connection.ExecuteScalarAsync<string>("ter.sp_createcommunity", param: new { ria_id = riaID, community_name = communityName }, commandType: CommandType.StoredProcedure);
            }

            return result;
        }

        public async Task<IEnumerable<CityInCommunityList>> GetCities(string areaId, string riaId, string cirId)
        {
            IEnumerable<CityInCommunityList> cities;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                cities = await connection.QueryAsync<CityInCommunityList>("ter.sp_getcitylist", param: new { areaId = areaId, riaId = riaId, cirId = cirId, UserId = UserID }, commandType: CommandType.StoredProcedure);
            }

            return cities.ToList();
        }

        public async Task<CityInCommunity> GetCity(Guid id)
        {
            CityInCommunity result = new CityInCommunity();

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryFirstOrDefaultAsync<CityInCommunity>("SELECT cic.*, city.NAME AS CITY_NAME, ct.ID AS CITYT_ID, ct.NAME AS CITYT_NAME FROM ter.CityInCommunity cic JOIN ter.City city ON city.ID = cic.CITY_ID JOIN ter.CityType ct ON ct.ID = city.CITYT_ID WHERE cic.ID = @ID", new { ID = id });
            }

            return result;
        }

        /// <summary>
        /// Количество улиц в городе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> GetStreetCountInCity(Guid id)
        {
            int streetCount = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                streetCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM ter.StreetInCity sic WHERE sic.CIC_ID = @cityID", new { cityID = id });
            }

            return streetCount;
        }

        public async Task<IEnumerable<CityType>> GetCityTypes()
        {
            IEnumerable<CityType> cityTypes;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                cityTypes = await connection.QueryAsync<CityType>("SELECT * FROM ter.CityType");
            }

            return cityTypes;
        }

        public async Task<IEnumerable<StreetInCity>> GetStreetInCity(Guid cityID)
        {
            IEnumerable<StreetInCity> streetInCityList;

            using (var connection = new SqlConnection(_connectionString))
            {
                streetInCityList = await connection.QueryAsync<StreetInCity>("ter.sp_getstreetincity", param: new { UserID = UserID, CityID = cityID }, commandType: CommandType.StoredProcedure);
            }

            return streetInCityList.ToList();
        }

        public async Task<IEnumerable<StreetList>> GetStreetNames()
        {
            IEnumerable<StreetList> streetNames;

            using (var connection = new SqlConnection(_connectionString))
            {
                streetNames = await connection.QueryAsync<StreetList>("SELECT s.ID, s.NAME AS STR_NAME, s.STRT_ID, st.NAME AS STRT_NAME FROM ter.Street s JOIN ter.StreetType st ON st.ID = s.STRT_ID");
            }

            return streetNames;
        }

        public async Task<IEnumerable<PrecinctList>> GetPrecincts(string areaId, string regId, string communityId)
        {
            IEnumerable<PrecinctList> precincts;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                precincts = await connection.QueryAsync<PrecinctList>("ter.sp_getprecinctlist", param: new { areaId = areaId, regId = regId, cmnId = communityId }, commandType: CommandType.StoredProcedure);
            }

            return precincts.ToList();
        }

        public async Task<IEnumerable<TerritoryDescription>> GetTerritoryDescription(Guid[] sicID)
        {
            IEnumerable<TerritoryDescription> territoryDescriptions;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                territoryDescriptions = await connection.QueryAsync<TerritoryDescription>("SELECT * FROM ter.TerritoryDescription WHERE SIC_ID IN @sic", param: new { sic = sicID });
            }

            return territoryDescriptions;
        }

        public async Task<FullAddress> GetFullAddress(Guid id)
        {
            FullAddress fullAddress;

            using (var connection = new SqlConnection(_connectionString))
            {
                fullAddress = await connection.QueryFirstOrDefaultAsync<FullAddress>("ter.sp_getfulladdress", param: new { ID = id.ToString(), UserID = UserID }, commandType: CommandType.StoredProcedure);
            }

            return fullAddress;
        }

        public async Task<string> GetSetAddressByStreet(Guid sicId, string buildingNumber)
        {
            string result = string.Empty;

            using (var connection = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("sicId", sicId.ToString());
                p.Add("buildNumber", buildingNumber);
                p.Add("addrId", dbType: DbType.Guid, direction: ParameterDirection.Output);
                var r = await connection.QueryAsync<int>("ter.sp_getsetaddressbystreet", p, commandType: CommandType.StoredProcedure);

                result = p.Get<Guid>("addrId").ToString();
            }

            return result;
        }
    }
}
