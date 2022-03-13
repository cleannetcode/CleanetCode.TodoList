namespace CleanetCode.TodoList.CLI.Operations;

public class LogoutUserOperation : IAuthorizedOperation
{
    public string Name => "Выйти из пользователя";
    public bool Execute(Guid userId)
    {
        throw new NotImplementedException();
    }
}