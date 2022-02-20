using CleanetCode.TodoList.CLI.Models;
using CleanetCode.TodoList.CLI.Storages;

namespace CleanetCode.TodoList.CLI.Operations
{
	public class LoginUserOperation : IOperation
	{
		public string Name => "Залогиниться в системе";

		public void Execute()
		{
			Console.Write("Введите ваш email:");
			string? email = Console.ReadLine();
			User? user = UserStorage.Get(email);

			if (user == null)
			{
				UserSession.CurrentUser = user;
			}
		}
	}
}
