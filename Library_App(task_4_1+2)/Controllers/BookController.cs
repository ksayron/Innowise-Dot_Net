using Library_App_task_4_1_2_.Modules.Entities;
using Library_App_task_4_1_2_.Modules.RepositoryDAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_App_task_4_1_2_.Controllers
{
    [Route("Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        [HttpGet(Name = "GetBooks")]
        public ActionResult<ICollection<Book>> GetBooks()
        {
            var books = _bookRepository.GetBooks();
            return Ok(books);
        }

        [HttpGet("after/{year:int}")]
        public ActionResult<ICollection<Book>> GetBooksPublishedAfter(int year)
        {
            var books = _bookRepository.GetBooks().Where(b => b.PublishYear > year);
            return Ok(books);
        }

        [HttpGet("search")]
        public ActionResult<ICollection<Book>> SearchBooks([FromQuery] string? title)
        {
            if (string.IsNullOrEmpty(title)) return BadRequest("Пустой запрос");
            var query = _bookRepository.GetBooks().Where(b => b.Title.Contains(title));
            
            return Ok(query.ToList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book is not null) return Ok(book);
            return BadRequest($"Нет книги с id:{id}");
        }

        [HttpPost(Name = "AddBook")]
        public ActionResult CreateBook(Book book)
        {
            if (book == null)
                return BadRequest("Книга не может быть null");
            if ( book.Title == null || book.Title.Length < 1)
                return BadRequest("Название книги не может быть пустым");
            if (_authorRepository.GetAuthorById(book.AuthorId) is null)
                return BadRequest("У книги обязан быть автор из имеющихся в реестре");

            if (_bookRepository.CreateBook(book)) return Ok($"Книга {book.Title} был успешно добавлен");
            else return StatusCode(500, "Произошла ошибка при добавлении");
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteBookById(int id)
        {
            if (_bookRepository.GetBookById(id) is null)
                return BadRequest("Нет книги с таким id");

            if (_bookRepository.RemoveBookById(id)) return Ok($"Книга с id:{id} была успешно удалена");
            else return StatusCode(500, "Произошла ошибка при удалении");
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateBookById(int id, Book book)
        {
            if (_bookRepository.GetBookById(id) is null)
                return BadRequest("Нет книги с таким id");
            if (book == null)
                return BadRequest("Книга не может быть null");
            if (book.Title.Length < 1 || book.Title == null)
                return BadRequest("Название книги не может быть пустым");
            if (_authorRepository.GetAuthorById(book.AuthorId) is null)
                return BadRequest("У книги обязан быть автор из имеющихся в реестре");

            if (_bookRepository.UpdateBookById(id, book)) return Ok($"Книга с id:{id} была успешно обновлена");
            else return StatusCode(500, "Произошла ошибка при обновлении");
        }
    }
}
