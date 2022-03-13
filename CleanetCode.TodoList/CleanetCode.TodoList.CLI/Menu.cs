using CleanetCode.TodoList.CLI.Operations;

namespace CleanetCode.TodoList.CLI
{
    public class Menu
    {
        private IOperation[] _operations;
        private readonly IAuthorizedOperation[] _authorizedOperations;

        public Menu(IOperation[] operations, IAuthorizedOperation[] authorizedOperations)
        {
            _operations = operations;
            _authorizedOperations = authorizedOperations;
        }

        public string[] GetOperationNames()
        {
            List<string> operationNames = new List<string>();

            if (UserSession.CurrentUser != null)
            {
                for (int i = 0; i < _authorizedOperations.Length; i++)
                {
                    IAuthorizedOperation operation = _authorizedOperations[i];
                    operationNames.Add($"{i} - {operation.Name}");
                }
            }
            else
            {
                for (int i = 0; i < _operations.Length; i++)
                {
                    IOperation operation = _operations[i];
                    operationNames.Add($"{i} - {operation.Name}");
                }
            }

            return operationNames.ToArray();
        }

        public (bool Result, string Error) Enter(int operationNumber)
        {
            if (UserSession.CurrentUser != null)
            {
                if (operationNumber < 0 || operationNumber >= _authorizedOperations.Length)
                {
                    return (false, "Некорректная операция: " + operationNumber);
                }

                bool result = _authorizedOperations[operationNumber].Execute(UserSession.CurrentUser.Id);
                return (result, string.Empty);
            }
            else
            {
                if (operationNumber < 0 || operationNumber >= _operations.Length)
                {
                    return (false, "Некорректная операция: " + operationNumber);
                }

                bool result = _operations[operationNumber].Execute();
                return (result, string.Empty);
            }
        }
    }
}