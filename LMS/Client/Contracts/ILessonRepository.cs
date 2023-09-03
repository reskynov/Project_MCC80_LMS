using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Lessons;

namespace Client.Contracts
{
    public interface ILessonRepository : IGeneralRepository<Lesson, Guid>
    {
        public Task<ResponseHandler<LessonDetailVM>> GetLessonDetailByGuid(Guid guid);
        public Task<ResponseHandler<NewLessonTaskVM>> CreateLessonWithTask(NewLessonTaskVM newLessonTaskVM);
        public Task<ResponseHandler<UpdateLessonTaskVM>> EditLessonWithTask(Guid guid,UpdateLessonTaskVM updateLessonTaskVM);
    }
}
