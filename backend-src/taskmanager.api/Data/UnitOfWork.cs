using taskmanager.api.Data.Repositories;

namespace taskmanager.api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskManagerDbContext _context;
        private TaskManagerRepository _taskManagerRepository;

        public UnitOfWork(TaskManagerDbContext context, TaskManagerRepository taskManagerRepository)
        {
            _context = context;
            _taskManagerRepository = taskManagerRepository;
        }

        public ITaskManagerRepository TaskManagerRepository
        {
            get
            {
                return _taskManagerRepository ??= new TaskManagerRepository(_context);
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
