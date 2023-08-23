using Client.Contracts;
using Client.Models;

namespace Client.Repositories
{
    public class ClassroomRepository : GeneralRepository<Classroom, Guid>, IClassroomRepository
    {
        public ClassroomRepository(string request = "classrooms/") : base(request)
        {
        }
    }
}
