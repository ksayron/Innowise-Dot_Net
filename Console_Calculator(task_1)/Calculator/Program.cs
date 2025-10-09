namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double firstInput,
                secondInput;
            string actionType;
            do
            {
                firstInput = NumberInput();
                actionType = ActionInput();
                secondInput = NumberInput();

                Calculate(firstInput, secondInput, actionType);
                actionType = CalcActionInput();
            } while (actionType == "1");
        }

        public static double NumberInput()
        {
            double numInput;
            while (true)
            {
                Console.WriteLine("Введите число (например: 1,1):");
                if (double.TryParse(Console.ReadLine(), out numInput))
                    return numInput;
                Console.WriteLine("Неверный формат числа, попробуйте снова.");
            }
        }

        public static string ActionInput()
        {
            string actionType = "";
            bool acceptedFormat = false;
            while (!acceptedFormat)
            {
                Console.WriteLine("Выберите операцию +,-,*,/");
                actionType = Console.ReadLine();
                if (
                    actionType != "+"
                    && actionType != "-"
                    && actionType != "*"
                    && actionType != "/"
                )
                {
                    Console.WriteLine("Данные введены в неверном формате! Повторите ввод.");
                }
                else
                {
                    acceptedFormat = true;
                }
            }
            return actionType;
        }

        public static string CalcActionInput()
        {
            string actionType = "";
            bool acceptedFormat = false;
            while (!acceptedFormat)
            {
                Console.WriteLine("Выберите операцию 1 - новая операция, 0 - закончить работу");
                actionType = Console.ReadLine();
                if (actionType != "1" && actionType != "0")
                {
                    Console.WriteLine("Данные введены в неверном формате! Повторите ввод.");
                }
                else
                {
                    acceptedFormat = true;
                }
            }
            return actionType;
        }

        public static void Calculate(double firstVariable, double secondVariable, string actionType)
        {
            Console.WriteLine("Результат:");
            switch (actionType)
            {
                case "+":
                {
                    Console.WriteLine(firstVariable + secondVariable);
                    break;
                }
                case "-":
                {
                    Console.WriteLine(firstVariable - secondVariable);
                    break;
                }
                case "*":
                {
                    Console.WriteLine(firstVariable * secondVariable);
                    break;
                }
                case "/":
                {
                    if (secondVariable == 0)
                    {
                        Console.WriteLine("Невозможно делить на ноль!");
                        break;
                    }
                    Console.WriteLine(firstVariable / secondVariable);
                    break;
                }
            }
        }
    }
}
