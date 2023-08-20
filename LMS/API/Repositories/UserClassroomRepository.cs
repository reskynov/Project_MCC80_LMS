using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class UserClassroomRepository : GenericRepository<UserClassroom>, IUserClassroomRepository
    {
        public UserClassroomRepository(LmsDbContext context) : base(context)
        {
        }

        public bool IsNotExist(Guid userGuid, Guid classroomGuid)
        {
            return _context.Set<UserClassroom>().FirstOrDefault(uc => uc.UserGuid == userGuid && uc.ClassroomGuid == classroomGuid) is null;
        }
    }
}
