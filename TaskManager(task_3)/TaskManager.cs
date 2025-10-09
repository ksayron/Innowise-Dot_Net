namespace TaskManager_task_3_
{
    public class TaskManager
    {
        public List<Task> Tasks { get; set; } = [];

        public TaskManager()
        {
            var repo = new TaskRepository();
            try
            {
                Tasks = new List<Task>(repo.GetTasks()); //клонируем всю коллекцию обьект а не только ссылку на нее
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowTasks()
        {
            Console.WriteLine();
            foreach (var task in Tasks)
            {
                Console.WriteLine(task.Id + $" - {task.Title},{task.IsCompleted},{task.CreatedAt}");
                Console.WriteLine(task.Description + '\n');
            }
        }

        public void AddTask()
        {
            var new_task = new Task();

            while (new_task.Title == "")
            {
                Console.WriteLine("\nВведите название задания:");
                string str = Console.ReadLine();
                if (str != null)
                    new_task.Title = str;
            }

            while (new_task.Description == "")
            {
                Console.WriteLine("\nВведите описание задания:");
                string str = Console.ReadLine();
                if (str != null)
                    new_task.Description = str;
            }

            var repo = new TaskRepository();
            try
            {
                repo.CreateTask(new_task);
                Tasks = new List<Task>(repo.GetTasks());
                Console.WriteLine("\nЗадание добавлено\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CompleteTaskById()
        {
            int id;
            var repo = new TaskRepository();
            while (true)
            {
                Console.WriteLine("Введите номер выполненного задания (например: 1):");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    if (repo.GetTaskById(id) is not null)
                        break;
                    else
                        Console.WriteLine("\nНет задания с таким номером, попробуйте снова.");
                }
                else
                    Console.WriteLine("\nНеверный формат числа, попробуйте снова.");
            }

            try
            {
                repo.CompleteTask(id);
                Tasks = new List<Task>(repo.GetTasks());
                Console.WriteLine("\nЗадание помеченно выполненым\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteTaskById()
        {
            int id;
            var repo = new TaskRepository();
            while (true)
            {
                Console.WriteLine("Введите номер выполненного задания (например: 1):");
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    if (repo.GetTaskById(id) is not null)
                        break;
                    else
                        Console.WriteLine("\nНет задания с таким номером, попробуйте снова.");
                }
                else
                    Console.WriteLine("\nНеверный формат числа, попробуйте снова.");
            }

            try
            {
                repo.DeleteTask(id);
                Tasks = new List<Task>(repo.GetTasks());
                Console.WriteLine("\nЗадание удалено\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
