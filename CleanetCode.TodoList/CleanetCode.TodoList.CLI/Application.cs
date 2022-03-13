namespace CleanetCode.TodoList.CLI
{
    public class Application
    {
        private readonly Menu _menu;

        public Application(Menu menu)
        {
            _menu = menu;
        }

        public void Run()
        {
            bool userQuit = false;
            const string quitKey = "q";

            while (!userQuit)
            {
                List<string> operationNames = new List<string>();
                operationNames.Add(quitKey + " - выйти из программы");
                operationNames.AddRange(_menu.GetOperationNames());

                Console.WriteLine(string.Join("\n", operationNames));
                Console.Write("Введите номер операции: ");

                string? userInput = Console.ReadLine();
                if (userInput != null && userInput.Trim().ToLower() == quitKey)
                {
                    userQuit = true;
                    continue;
                }

                bool isNumber = int.TryParse(userInput, out int operationNumber);
                if (isNumber)
                {
                    Console.Clear();
                    var (isSuccess, error) = _menu.Enter(operationNumber);

                    Console.WriteLine(isSuccess ? "Операция прошла успешно" : error);
                }
                else
                {
                    Console.WriteLine("Вы ввели не число: " + userInput);
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}