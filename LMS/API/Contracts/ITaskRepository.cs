using API.Models;
using Task = API.Models.Task;

namespace API.Contracts
{
    public interface ITaskRepository : IGeneralRepository<Task>
    {
        bool IsNotExist(Guid userGuid, Guid lessonGuid);

        bool IsDataUnique(Guid userGuid, Guid lessonGuid);
    }
}
