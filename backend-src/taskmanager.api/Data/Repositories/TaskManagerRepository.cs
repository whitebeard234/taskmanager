using Microsoft.EntityFrameworkCore;
using taskmanager.api.Models;

namespace taskmanager.api.Data.Repositories
{
    public class TaskManagerRepository : ITaskManagerRepository
    {
        private readonly TaskManagerDbContext _context;

        public TaskManagerRepository(TaskManagerDbContext context) => _context = context;

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            await _context.TaskItems.AddAsync(task);
        }

        public async Task UpdateTask(TaskItem task)
        {
            var existingTask = await GetTaskByIdAsync(task.Id);

            if (existingTask is not null)
            {
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Status = task.Status;

                _context.TaskItems.Update(existingTask);
            }
        }

        public void DeleteTask(TaskItem task)
        {
            _context.TaskItems.Remove(task);
        }
    }
}
