using CleanetCode.TodoList.CLI.Models;
using CleanetCode.TodoList.CLI.Storages;

namespace CleanetCode.TodoList.CLI.Operations
{
    public class ReadAllTasksOperation : IAuthorizedOperation
    {
        private readonly Menu _menu;

        public ReadAllTasksOperation(Menu menu)
        {
            _menu = menu;
        }

        public string Name => "Вывести список задач";

        public bool Execute(Guid userId)
        {
            bool userQuit = false;
            const string quitKey = "q";

            while (!userQuit)
            {
                TodoTask[]? tasks = TaskStorage.Get(userId);

                if (tasks == null || tasks.Length == 0)
                {
                    Console.WriteLine("Задач нет, добавьте новые через меню");
                }
                else
                {
                    foreach (var task in tasks)
                    {
                        Console.WriteLine(
                            $"Id: {task.Id}, Name: {task.Name}, Description: {task.Description}, IsDone: {task.IsCompleted}");
                    }

                    Console.WriteLine();
                }

                List<string> operationNames = new List<string>();
                operationNames.Add(quitKey + " - вернуться назад");
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

            return true;
        }
    }
}