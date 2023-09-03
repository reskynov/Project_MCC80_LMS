using Client.Contracts;
using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Lessons;
using Newtonsoft.Json;
using System.Text;


namespace Client.Repositories
{
    public class LessonRepository : GeneralRepository<Lesson, Guid>, ILessonRepository
    {
        public LessonRepository(string request = "lessons/") : base(request)
        {
        }

        public async Task<ResponseHandler<NewLessonTaskVM>> CreateLessonWithTask(NewLessonTaskVM newLessonTaskVM)
        {
            ResponseHandler<NewLessonTaskVM> createLesson = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(newLessonTaskVM), Encoding.UTF8, "application/json");
            using(var response = httpClient.PostAsync(request + "task", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                createLesson = JsonConvert.DeserializeObject<ResponseHandler<NewLessonTaskVM>>(apiResponse);
            }
            return createLesson;
        }

        public async Task<ResponseHandler<UpdateLessonTaskVM>> EditLessonWithTask(Guid guid, UpdateLessonTaskVM updateLessonTaskVM)
        {
            ResponseHandler<UpdateLessonTaskVM> editLesson = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(updateLessonTaskVM), Encoding.UTF8, "application/json");
            using(var response = httpClient.PutAsync(request + "task", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                editLesson = JsonConvert.DeserializeObject<ResponseHandler<UpdateLessonTaskVM>>(apiResponse);
            }
            return editLesson;
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
