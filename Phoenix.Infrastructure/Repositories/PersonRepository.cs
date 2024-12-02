using Dapper;
using Phoenix.Infrastructure.Entities;
using Phoenix.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _connectionString;

        public Guid UserID { get; set; }

        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<PersonList> GetPeopleList(PersonFilter filter, int skip = 0, int take = 10)
        {
            PersonList result = new PersonList();
            
            DataTable dtPersonFilter = GeneratePersonFilterTVP();

            dtPersonFilter.Rows.Add(filter.AreaId, filter.RegionId, filter.CommunityId,
                filter.HasTelegram, filter.HasViber, filter.HasWhatsapp,
                filter.HasFacebook, filter.HasInstagram,
                filter.IsEmployee, filter.PositionId, filter.IsDeputy, filter.IsPartyMember,
                filter.IsDeleted);

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync("sp_getpersonlist", param: new { Filter = dtPersonFilter.AsTableValuedParameter("dbo.TVP_PersonFilter"), Skip = skip, Take = take, UserID = UserID }, commandType: CommandType.StoredProcedure))
                {
                    result.TotalCount = multi.ReadSingle<long>();
                    result.Persons = multi.Read<Person>().ToList();
                }
                //result = await connection.QueryAsync<Person>("sp_getpersonlist", param: new { Filter = dtPersonFilter.AsTableValuedParameter("dbo.TVP_PersonFilter"), Skip = skip, Take = take, UserID = UserID }, commandType: CommandType.StoredProcedure);
            }

            return result;
        }

        public async Task<int> GetPeopleListCount()
        {
            int result = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.ExecuteScalarAsync<int>("dbo.sp_getpersonlistcount", param: new { UserID = UserID }, commandType: CommandType.StoredProcedure);
            }

            return result;
        }

        public async Task<byte> GetLastFormStatus(Guid personId)
        {
            byte result = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.ExecuteScalarAsync<byte>("SELECT ISNULL(pfs.STATUS, 0) AS [STATUS] FROM dbo.PersonFormStatus pfs WHERE pfs.PSN_ID = @psnId AND ISNULL(pfs.CREATE_DATE, N'') = (SELECT ISNULL(MAX(maxStatus.CREATE_DATE), N'') FROM PersonFormStatus maxStatus WHERE maxStatus.PSN_ID = @psnId)", param: new { @psnId = personId }, commandType: CommandType.Text);
            }

            return result;
        }

        public async Task<Person> GetPerson(Guid personID)
        {
            bool checkAccess = false;
            Person person = new Person();

            using (var connection = new SqlConnection(_connectionString))
            {
                //checkAccess = await connection.ExecuteScalarAsync<bool>("sp_checkuseraccess", param: new { UserID = UserID, AccessType = "Person", ObjectAccess = "Address", ObjectID = personID }, commandType: CommandType.StoredProcedure);

                //if (checkAccess)
                //{
                    person = await connection.QueryFirstOrDefaultAsync<Person>("sp_getperson", param: new { ID = personID, UserID = UserID }, commandType: CommandType.StoredProcedure);
                //}
            }

            return person;
        }

        public async Task<PersonInfo> GetPersonInfo(Guid personID)
        {
            PersonInfo personInfo = new PersonInfo();

            using (var connection = new SqlConnection(_connectionString))
            {
                personInfo = await connection.QueryFirstOrDefaultAsync<PersonInfo>("SELECT * FROM dbo.PersonInfo WHERE PSN_ID = @ID", param: new { ID = personID }, commandType: CommandType.Text);
            }

            return personInfo;
        }

        public async Task<Person> CreatePerson(Person person)
        {
            DataTable dtPerson = GenerateTVP();

            dtPerson.Rows.Add(null, person.LNAME, person.FNAME, person.MNAME,
                person.GENDER, person.BDATE, person.VOTE,
                person.ADDR_REG, person.ADDR_REG_ROOM, person.ADDR_HOME, person.ADDR_HOME_ROOM,
                person.PHONE, person.HAS_TELEGRAM, person.HAS_VIBER, person.HAS_WHATSAPP, 
                person.EMAIL, person.HAS_FACEBOOK, person.HAS_INSTAGRAM,
                person.IS_EMPLOYEE, person.IS_DEPUTY, person.IS_PARTY_MEMBER,
                person.IS_DELETED);

            using (var connection = new SqlConnection(_connectionString))
            {
                person.ID = await connection.ExecuteScalarAsync<Guid>("dbo.sp_createperson",
                    param: new { person = dtPerson.AsTableValuedParameter("dbo.TVP_Person"), userid = UserID },
                    commandType: CommandType.StoredProcedure);
            }

            return person;
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            int result = 0;
            DataTable dtPerson = GenerateTVP();

            dtPerson.Rows.Add(person.ID, person.LNAME, person.FNAME, person.MNAME,
                person.GENDER, person.BDATE, person.VOTE,
                person.ADDR_REG, person.ADDR_REG_ROOM, person.ADDR_HOME, person.ADDR_HOME_ROOM,
                person.PHONE, person.HAS_TELEGRAM, person.HAS_VIBER, person.HAS_WHATSAPP,
                person.EMAIL, person.HAS_FACEBOOK, person.HAS_INSTAGRAM,
                person.IS_EMPLOYEE, person.IS_DEPUTY, person.IS_PARTY_MEMBER,
                person.IS_DELETED);

            var tvpPerson = dtPerson.AsTableValuedParameter("dbo.TVP_Person");

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.ExecuteScalarAsync<int>("dbo.sp_updateperson",
                    param: new { person = dtPerson.AsTableValuedParameter("dbo.TVP_Person") },
                    commandType: CommandType.StoredProcedure);
            }

            if (result == 0)
                return person;
            else
                return null;
        }

        public async Task<IEnumerable<Person>> FindDublicates(string lastName, string firstName)
        {
            IEnumerable<Person> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryAsync<Person>("sp_finddublicates", param: new { lastName = lastName, firstName = firstName }, commandType: CommandType.StoredProcedure);
            }

            return result.ToList();
        }

        public async Task<PersonInfo> UpdatePersonInfo(PersonInfo personInfo)
        {
            int result = 0;
            DataTable dtPersonInfo = new DataTable();
            dtPersonInfo.Columns.Add("PSN_ID");
            dtPersonInfo.Columns.Add("PASS_SERIES");
            dtPersonInfo.Columns.Add("PASS_NUMBER");
            dtPersonInfo.Columns.Add("PASS_ISSUE");
            dtPersonInfo.Columns.Add("TAX_NUMBER");

            dtPersonInfo.Rows.Add(personInfo.PSN_ID, personInfo.PASS_SERIES, personInfo.PASS_NUMBER,
                personInfo.PASS_ISSUE, personInfo.TAX_NUMBER);

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.ExecuteScalarAsync<int>("dbo.sp_updatepersoninfo",
                    param: new { personInfo = dtPersonInfo.AsTableValuedParameter("dbo.TVP_PersonInfo") },
                    commandType: CommandType.StoredProcedure);
            }

            if (result == 0)
                return personInfo;
            else
                return null;
        }

        #region Form Statuses

        public async Task<IEnumerable<PersonFormStatus>> GetPersonFormStatuses(Guid personId)
        {
            IEnumerable<PersonFormStatus> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = await connection.QueryAsync<PersonFormStatus>("SELECT * FROM dbo.PersonFormStatus WHERE PSN_ID = @psnId ORDER BY CREATE_DATE DESC", param: new { @psnId = personId }, commandType: CommandType.Text);
            }

            return result.ToList();
        }

        public async Task<IEnumerable<Guid>> CreateOrUpdateFormStatuses(Guid psnId, List<PersonFormStatus> statuses)
        {
            IEnumerable<Guid> statusesID;

            using (var connection = new SqlConnection(_connectionString))
            {
                foreach (var sts in statuses)
                {
                    switch (sts.ACTION)
                    {
                        case 0:     // создать
                            await connection.ExecuteAsync(@"INSERT INTO dbo.PersonFormStatus VALUES ( NEWID(), @psnID, @status, @userID, @cDate, @comment )", new { @psnID = psnId, @status = sts.STATUS, @userID = sts.USER_ID, @cDate = DateTime.Now, @comment = sts.COMMENT });
                            break;
                        case 1:     // обновить
                            await connection.ExecuteAsync(@"UPDATE dbo.PersonFormStatus SET STATUS = @status, CREATE_DATE = @cDate, COMMENT = @comment WHERE ID = @id", new { @status = sts.STATUS, @cDate = sts.CREATE_DATE, @comment = sts.COMMENT, @id = sts.ID });
                            break;
                        case 2:     // удалить
                            await connection.ExecuteAsync(@"DELETE FROM dbo.PersonFormStatus WHERE ID = @id", new { @id = sts.ID });
                            break;
                    }
                }

                statusesID = await connection.QueryAsync<Guid>(@"SELECT ID FROM dbo.PersonFormStatus WHERE PSN_ID = @psnID", new { @psnID = psnId });
            }

            return statusesID;
        }

        #endregion

        private DataTable GenerateTVP()
        {
            DataTable value = new DataTable();
            value.Columns.Add("ID");
            value.Columns.Add("LNAME");
            value.Columns.Add("FNAME");
            value.Columns.Add("MNAME");
            value.Columns.Add("GENDER");
            value.Columns.Add("BDATE", typeof(DateTime));
            value.Columns.Add("VOTE");
            value.Columns.Add("ADDR_REG");
            value.Columns.Add("ADDR_REG_ROOM");
            value.Columns.Add("ADDR_HOME");
            value.Columns.Add("ADDR_HOME_ROOM");
            value.Columns.Add("PHONE");
            value.Columns.Add("HAS_TELEGRAM");
            value.Columns.Add("HAS_VIBER");
            value.Columns.Add("HAS_WHATSAPP");
            value.Columns.Add("EMAIL");
            value.Columns.Add("HAS_FACEBOOK");
            value.Columns.Add("HAS_INSTAGRAM");
            value.Columns.Add("IS_EMPLOYEE");
            value.Columns.Add("IS_DEPUTY");
            value.Columns.Add("IS_PARTY_MEMBER");
            value.Columns.Add("IS_DELETED");

            return value;
        }

        private DataTable GeneratePersonFilterTVP()
        {
            DataTable value = new DataTable();
            value.Columns.Add("AREA_ID");
            value.Columns.Add("REG_ID");
            value.Columns.Add("CMN_ID");
            value.Columns.Add("HAS_TELEGRAM");
            value.Columns.Add("HAS_VIBER");
            value.Columns.Add("HAS_WHATSAPP");
            value.Columns.Add("HAS_FACEBOOK");
            value.Columns.Add("HAS_INSTAGRAM");
            value.Columns.Add("IS_EMPLOYEE");
            value.Columns.Add("POS_ID");
            value.Columns.Add("IS_DEPUTY");
            value.Columns.Add("IS_PARTYMEMBER");
            value.Columns.Add("IS_DELETED");

            return value;
        }
    }
}
