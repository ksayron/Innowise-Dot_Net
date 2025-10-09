namespace TaskManager_task_3_
{
    internal class Program
    {
        static int Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            Console.WriteLine("Добро пожаловать в TaskManager.");
            string? actionType = "0";
            while (true)
            {
                Console.WriteLine(
                    "\nВыберите операцию\n\n"
                        + "1 - Показать задания\n"
                        + "2 - Добавить задание\n"
                        + "3 - Пометить задачу выполненной\n"
                        + "4 - Удалить задачу\n"
                        + "0 - Выход из программы"
                );
                actionType = Console.ReadLine();
                switch (actionType)
                {
                    case "1":
                    {
                        taskManager.ShowTasks();
                        break;
                    }
                    case "2":
                    {
                        taskManager.AddTask();
                        break;
                    }
                    case "3":
                    {
                        taskManager.CompleteTaskById();
                        break;
                    }
                    case "4":
                    {
                        taskManager.DeleteTaskById();
                        break;
                    }
                    case "0":
                    {
                        Console.WriteLine("Спасибо за использование TaskManager.");
                        return 0;
                    }
                    default:
                        Console.WriteLine("Некорректный ввод");
                        break;
                }
            }
        }
    }
}
