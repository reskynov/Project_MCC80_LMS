using API.Models;
using Task = API.Models.Task;

namespace API.Contracts
{
    public interface ITaskRepository : IGeneralRepository<Task>
    {
    }
}
