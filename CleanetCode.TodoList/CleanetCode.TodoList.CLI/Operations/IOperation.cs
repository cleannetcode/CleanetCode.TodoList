namespace CleanetCode.TodoList.CLI.Operations
{
	public interface IOperation
	{
		string Name { get; }
		bool Execute();
	}
}
