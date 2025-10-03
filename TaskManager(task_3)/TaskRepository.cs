using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager_task_3_
{
    public class ConnectFactory
    {
        private readonly string _connString;
        public ConnectFactory()
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 17))//находим файл конфигурации не в окружении исполняемого файла,а в папке проекта
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
            _connString = configuration["ConnectionStrings:DefaultConnection"];
            
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connString);
    }
    public class TaskRepository
    {
        readonly ConnectFactory _factory = null;

        public TaskRepository()
        {
            var factory = new ConnectFactory();
            this._factory = factory;
        }

        public List<Task> GetTasks()
        {
            using var db = _factory.CreateConnection();
            try
            {
                return db.Query<Task>("Select * From Tasks").ToList();
            }
            catch
            {
                throw;//кидаем исключение выше
            }
        }
        public Task GetTaskById(int TaskId)
        {
            using var db = _factory.CreateConnection();
            try
            {
                return db.Query<Task>("Select * From Tasks Where Id = @TaskId", new { TaskId }).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }
        public void CreateTask(Task task)
        {
            using var db = _factory.CreateConnection();
            try
            {
                var sqlQuery = "Insert into Tasks (Title,Description,IsCompleted,CreatedAt) Values (@Title,@Description,@IsCompleted,@CreatedAt)";
                db.Execute(sqlQuery, task);
            }
            catch
            {
                throw;
            }
        }
        public void CompleteTask(int TaskId)
        {
            using var db = _factory.CreateConnection();
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
            using var db = _factory.CreateConnection();
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
