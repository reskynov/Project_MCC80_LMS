using Client.Contracts;
using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Classrooms;
using Newtonsoft.Json;

namespace Client.Repositories
{
    public class ClassroomRepository : GeneralRepository<Classroom, Guid>, IClassroomRepository
    {
        public ClassroomRepository(string request = "classrooms/") : base(request)
        {
           
        }
        public async Task<ResponseHandler<IEnumerable<ClassroomPeopleVM>>> GetClassroomPeople(Guid guid)
        {
            ResponseHandler<IEnumerable<ClassroomPeopleVM>> classroomPeople = null;
            using (var response = await httpClient.GetAsync(request + "classroom?guid=" + guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                classroomPeople = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<ClassroomPeopleVM>>>(apiResponse);
            }
            return classroomPeople;
        }
    }
}
