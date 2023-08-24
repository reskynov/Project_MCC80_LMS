using Client.Contracts;
using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Classrooms;
using Newtonsoft.Json;

namespace Client.Repositories;

public class ClassroomRepository : GeneralRepository<Classroom, Guid>, IClassroomRepository
{
    public ClassroomRepository(string request = "classrooms/") : base(request)
    {
    }

    public async Task<ResponseHandler<IEnumerable<ClassroomLessonVM>>> GetLessonByClassroom(Guid guid)
    {
        ResponseHandler<IEnumerable<ClassroomLessonVM>> lessonByClassroom = null;
        using (var response = await httpClient.GetAsync(request + "lesson?guid=" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            lessonByClassroom = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<ClassroomLessonVM>>>(apiResponse);
        }

        return lessonByClassroom;
    }

    public async Task<ResponseHandler<IEnumerable<ClassroomPeopleVM>>> GetPeople(Guid guid)
    {
        ResponseHandler<IEnumerable<ClassroomPeopleVM>> student = null;
        using (var response = await httpClient.GetAsync(request + "people?guid=" + guid))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            student = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<ClassroomPeopleVM>>>(apiResponse);
        }
        return student;
    }
}
