using CleanetCode.TodoList.CLI.Models;
using CleanetCode.TodoList.CLI.Storages;

namespace CleanetCode.TodoList.CLI.Operations
{
	public class CreateNewUserOperation : IOperation
	{
		public string Name => "Создать нового пользователя";

		public bool Execute()
		{
			Console.Write("Введите ваш email:");
			string? userInput = Console.ReadLine();
			var (email, error) = Email.Create(userInput);
            if (email == null)
            {
                Console.WriteLine(error);
                return false;
            }

			User newUser = new User
			{
				Email = email
			};

			bool userCreated = UserStorage.Create(newUser);
			if (!userCreated)
			{
				Console.WriteLine("Пользователь с таким email уже есть");
				return false;
			}

			return true;
		}
	}
}

