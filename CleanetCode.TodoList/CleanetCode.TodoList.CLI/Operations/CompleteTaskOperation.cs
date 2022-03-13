using CleanetCode.TodoList.CLI.Models;
using CleanetCode.TodoList.CLI.Storages;

namespace CleanetCode.TodoList.CLI.Operations
{
    public class CompleteTaskOperation : IAuthorizedOperation
    {
        public string Name => "Завершить задачу";

        public bool Execute(Guid userId)
        {
            TodoTask[]? tasks = TaskStorage.Get(userId);

            if (tasks == null || tasks.Length == 0)
            {
                Console.WriteLine("Задач нет, добавьте новые через меню");
                return true;
            }

            for (var i = 0; i < tasks.Length; i++)
            {
                var task = tasks[i];
                Console.WriteLine($"#{i} - Name: {task.Name}, IsDone: {task.IsCompleted}");
            }

            Console.WriteLine();

            Console.Write("Введите номер задачи для выполнения: ");
            string? userInput = Console.ReadLine();
            bool isNumber = int.TryParse(userInput, out int taskNumber);

            if (!isNumber)
            {
                Console.WriteLine("Неверный номер задачи: " + userInput);
                return false;
            }

            TodoTask taskToUpdate = tasks[taskNumber];
            taskToUpdate.IsCompleted = true;
            
            TaskStorage.Update(userId, taskToUpdate);
            return true;
        }
    }
}