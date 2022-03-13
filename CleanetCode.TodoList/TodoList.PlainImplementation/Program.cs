using System.Text;

Console.OutputEncoding = Encoding.UTF8;

bool isQuit = false;
User? currentUser = null;
Dictionary<Guid, List<Task>> usersTasks = new();
Dictionary<string, User> users = new();
const string quitKey = "q";

string[] taskOperations =
{
    Operations.Task.CreateTask,
    Operations.Task.UpdateTask,
    Operations.Task.DeleteTask,
    Operations.Task.CompleteTask,
    Operations.Task.Back
};

while (!isQuit)
{
    StringBuilder menuBuilder = new StringBuilder("Меню программы:")
        .Append(Environment.NewLine)
        .Append(quitKey).Append(" - ").Append(Operations.Quit)
        .Append(Environment.NewLine);

    string[] operations;
    if (currentUser is null)
    {
        operations = new[]
        {
            Operations.User.Login,
            Operations.User.CreateNewUser
        };
    }
    else
    {
        operations = new[]
        {
            Operations.User.Logout,
            Operations.Task.GetTasks
        };
    }

    for (int i = 0; i < operations.Length; i++)
    {
        menuBuilder.Append(i).Append(" - ").Append(operations[i])
            .Append(Environment.NewLine);
    }

    Console.WriteLine(menuBuilder.ToString());
    string? userInput = Console.ReadLine();

    if (userInput?.Trim().ToLower() == quitKey)
    {
        isQuit = true;
        continue;
    }

    bool isOperationNumber = int.TryParse(userInput, out int operationNumber);
    string? operation = isOperationNumber && 0 <= operationNumber && operationNumber < operations.Length
        ? operations[operationNumber]
        : userInput;

    switch (operation)
    {
        case Operations.User.Login:
        {
            Console.Write("Введите Email для входа: ");
            string? email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Некорретный email");
            }
            else
            {
                bool userFound = users.TryGetValue(email, out currentUser);
                if (!userFound)
                {
                    Console.WriteLine("Пользователь не найден");
                }
                else
                {
                    Console.WriteLine("Вы успешно зашли в систему!");
                }
            }

            break;
        }
        case Operations.User.Logout:
        {
            currentUser = null;
            break;
        }
        case Operations.User.CreateNewUser:
        {
            Console.Write("Введите Email: ");
            string? email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Некорретный email");
            }
            else
            {
                User newUser = new User(Guid.NewGuid(), email);
                users.Add(newUser.Email, newUser);
            }

            break;
        }
        case Operations.Task.GetTasks:
        {
            if (currentUser is null)
            {
                Console.WriteLine("Чтобы просмотреть список задач, нужно зайти под пользователем");
            }
            else
            {
                if (!usersTasks.TryGetValue(currentUser.Id, out List<Task>? tasks))
                {
                    tasks = new List<Task>();
                    usersTasks.Add(currentUser.Id, tasks);
                }

                bool isContinue = true;
                while (isContinue)
                {
                    foreach (var task in tasks)
                    {
                        Console.WriteLine(task);
                    }

                    StringBuilder taskMenu = new StringBuilder();
                    for (int i = 0; i < taskOperations.Length; i++)
                    {
                        taskMenu.Append(i).Append(" - ").Append(taskOperations[i])
                            .Append(Environment.NewLine);
                    }

                    Console.WriteLine(taskMenu.ToString());

                    userInput = Console.ReadLine();
                    isOperationNumber = int.TryParse(userInput, out operationNumber);
                    operation = isOperationNumber && 0 <= operationNumber && operationNumber < taskOperations.Length
                        ? taskOperations[operationNumber]
                        : userInput;

                    switch (operation)
                    {
                        case Operations.Task.CreateTask:
                        {
                            if (currentUser is null)
                            {
                                Console.WriteLine("Чтобы просмотреть список задач, нужно зайти под пользователем");
                            }
                            else
                            {
                                Console.Write("Введите название задачи:");
                                string? name = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(name))
                                {
                                    Console.WriteLine("Неверное название задачи");
                                    break;
                                }

                                Console.Write("Введите описание задачи:");
                                string? description = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(description))
                                {
                                    Console.WriteLine("Неверное описание задачи");
                                    break;
                                }

                                Task task = new Task(
                                    Id: Guid.NewGuid(),
                                    Name: name,
                                    Description: description,
                                    UserId: currentUser.Id);

                                tasks.Add(task);
                            }

                            break;
                        }
                        case Operations.Task.UpdateTask: break;
                        case Operations.Task.DeleteTask: break;
                        case Operations.Task.CompleteTask: break;
                        case Operations.Task.Back:
                        {
                            isContinue = false;
                            break;
                        }
                    }

                    Console.Clear();
                }
            }

            break;
        }
        default:
        {
            Console.WriteLine($"Неизвесная операция {operation}");
            break;
        }
    }

    Console.WriteLine("Нажмите любую клавишу для продолжения");
    Console.ReadKey(true);
    Console.Clear();
}