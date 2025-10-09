using System.Diagnostics.Contracts;
using Library_App_task_4_1_2_.Modules.DB;
using Library_App_task_4_1_2_.Modules.Entities;
using Library_App_task_4_1_2_.Modules.InterfaceDAL;
using Microsoft.EntityFrameworkCore;

namespace Library_App_task_4_1_2_.Modules.RepositoryDAL
{
    public interface IBookRepository : IGenericBookRepository<Book>;

    public class BookRepository : IBookRepository
    {
        private LibraryDbContext dbContext;

        public BookRepository(LibraryDbContext dB_Context)
        {
            this.dbContext = dB_Context;
        }

        public bool CreateBook(Book newBook)
        {
            try
            {
                this.dbContext.Books.Add(newBook);
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

        public Book? GetBookById(int search)
        {
            return this.dbContext.Books.FirstOrDefault(b => b.Id == search);
        }

        public ICollection<Book> GetBooks()
        {
            return this.dbContext.Books.ToList();
        }

        public bool RemoveBookById(int removalId)
        {
            try
            {
                var removedBook = GetBookById(removalId);
                if (removedBook is null)
                {
                    throw new Exception("No book with this id");
                }
                this.dbContext.Books.Remove(removedBook);
                this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool UpdateBookById(int updateId, Book updatedBook)
        {
            try
            {
                var oldBook = GetBookById(updateId);
                if (oldBook is null)
                {
                    throw new Exception("No book with this id");
                }
                oldBook.Title = updatedBook.Title;
                oldBook.AuthorId = updatedBook.AuthorId;
                oldBook.PublishYear = updatedBook.PublishYear;
                this.dbContext.Books.Update(oldBook);
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
