using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class ClassroomRepository : GenericRepository<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(LmsDbContext context) : base(context)
        {
        }


    }
}