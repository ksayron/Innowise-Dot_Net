using System.Globalization;
using Library_App_task_4_1_2_.Modules.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library_App_task_4_1_2_.Modules.DB
{
    public class LibraryDbContext : DbContext
    {
        private string _connString;

        public LibraryDbContext(string connString)
            : base()
        {
            _connString = connString;
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer(_connString)
                .UseSeeding(
                    (context, _) =>
                    {
                        if (!context.Set<Author>().Any())
                        {
                            var authors = new List<Author>
                            {
                                new Author
                                {
                                    Name = "Лев",
                                    Surname = "Толстой",
                                    DateOfBirth = new DateOnly(1828, 9, 9),
                                },
                                new Author
                                {
                                    Name = "Фёдор",
                                    Surname = "Достоевский",
                                    DateOfBirth = new DateOnly(1821, 11, 11),
                                },
                                new Author
                                {
                                    Name = "Антон",
                                    Surname = "Чехов",
                                    DateOfBirth = new DateOnly(1860, 1, 29),
                                },
                                new Author
                                {
                                    Name = "Александр",
                                    Surname = "Пушкин",
                                    DateOfBirth = new DateOnly(1799, 6, 6),
                                },
                                new Author
                                {
                                    Name = "Михаил",
                                    Surname = "Булгаков",
                                    DateOfBirth = new DateOnly(1891, 5, 15),
                                },
                                new Author
                                {
                                    Name = "Иван",
                                    Surname = "Тургенев",
                                    DateOfBirth = new DateOnly(1818, 11, 9),
                                },
                                new Author
                                {
                                    Name = "Николай",
                                    Surname = "Гоголь",
                                    DateOfBirth = new DateOnly(1809, 4, 1),
                                },
                            };

                            context.Set<Author>().AddRange(authors);
                            context.SaveChanges();
                        }

                        if (!context.Set<Book>().Any())
                        {
                            var authors = context.Set<Author>().ToList();

                            var books = new List<Book>
                            {
                                new Book
                                {
                                    Title = "Война и мир",
                                    PublishYear = 1869,
                                    AuthorId = authors.First(a => a.Surname == "Толстой").Id,
                                },
                                new Book
                                {
                                    Title = "Анна Каренина",
                                    PublishYear = 1877,
                                    AuthorId = authors.First(a => a.Surname == "Толстой").Id,
                                },
                                new Book
                                {
                                    Title = "Преступление и наказание",
                                    PublishYear = 1866,
                                    AuthorId = authors.First(a => a.Surname == "Достоевский").Id,
                                },
                                new Book
                                {
                                    Title = "Идиот",
                                    PublishYear = 1869,
                                    AuthorId = authors.First(a => a.Surname == "Достоевский").Id,
                                },
                                new Book
                                {
                                    Title = "Братья Карамазовы",
                                    PublishYear = 1880,
                                    AuthorId = authors.First(a => a.Surname == "Достоевский").Id,
                                },
                                new Book
                                {
                                    Title = "Вишнёвый сад",
                                    PublishYear = 1904,
                                    AuthorId = authors.First(a => a.Surname == "Чехов").Id,
                                },
                                new Book
                                {
                                    Title = "Три сестры",
                                    PublishYear = 1901,
                                    AuthorId = authors.First(a => a.Surname == "Чехов").Id,
                                },
                                new Book
                                {
                                    Title = "Евгений Онегин",
                                    PublishYear = 1833,
                                    AuthorId = authors.First(a => a.Surname == "Пушкин").Id,
                                },
                                new Book
                                {
                                    Title = "Капитанская дочка",
                                    PublishYear = 1836,
                                    AuthorId = authors.First(a => a.Surname == "Пушкин").Id,
                                },
                                new Book
                                {
                                    Title = "Мастер и Маргарита",
                                    PublishYear = 1967,
                                    AuthorId = authors.First(a => a.Surname == "Булгаков").Id,
                                },
                                new Book
                                {
                                    Title = "Собачье сердце",
                                    PublishYear = 1925,
                                    AuthorId = authors.First(a => a.Surname == "Булгаков").Id,
                                },
                                new Book
                                {
                                    Title = "Отцы и дети",
                                    PublishYear = 1862,
                                    AuthorId = authors.First(a => a.Surname == "Тургенев").Id,
                                },
                                new Book
                                {
                                    Title = "Записки охотника",
                                    PublishYear = 1852,
                                    AuthorId = authors.First(a => a.Surname == "Тургенев").Id,
                                },
                                new Book
                                {
                                    Title = "Мёртвые души",
                                    PublishYear = 1842,
                                    AuthorId = authors.First(a => a.Surname == "Гоголь").Id,
                                },
                                new Book
                                {
                                    Title = "Ревизор",
                                    PublishYear = 1836,
                                    AuthorId = authors.First(a => a.Surname == "Гоголь").Id,
                                },
                                new Book
                                {
                                    Title = "Шинель",
                                    PublishYear = 1842,
                                    AuthorId = authors.First(a => a.Surname == "Гоголь").Id,
                                },
                                new Book
                                {
                                    Title = "Воскресение",
                                    PublishYear = 1899,
                                    AuthorId = authors.First(a => a.Surname == "Толстой").Id,
                                },
                                new Book
                                {
                                    Title = "Каштанка",
                                    PublishYear = 1887,
                                    AuthorId = authors.First(a => a.Surname == "Чехов").Id,
                                },
                                new Book
                                {
                                    Title = "Пиковая дама",
                                    PublishYear = 1834,
                                    AuthorId = authors.First(a => a.Surname == "Пушкин").Id,
                                },
                                new Book
                                {
                                    Title = "Белая гвардия",
                                    PublishYear = 1925,
                                    AuthorId = authors.First(a => a.Surname == "Булгаков").Id,
                                },
                            };

                            context.Set<Book>().AddRange(books);
                            context.SaveChanges();
                        }
                    }
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books").HasKey(b => b.Id);
            modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired().HasMaxLength(35);
            modelBuilder.Entity<Book>().Property(b => b.PublishYear);

            modelBuilder.Entity<Author>().ToTable("Authors").HasKey(a => a.Id);
            modelBuilder.Entity<Author>().Property(a => a.Surname).HasMaxLength(15);
            modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(15);
            modelBuilder.Entity<Author>().Property(a => a.DateOfBirth);
            modelBuilder
                .Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne()
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
