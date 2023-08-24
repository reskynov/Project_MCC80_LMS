using API.Models;

namespace API.Contracts
{
    public interface IUserTaskRepository : IGeneralRepository<UserTask>
    {
        bool IsNotExist(Guid userGuid, Guid taskGuid);

        bool IsDataUnique(Guid userGuid, Guid taskGuid);
    }
}
