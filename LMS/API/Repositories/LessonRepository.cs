using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(LmsDbContext context) : base(context)
        {
        }
    }
}
