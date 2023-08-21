using API.Contracts;
using API.Data;
using API.Models;
using Task = API.Models.Task;

namespace API.Repositories
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(LmsDbContext context) : base(context) { }

        public bool IsNotExist(Guid userGuid, Guid lessonGuid)
        {
            return _context.Set<Task>()
                .FirstOrDefault(t => t.UserGuid == userGuid && t.LessonGuid == lessonGuid) is null;
        }
        public bool IsDataUnique(Guid userGuid, Guid lessonGuid)
        {
            var existingData = _context.Set<Task>().FirstOrDefault(t => t.UserGuid == userGuid && t.LessonGuid == lessonGuid);

            if (existingData is null)
            {
                return true;
            }


            bool isSameAsExisitingData = existingData.UserGuid == userGuid && existingData.LessonGuid == lessonGuid;

            return isSameAsExisitingData;
        }

    }
}
