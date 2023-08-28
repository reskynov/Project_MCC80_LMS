using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.UserTasks;

namespace Client.Contracts
{
    public interface IUserTaskRepository : IGeneralRepository<UserTask, Guid>
    {
        public Task<ResponseHandler<GetSubmittedTaskVM>> GetSubmittedTask(Guid userGuid, Guid lessonGuid);
    }
}
