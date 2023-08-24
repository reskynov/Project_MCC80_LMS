using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Classrooms;
using Client.ViewModels.Users;

namespace Client.Contracts
{
    public interface IClassroomRepository : IGeneralRepository<Classroom, Guid>
    {
        public Task<ResponseHandler<IEnumerable<ClassroomPeopleVM>>> GetClassroomPeople(Guid guid);
    }
}
