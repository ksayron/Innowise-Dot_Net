namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double var1, var2;
            string action;


            do
            {
                var1 = NumberInput();
                action = ActionInput();
                var2 = NumberInput();

                Calculate(var1, var2, action);
                action = CalcActionInput();
            } while (action == "1");
           
        }
        public static double NumberInput()
        {
            
            double operation;
            while (true)
            {
                Console.WriteLine("Введите число (например: 1,1):");
                if (  double.TryParse(Console.ReadLine(),out operation))
                    return operation;
                Console.WriteLine("Неверный формат числа, попробуйте снова.");

            }
        }
        public static string ActionInput()
        {
            string  action = "";
            bool accepted_format = false;
            while (!accepted_format)
            {
                Console.WriteLine("Выберите операцию +,-,*,/");
                action = Console.ReadLine();
                if (action != "+" && action != "-" && action != "*" && action != "/")
                {
                    Console.WriteLine("Данные введены в неверном формате! Повторите ввод.");
                }
                else
                {
                    accepted_format = true;
                }
                
            }
            return action;
        }
        public static string CalcActionInput()
        {
            
            string action = "";
            bool accepted_format = false;
            while (!accepted_format)
            {
                Console.WriteLine("Выберите операцию 1 - новая операция, 0 - закончить работу");
                action = Console.ReadLine();
                if (action != "1" && action != "0")
                {
                    Console.WriteLine("Данные введены в неверном формате! Повторите ввод.");
                }
                else
                {
                    accepted_format = true;
                }
                
            }
            return action;
        }
        public static void Calculate(double? x,double? y,string action_type)
        {
            Console.WriteLine("Результат:");
            switch (action_type)
            {

                case "+": Console.WriteLine(x + y); break;
                case "-": Console.WriteLine(x - y); break;
                case "*": Console.WriteLine(x * y); break;
                case "/":
                    {
                        if (y == 0) { Console.WriteLine("Невозможно делить на ноль!");break; }
                        Console.WriteLine(x / y); break;
                    }
            }
        }

    }

   
}
