using System.Diagnostics;

namespace Async_Data_Processor
{
    class Program
    {
        async static Task Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            Console.WriteLine("Синхронная обработка:");
            timer.Start();
            TestingData();
            timer.Stop();
            Console.WriteLine($"Обработка заняла : {timer.Elapsed.TotalSeconds} секунд");

            Console.WriteLine("\nАсинхронная обработка:");
            timer.Restart();
            timer.Start();
            await TestingDataAsync(); 
            timer.Stop();
            
            Console.WriteLine($"Обработка заняла : {timer.Elapsed.TotalSeconds} секунд");
        }

        public static void ProcessData(string Data_Name)
        {
            Console.WriteLine($" Начало обработки {Data_Name}");
            Thread.Sleep(3000);
            Console.WriteLine($" {Data_Name} успешно обработан!");
        }
        async public static Task ProcessDataAsync(string Data_Name)
        {
            Console.WriteLine($" Начало обработки {Data_Name}");
            await Task.Delay(3000); //передаем управление далее синхронному коду,пока не выполнится таск
            Console.WriteLine($" {Data_Name} успешно обработан!");
        }
        public static void TestingData()
        {

            ProcessData("dataset1.json");
            ProcessData("dataset2.json");
            ProcessData("dataset3.json");

        }
        public async static Task TestingDataAsync()
        {
            
            var task1 = ProcessDataAsync("dataset1.json");
            var task2 = ProcessDataAsync("dataset2.json");
            var task3 = ProcessDataAsync("dataset3.json");

            await Task.WhenAll(task1, task2, task3);//таск выполнится при завершении всех вложенных
            
        }
    }
}
