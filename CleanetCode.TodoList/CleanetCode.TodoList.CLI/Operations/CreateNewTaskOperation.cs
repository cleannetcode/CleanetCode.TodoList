using CleanetCode.TodoList.CLI.Models;
using CleanetCode.TodoList.CLI.Storages;

namespace CleanetCode.TodoList.CLI.Operations
{
    public class CreateNewTaskOperation : IAuthorizedOperation
    {
        public string Name => "Создать новую задачу";

        public bool Execute(Guid userId)
        {
            Console.Write("Введите название задачи: ");
            string? name = Console.ReadLine();

            Console.Write("Введите описание задачи: ");
            string? description = Console.ReadLine();

            var (newTask, error) = TodoTask.Create(name, description, userId);

            if (newTask == null)
            {
                Console.WriteLine(error);
                return false;
            }

            TaskStorage.Add(newTask);

            return true;
        }
    }
}