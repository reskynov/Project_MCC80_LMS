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
    }
}
