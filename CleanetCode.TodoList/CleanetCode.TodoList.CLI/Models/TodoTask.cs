namespace CleanetCode.TodoList.CLI.Models
{
    public record TodoTask
    {
        private TodoTask()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public string Name { get; init; }
        public string Description { get; init; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; init; }

        public (TodoTask? Task, string Error) Update(string? name, string? description)
        {
            var (isValid, error) = Validate(name, description);
            if (!isValid)
            {
                return (null, error);
            }

            TodoTask updatedTask = this with
            {
                Name = name,
                Description = description
            };
            
            return (updatedTask, String.Empty);
        }

        public static (TodoTask? Task, string Error) Create(string? name, string? description, Guid userId)
        {
            var (isValid, error) = Validate(name, description);
            if (!isValid)
            {
                return (null, error);
            }

            TodoTask newTask = new TodoTask
            {
                Name = name,
                Description = description,
                UserId = userId
            };

            return (newTask, String.Empty);
        }

        private static (bool Result, string Error) Validate(string? name, string? description)
        {
            List<string> errors = new();
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add("Неверно введeно название: " + name);
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                errors.Add("Неверно введeно описание: " + description);
            }

            if (errors.Any())
            {
                return (false, string.Join("\n", errors));
            }

            return (true, String.Empty);
        }
    }
}