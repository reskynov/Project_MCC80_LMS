using Client.Contracts;
using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.UserTasks;
using Newtonsoft.Json;

namespace Client.Repositories
{
    public class UserTaskRepository : GeneralRepository<UserTask, Guid>, IUserTaskRepository
    {
        public UserTaskRepository(string request = "user-tasks/") : base(request)
        {
        }

        public async Task<ResponseHandler<GetSubmittedTaskVM>> GetSubmittedTask(Guid userGuid, Guid lessonGuid)
        {
            ResponseHandler<GetSubmittedTaskVM> getSubmittedTask = null;
            using (var response = await httpClient.GetAsync(request + "submit-task?UserGuid=" + userGuid + "&LessonGuid=" + lessonGuid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                getSubmittedTask = JsonConvert.DeserializeObject<ResponseHandler<GetSubmittedTaskVM>>(apiResponse);
            }
            return getSubmittedTask;
        }
    }
}
