using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        public GradeRepository(LmsDbContext context) : base(context)
        {
        }

        public bool IsNotExist(Guid userGuid, Guid taskGuid)
        {
            return _context.Set<Grade>()
                .FirstOrDefault(g => g.UserGuid == userGuid && g.TaskGuid == taskGuid) is null;
        }
    }
}
