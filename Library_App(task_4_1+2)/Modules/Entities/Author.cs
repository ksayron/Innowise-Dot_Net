using System.ComponentModel.DataAnnotations.Schema;

namespace Library_App_task_4_1_2_.Modules.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; } = [];

        public Author()
        {
            Name = "";
            Surname = "";
            Books = [];
        }
    }
}
