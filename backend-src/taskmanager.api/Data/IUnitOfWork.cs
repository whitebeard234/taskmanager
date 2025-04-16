using System.Threading.Tasks;
using taskmanager.api.Data.Repositories;

namespace taskmanager.api.Data
{
    public interface IUnitOfWork
    {
        ITaskManagerRepository TaskManagerRepository { get; }
        Task<int> CompleteAsync();
    }
}
