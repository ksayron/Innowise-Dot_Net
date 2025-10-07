using System.ComponentModel.DataAnnotations.Schema;

namespace Library_App_task_4_1_2_.Modules.Entities
{
    
    public class Book 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublishYear { get; set; }
        public int AuthorId { get; set; }
        public Book()
        {
            Title = "";
            PublishYear = 0;
        }
      
    }
}
