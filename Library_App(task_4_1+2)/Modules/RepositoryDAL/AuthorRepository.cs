using Library_App_task_4_1_2_.Modules.DB;
using Library_App_task_4_1_2_.Modules.Entities;
using Library_App_task_4_1_2_.Modules.InterfaceDAL;
using Microsoft.EntityFrameworkCore;

namespace Library_App_task_4_1_2_.Modules.RepositoryDAL
{
    public interface IAuthorRepository : IGenericAuthorRepository<Author>;

    public class AuthorRepository : IAuthorRepository
    {
        private LibraryDbContext dbContext;

        public AuthorRepository(LibraryDbContext dB_Context)
        {
            this.dbContext = dB_Context;
        }

        public bool CreateAuthor(Author newAuthor)
        {
            try
            {
                this.dbContext.Authors.Add(newAuthor);
                this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public void Dispose() { }

        public Author? GetAuthorById(int searchId)
        {
            return this.dbContext.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == searchId);
        }

        public ICollection<Author> GetAuthors()
        {
            return this.dbContext.Authors.Include(a => a.Books).ToList();
        }

        public bool RemoveAuthorById(int removalId)
        {
            try
            {
                var removedAuthor = GetAuthorById(removalId);
                if (removedAuthor is null)
                {
                    throw new Exception("No author with this id");
                }
                this.dbContext.Authors.Remove(removedAuthor);
                this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool UpdateAuthorById(int updateId, Author newAuthor)
        {
            try
            {
                var oldAuthor = GetAuthorById(updateId);
                if (oldAuthor is null)
                {
                    throw new Exception("No author with this id");
                }

                oldAuthor.Surname = newAuthor.Surname;
                oldAuthor.Name = newAuthor.Name;
                oldAuthor.DateOfBirth = newAuthor.DateOfBirth;
                oldAuthor.Books = newAuthor.Books;

                this.dbContext.Authors.Update(oldAuthor);
                this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
