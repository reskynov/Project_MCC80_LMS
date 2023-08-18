using API.Contracts;
using API.Data;
using API.Models;
using Task = API.Models.Task;

namespace API.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(LmsDbContext context) : base(context) { }
    }
}
