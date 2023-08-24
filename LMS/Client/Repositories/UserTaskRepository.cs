using Client.Contracts;
using Client.Models;

namespace Client.Repositories
{
    public class UserTaskRepository : GeneralRepository<UserTask, Guid>, IUserTaskRepository
    {
        public UserTaskRepository(string request = "grades/") : base(request)
        {
        }
    }
}
