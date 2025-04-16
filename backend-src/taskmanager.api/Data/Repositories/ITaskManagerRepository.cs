using taskmanager.api.Models;

namespace taskmanager.api.Data.Repositories
{
    public interface ITaskManagerRepository
    {
        Task AddTaskAsync(TaskItem task);
        void DeleteTask(TaskItem task);
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task UpdateTask(TaskItem task);
    }
}