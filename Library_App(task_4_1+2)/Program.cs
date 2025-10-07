
using Library_App_task_4_1_2_.Modules.DB;
using Library_App_task_4_1_2_.Modules.Entities;
using Library_App_task_4_1_2_.Modules.InterfaceDAL;
using Library_App_task_4_1_2_.Modules.RepositoryDAL;

namespace Library_App_task_4_1_2_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dBconf  = new ConfigurationBuilder().AddJsonFile("dbsettings.json").Build();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<LibraryDB_Context>((p) =>
            {
                return new LibraryDB_Context(dBconf["ConnectionStrings:DefaultConnection"]);
            });
            builder.Services.AddScoped<IBookRepository, BookRepository>((p) =>
            {
                return new BookRepository(p.GetRequiredService<LibraryDB_Context>());
            });
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>((p) =>
            {
                return new AuthorRepository(p.GetRequiredService<LibraryDB_Context>());
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            
            app.Run();
        }
    }
}
