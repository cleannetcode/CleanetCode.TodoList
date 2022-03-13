using CleanetCode.TodoList.CLI.Models;

namespace CleanetCode.TodoList.CLI.Storages
{
    public static class TaskStorage
    {
        private static readonly Dictionary<Guid, List<TodoTask>> UsersTasks = new();

        public static TodoTask[]? Get(Guid userId)
        {
            UsersTasks.TryGetValue(userId, out List<TodoTask>? tasks);
            return tasks?.ToArray();
        }

        public static void Add(TodoTask newTask)
        {
            UsersTasks.TryGetValue(newTask.UserId, out List<TodoTask>? tasks);
            if (tasks == null)
            {
                UsersTasks.Add(newTask.UserId, new List<TodoTask> { newTask });
            }
            else
            {
                tasks.Add(newTask);
            }
        }

        public static void Remove(Guid userId, Guid taskId)
        {
            UsersTasks.TryGetValue(userId, out List<TodoTask>? tasks);
            if (tasks == null)
            {
                return;
            }

            TodoTask? taskToDelete = null;
            foreach (var task in tasks)
            {
                if (task.Id == taskId)
                {
                    taskToDelete = task;
                }
            }

            if (taskToDelete == null)
            {
                return;
            }

            tasks.Remove(taskToDelete);
        }

        public static void Update(Guid userId, TodoTask updatedTask)
        {
            UsersTasks.TryGetValue(userId, out List<TodoTask>? tasks);
            if (tasks == null)
            {
                return;
            }

            TodoTask? taskToUpdate = null;
            foreach (var task in tasks)
            {
                if (task.Id == updatedTask.Id)
                {
                    taskToUpdate = task;
                }
            }

            if (taskToUpdate == null)
            {
                return;
            }

            tasks.Remove(taskToUpdate);
            tasks.Add(updatedTask);
        }
    }
}