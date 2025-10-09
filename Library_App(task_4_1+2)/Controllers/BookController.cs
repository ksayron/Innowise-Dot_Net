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
        public ActionResult<ICollection<Book>> GetBooksPublishedAfter(int queryYear)
        {
            var matchingBooks = _bookRepository.GetBooks().Where(b => b.PublishYear > queryYear).ToList();
            return Ok(matchingBooks);
        }

        [HttpGet("search")]
        public ActionResult<ICollection<Book>> SearchBooks([FromQuery] string? queryTitle)
        {
            if (string.IsNullOrEmpty(queryTitle))
                return BadRequest("Пустой запрос");
            var matchingBooks = _bookRepository.GetBooks().Where(b => b.Title.Contains(queryTitle)).ToList();

            return Ok(matchingBooks);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Book> GetBookById(int searchId)
        {
            var book = _bookRepository.GetBookById(searchId);
            if (book is not null)
                return Ok(book);
            return BadRequest($"Нет книги с id:{searchId}");
        }

        [HttpPost(Name = "AddBook")]
        public ActionResult CreateBook(Book parsedBook)
        {
            if (parsedBook == null)
                return BadRequest("Книга не может быть null");
            if (parsedBook.Title == null || parsedBook.Title.Length < 1)
                return BadRequest("Название книги не может быть пустым");
            if (_authorRepository.GetAuthorById(parsedBook.AuthorId) is null)
                return BadRequest("У книги обязан быть автор из имеющихся в реестре");

            if (_bookRepository.CreateBook(parsedBook))
                return Ok($"Книга {parsedBook.Title} был успешно добавлен");
            else
                return StatusCode(500, "Произошла ошибка при добавлении");
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteBookById(int deletionId)
        {
            if (_bookRepository.GetBookById(deletionId) is null)
                return BadRequest("Нет книги с таким id");

            if (_bookRepository.RemoveBookById(deletionId))
                return Ok($"Книга с id:{deletionId} была успешно удалена");
            else
                return StatusCode(500, "Произошла ошибка при удалении");
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateBookById(int updateId, Book parsedBook)
        {
            if (_bookRepository.GetBookById(updateId) is null)
                return BadRequest("Нет книги с таким id");
            if (parsedBook == null)
                return BadRequest("Книга не может быть null");
            if (parsedBook.Title.Length < 1 || parsedBook.Title == null)
                return BadRequest("Название книги не может быть пустым");
            if (_authorRepository.GetAuthorById(parsedBook.AuthorId) is null)
                return BadRequest("У книги обязан быть автор из имеющихся в реестре");

            if (_bookRepository.UpdateBookById(updateId, parsedBook))
                return Ok($"Книга с id:{updateId} была успешно обновлена");
            else
                return StatusCode(500, "Произошла ошибка при обновлении");
        }
    }
}
