using System.Diagnostics;

namespace Async_Data_Processor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch processingTimer = new Stopwatch();
            Console.WriteLine("Синхронная обработка:");
            processingTimer.Start();
            ProcessingDataSync();
            processingTimer.Stop();
            Console.WriteLine($"Обработка заняла : {processingTimer.Elapsed.TotalSeconds} секунд");

            Console.WriteLine("\nАсинхронная обработка:");
            processingTimer.Restart();
            processingTimer.Start();
            await ProcessingDataAsync();
            processingTimer.Stop();

            Console.WriteLine($"Обработка заняла : {processingTimer.Elapsed.TotalSeconds} секунд");
        }

        public static void ProcessJSON(string fileName)
        {
            Console.WriteLine($" Начало обработки {fileName}");
            Thread.Sleep(3000);
            Console.WriteLine($" {fileName} успешно обработан!");
        }

        public static async Task ProcessJSONAsync(string fileName)
        {
            Console.WriteLine($" Начало обработки {fileName}");
            await Task.Delay(3000);
            Console.WriteLine($" {fileName} успешно обработан!");
        }

        public static void ProcessingDataSync()
        {
            ProcessJSON("dataset1.json");
            ProcessJSON("dataset2.json");
            ProcessJSON("dataset3.json");
        }

        public static async Task ProcessingDataAsync()
        {
            var processingTask1 = ProcessJSONAsync("dataset1.json");
            var processingTask2 = ProcessJSONAsync("dataset2.json");
            var processingTask3 = ProcessJSONAsync("dataset3.json");

            await Task.WhenAll(processingTask1, processingTask2, processingTask3);
        }
    }
}
