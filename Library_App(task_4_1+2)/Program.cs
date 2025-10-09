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
            var dBconf = new ConfigurationBuilder().AddJsonFile("dbsettings.json").Build();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<LibraryDbContext>(
                (p) =>
                {
                    return new LibraryDbContext(dBconf["ConnectionStrings:DefaultConnection"]);
                }
            );
            builder.Services.AddScoped<IBookRepository, BookRepository>(
                (p) =>
                {
                    return new BookRepository(p.GetRequiredService<LibraryDbContext>());
                }
            );
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>(
                (p) =>
                {
                    return new AuthorRepository(p.GetRequiredService<LibraryDbContext>());
                }
            );

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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
