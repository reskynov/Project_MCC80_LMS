using Client.Contracts;
using Client.Models;

namespace Client.Repositories
{
    public class LessonRepository : GeneralRepository<Lesson, Guid>, ILessonRepository
    {
        public LessonRepository(string request = "lessons/") : base(request)
        {
        }
    }
}
