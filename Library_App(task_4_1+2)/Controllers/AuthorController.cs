using Library_App_task_4_1_2_.Modules.Entities;
using Library_App_task_4_1_2_.Modules.RepositoryDAL;
using Microsoft.AspNetCore.Mvc;

namespace Library_App_task_4_1_2_.Controllers
{
    [ApiController]
    [Route("Author")]
    public class AuthorController : ControllerBase
    {
        private IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public ActionResult<ICollection<Author>> GetAuthors()
        {
            var authors = _authorRepository.GetAuthors();
            return Ok(authors);
        }

        [HttpGet("search")]
        public ActionResult<ICollection<Author>> SearchAuthors([FromQuery] string? querySurname)
        {
            if (string.IsNullOrEmpty(querySurname))
                return BadRequest("Пустой запрос");
            var matchingAuthors = _authorRepository.GetAuthors().Where(a => a.Surname.Contains(querySurname)).ToList();

            return Ok(matchingAuthors);
        }

        [HttpGet("book-count/{count:int}")]
        public ActionResult<ICollection<Author>> GetAuthorsWithNBooks(int queryCount)
        {
            var authors = _authorRepository.GetAuthors().Where(a => a.Books.Count == queryCount);
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Author> GetAuthorById(int searchId)
        {
            var author = _authorRepository.GetAuthorById(searchId);
            if (author == null)
            {
                return BadRequest("Нет автора с таким id");
            }
            return Ok(author);
        }

        [HttpPost]
        public ActionResult CreateAuthor(Author parsedAuthor)
        {
            if (parsedAuthor == null)
                return BadRequest("Автор не может быть null");
            if (parsedAuthor.Surname.Length < 1)
                return BadRequest(
                    "Фамилия автора не может быть пустой (При отсуствии информации указать -- Неизвестный"
                );
            if (parsedAuthor.Name == null)
                return BadRequest("Имя автора не может быть null (Указать \"\")");
            if (parsedAuthor.DateOfBirth > DateOnly.FromDateTime(DateTime.Now))
                return BadRequest("Имя автора не может быть null");
            foreach (var book in parsedAuthor.Books)
            {
                if (book == null)
                    return BadRequest("Книга не может быть null");
                if (book.Title.Length < 1 || book.Title == null)
                    return BadRequest("Название книги не может быть пустым");
            }
            if (_authorRepository.CreateAuthor(parsedAuthor))
                return Ok($"Автор {parsedAuthor.Surname} {parsedAuthor.Name} был успешно добавлен");
            else
                return StatusCode(500, "Произошла ошибка при добавлении");
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteAuthorById(int id)
        {
            if (_authorRepository.GetAuthorById(id) == null)
                return BadRequest("Нет автора с таким id");

            if (_authorRepository.RemoveAuthorById(id))
                return Ok($"Автор был успешно удален");
            else
                return StatusCode(500, "Произошла ошибка при удалении");
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateAuthorById(int updateId, Author parsedAuthor)
        {
            if (_authorRepository.GetAuthorById(updateId) == null)
                return BadRequest("Нет автора с таким id");
            if (parsedAuthor == null)
                return BadRequest("Автор не может быть null");
            if (parsedAuthor.Name == null)
                return BadRequest("Имя автора не может быть null (Указать \"\")");
            if (parsedAuthor.Surname.Length < 1)
                return BadRequest(
                    "Фамилия автора не может быть пустой (При отсуствии информации указать -- Неизвестный"
                );
            if (parsedAuthor.DateOfBirth > DateOnly.FromDateTime(DateTime.Now))
                return BadRequest("Имя автора не может быть null");

            if (_authorRepository.UpdateAuthorById(updateId, parsedAuthor))
                return Ok($"Автор был успешно обновлен");
            else
                return StatusCode(500, "Произошла ошибка при обновлении");
        }
    }
}
