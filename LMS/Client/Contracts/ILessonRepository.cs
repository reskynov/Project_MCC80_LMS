using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Lessons;

namespace Client.Contracts
{
    public interface ILessonRepository : IGeneralRepository<Lesson, Guid>
    {
        public Task<ResponseHandler<LessonDetailVM>> GetLessonDetailByGuid(Guid guid);
    }
}
