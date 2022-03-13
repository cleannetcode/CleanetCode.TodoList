namespace CleanetCode.TodoList.CLI.Operations;

public interface IAuthorizedOperation
{
    string Name { get; }
    bool Execute(Guid userId);
}