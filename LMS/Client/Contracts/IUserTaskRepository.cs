using Client.Models;

namespace Client.Contracts
{
    public interface IUserTaskRepository : IGeneralRepository<UserTask, Guid>
    {
    }
}
