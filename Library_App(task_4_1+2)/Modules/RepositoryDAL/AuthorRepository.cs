using Library_App_task_4_1_2_.Modules.DB;
using Library_App_task_4_1_2_.Modules.Entities;
using Library_App_task_4_1_2_.Modules.InterfaceDAL;
using Microsoft.EntityFrameworkCore;

namespace Library_App_task_4_1_2_.Modules.RepositoryDAL
{
    public interface IAuthorRepository : IGenericAuthorRepository<Author>;
    public class AuthorRepository : IAuthorRepository
    {
        private LibraryDB_Context dB_Context;

        public AuthorRepository(LibraryDB_Context dB_Context)
        {
            this.dB_Context = dB_Context;
        }
        public bool CreateAuthor(Author author)
        {
            try
            {
                this.dB_Context.Authors.Add(author);
                if(author.Books.Count > 0)
                {
                    foreach(var book in author.Books)
                    {
                        book.AuthorId = author.Id;
                        this.dB_Context.Books.Add(book);
                    }
                }
                this.dB_Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public void Dispose() { }

        public Author? GetAuthorById(int id)
        {
            return this.dB_Context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Author> GetAuthors()
        {
            return this.dB_Context.Authors.Include(a => a.Books).ToList();
        }

        public bool RemoveAuthorById(int id)
        {
            try
            {
                var author = GetAuthorById(id);
                if (author is null) { throw new Exception("No author with this id"); }
                this.dB_Context.Authors.Remove(author);
                this.dB_Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool UpdateAuthorById(int id, Author author)
        {
            try
            {
                var upd_author = GetAuthorById(id);
                if (upd_author is null) { throw new Exception("No author with this id"); }

                upd_author.Surname = author.Surname;
                upd_author.Name = author.Name;
                upd_author.DateOfBirth = author.DateOfBirth;
                upd_author.Books = author.Books;

                this.dB_Context.Authors.Update(upd_author);
                this.dB_Context.SaveChanges();
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
