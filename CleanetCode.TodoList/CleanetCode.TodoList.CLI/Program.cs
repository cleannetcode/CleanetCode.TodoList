using CleanetCode.TodoList.CLI;
using CleanetCode.TodoList.CLI.Operations;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

IOperation[] operations = new IOperation[]
{
	new LoginUserOperation(),
	new CreateNewUserOperation()
};

Menu menu = new Menu(operations);
Application application = new Application(menu);
application.Run();
