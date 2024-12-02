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
    public class ServiceDeskRepository : IServiceDeskRepository
    {
        private readonly string _connectionString;

        public Guid UserID { get; set; }

        public ServiceDeskRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TaskObject>> GetTasks(bool isAdmin = false)
        {
            IEnumerable<TaskObject> tasks;

            using (var connection = new SqlConnection(_connectionString))
            {
                if (isAdmin)
                    tasks = await connection.QueryAsync<TaskObject>(@"SELECT * FROM sd.Task ORDER BY CREATE_DATE DESC");
                else
                    tasks = await connection.QueryAsync<TaskObject>(@"SELECT * FROM sd.Task WHERE OWNER_ID = @owner ORDER BY CREATE_DATE DESC", param: new { owner = UserID });
            }

            return tasks;
        }

        public async Task<TaskObject> GetTask(string id)
        {
            TaskObject task;

            using (var connection = new SqlConnection(_connectionString))
            {
                task = await connection.QueryFirstOrDefaultAsync<TaskObject>(@"SELECT * FROM sd.Task WHERE ID = @taskId", param: new { taskId = id });
            }

            return task;
        }

        public async Task<TaskObject> EditTaskState(TaskObject item, byte type)
        {
            TaskObject task;

            using (var connection = new SqlConnection(_connectionString))
            {
                switch (type)
                {
                    case 0: // создание задачи
                        await connection.ExecuteAsync(@"INSERT INTO sd.Task (ID, OWNER_ID, REQUEST, CREATE_DATE) VALUES (@id, @owner, @request, @create)",
                            param: new
                            {
                                id = item.ID,
                                owner = UserID,
                                request = item.REQUEST,
                                create = item.CREATE_DATE
                            });
                        break;
                    case 1: // редактирвоание задачи
                        await connection.ExecuteAsync(@"UPDATE sd.Task SET REQUEST = @request, CREATE_DATE = @create WHERE ID = @id",
                            param: new
                            {
                                id = item.ID,
                                request = item.REQUEST,
                                create = item.CREATE_DATE
                            });
                        break;
                    case 2: // отправка задачи разработчику
                        await connection.ExecuteAsync(@"UPDATE sd.Task SET REQUEST = @request, SEND_DATE = @send WHERE ID = @id",
                            param: new
                            {
                                id = item.ID,
                                request = item.REQUEST,
                                send = item.SEND_DATE
                            });
                        break;
                    case 3: // принятие задачи в работу разработчиком
                        await connection.ExecuteAsync(@"UPDATE sd.Task SET ACCEPT_DATE = @accept, PERFORMER_ID = @performer WHERE ID = @id",
                            param: new
                            {
                                id = item.ID,
                                accept = item.ACCEPT_DATE,
                                performer = UserID
                            });
                        break;
                    case 4: // отклонение задачи разработчиком (возвращает задачи в статусе созданной)
                        await connection.ExecuteAsync(@"UPDATE sd.Task SET SEND_DATE = @send WHERE ID = @id",
                            param: new
                            {
                                id = item.ID,
                                send = item.SEND_DATE
                            });
                        break;
                    case 5: // удаление задачи
                        await connection.ExecuteAsync(@"DELETE FROM sd.Task WHERE ID = @id",
                            param: new
                            {
                                id = item.ID
                            });
                        break;
                    default: break;
                }

                task = await connection.QueryFirstOrDefaultAsync<TaskObject>(@"SELECT * FROM sd.Task WHERE ID = @taskId", param: new { taskId = item.ID });
            }

            return task;
        }
    }
}
