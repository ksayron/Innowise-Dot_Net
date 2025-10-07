using Library_App_task_4_1_2_.Modules.DB;
using Library_App_task_4_1_2_.Modules.Entities;
using Library_App_task_4_1_2_.Modules.InterfaceDAL;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Library_App_task_4_1_2_.Modules.RepositoryDAL
{
    public interface IBookRepository : IGenericBookRepository<Book>;
    public class BookRepository : IBookRepository
    {
        private LibraryDB_Context dB_Context;

        public BookRepository(LibraryDB_Context dB_Context)
        {
            this.dB_Context = dB_Context;
        }

        public bool CreateBook(Book book)
        {
            try
            {
                this.dB_Context.Books.Add(book);
                this.dB_Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public void Dispose() {}

        public Book? GetBookById(int id)
        {
            return this.dB_Context.Books.FirstOrDefault(b => b.Id==id);
        }

        public ICollection<Book> GetBooks()
        {
            return this.dB_Context.Books.ToList();
        }

        public bool RemoveBookById(int id)
        {
            
            try
            {
                var book = GetBookById(id);
                if (book is null) { throw new Exception("No book with this id"); }
                this.dB_Context.Books.Remove(book);
                this.dB_Context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool UpdateBookById(int id, Book book)
        {
            try
            {
                var upd_book = GetBookById(id);
                if (upd_book is null) { throw new Exception("No book with this id"); }
                upd_book.Title = book.Title;
                upd_book.AuthorId = book.AuthorId;
                upd_book.PublishYear = book.PublishYear;
                this.dB_Context.Books.Update(upd_book);
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
