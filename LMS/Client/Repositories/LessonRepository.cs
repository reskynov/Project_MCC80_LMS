using Client.Contracts;
using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Lessons;
using Newtonsoft.Json;

namespace Client.Repositories
{
    public class LessonRepository : GeneralRepository<Lesson, Guid>, ILessonRepository
    {
        public LessonRepository(string request = "lessons/") : base(request)
        {
        }

        public async Task<ResponseHandler<LessonDetailVM>> GetLessonDetailByGuid(Guid guid)
        {
            ResponseHandler<LessonDetailVM> lessonDetail = null;
            using(var response = await httpClient.GetAsync(request + "detail?guid=" +  guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                lessonDetail = JsonConvert.DeserializeObject<ResponseHandler<LessonDetailVM>>(apiResponse);
            }
            return lessonDetail;
        }
    }
}
