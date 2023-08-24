using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Classrooms;

namespace Client.Contracts
{
    public interface IClassroomRepository : IGeneralRepository<Classroom, Guid>
    {
        public Task<ResponseHandler<IEnumerable<ClassroomLessonVM>>> GetLessonByClassroom(Guid guid);
        public Task<ResponseHandler<IEnumerable<ClassroomPeopleVM>>> GetPeople(Guid guid);
    }
}
