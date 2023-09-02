using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.UserTasks;

namespace Client.Contracts
{
    public interface IUserTaskRepository : IGeneralRepository<UserTask, Guid>
    {
        public Task<ResponseHandler<GetSubmittedTaskVM>> GetSubmittedTask(Guid userGuid, Guid lessonGuid);
        public Task<ResponseHandler<IEnumerable<GetTaskToGradeVM>>> GetTaskToGrade(Guid guid);
        public Task<ResponseHandler<SubmitTaskVM>> SubmitTask(SubmitTaskVM submitTaskVM);
        public Task<ResponseHandler<SubmitTaskVM>> EditTask(SubmitTaskVM editTaskVM);
    }
}
