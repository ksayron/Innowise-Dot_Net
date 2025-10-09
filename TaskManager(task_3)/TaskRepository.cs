using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TaskManager_task_3_
{
    public class ConnectFactory
    {
        private readonly string _connString;

        public ConnectFactory()
        {
            var connectionConfiguration = new ConfigurationBuilder()
                .SetBasePath(
                    Directory
                        .GetCurrentDirectory()
                        .Substring(0, Directory.GetCurrentDirectory().Length - 17)
                )
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            _connString = connectionConfiguration["ConnectionStrings:DefaultConnection"];
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connString);
    }

    public class TaskRepository
    {
        readonly ConnectFactory _connectFactory = null;

        public TaskRepository()
        {
            var connectFactory = new ConnectFactory();
            this._connectFactory = connectFactory;
        }

        public List<Task> GetTasks()
        {
            using var db = _connectFactory.CreateConnection();
            try
            {
                return db.Query<Task>("Select * From Tasks").ToList();
            }
            catch
            {
                throw;
            }
        }

        public Task GetTaskById(int TaskId)
        {
            using var db = _connectFactory.CreateConnection();
            try
            {
                return db.Query<Task>("Select * From Tasks Where Id = @TaskId", new { TaskId })
                    .FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public void CreateTask(Task task)
        {
            using var db = _connectFactory.CreateConnection();
            try
            {
                var sqlQuery =
                    "Insert into Tasks (Title,Description,IsCompleted,CreatedAt) Values (@Title,@Description,@IsCompleted,@CreatedAt)";
                db.Execute(sqlQuery, task);
            }
            catch
            {
                throw;
            }
        }

        public void CompleteTask(int TaskId)
        {
            using var db = _connectFactory.CreateConnection();
            try
            {
                var sqlQuery = "Update Tasks Set IsCompleted = 1 Where Id = @TaskId";
                db.Execute(sqlQuery, new { TaskId });
            }
            catch
            {
                throw;
            }
        }

        public void DeleteTask(int TaskId)
        {
            using var db = _connectFactory.CreateConnection();
            try
            {
                var sqlQuery = "Delete from Tasks Where Id = @TaskId";
                db.Execute(sqlQuery, new { TaskId });
            }
            catch
            {
                throw;
            }
        }
    }
}
