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
        public ActionResult<ICollection<Author>> SearchAuthors([FromQuery] string? surname)
        {
            if (string.IsNullOrEmpty(surname)) return BadRequest("Пустой запрос");
            var query = _authorRepository.GetAuthors().Where(a => a.Surname.Contains(surname));

            return Ok(query.ToList());
        }
        
        [HttpGet("book-count/{count:int}")]
        public ActionResult<ICollection<Author>> GetAuthorsWithNBooks(int count)
        {
            var authors = _authorRepository.GetAuthors().Where(a => a.Books.Count == count);
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Author> GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null) { return BadRequest("Нет автора с таким id"); }
            return Ok(author);
        }
        [HttpPost]
        public ActionResult CreateAuthor(Author author)

        {
            if (author == null)
                return BadRequest("Автор не может быть null");
            if (author.Name == null)
                return BadRequest("Имя автора не может быть null");
            if (author.Surname.Length < 1)
                return BadRequest("Фамилия автора не может быть пустой (При отсуствии информации указать -- Неизвестный");
            if (author.Name == null)
                return BadRequest("Имя автора не может быть null (Указать \"\")");
            if (author.DateOfBirth > DateOnly.FromDateTime(DateTime.Now))
                    return BadRequest("Имя автора не может быть null");
            foreach (var book in author.Books)
            {
                if (book == null)
                    return BadRequest("Книга не может быть null");
                if (book.Title.Length < 1 || book.Title == null)
                    return BadRequest("Название книги не может быть пустым");
                
            }
            if (_authorRepository.CreateAuthor(author)) return Ok($"Автор {author.Surname} {author.Name} был успешно добавлен");
            else return StatusCode(500,"Произошла ошибка при добавлении");
        }
        [HttpDelete("{id:int}")]
        public ActionResult DeleteAuthorById(int id)
        {
            if (_authorRepository.GetAuthorById(id) == null)
                return BadRequest("Нет автора с таким id");

            if (_authorRepository.RemoveAuthorById(id)) return Ok($"Автор был успешно удален");
            else return StatusCode(500, "Произошла ошибка при удалении");
        }
        [HttpPut("{id:int}")]
        public ActionResult UpdateAuthorById(int id, Author author)
        {
            if (_authorRepository.GetAuthorById(id) == null)
                return BadRequest("Нет автора с таким id");
            if (author == null)
                return BadRequest("Автор не может быть null");
            if (author.Name == null)
                return BadRequest("Имя автора не может быть null");
            if (author.Surname.Length < 1)
                return BadRequest("Фамилия автора не может быть пустой (При отсуствии информации указать -- Неизвестный");
            if (author.Name == null)
                return BadRequest("Имя автора не может быть null (Указать \"\")");
            if (author.DateOfBirth > DateOnly.FromDateTime(DateTime.Now))
                return BadRequest("Имя автора не может быть null");

            if (_authorRepository.UpdateAuthorById(id, author)) return Ok($"Автор был успешно обновлен");
            else return StatusCode(500, "Произошла ошибка при обновлении");
        }
    }
}
