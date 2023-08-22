using Client.Contracts;
using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Users;
using Newtonsoft.Json;

namespace Client.Repositories
{
    public class UserRepository : GeneralRepository<User, Guid>, IUserRepository
    {
        public UserRepository(string request = "users/") : base(request)
        {
        }

        public async Task<ResponseHandler<IEnumerable<ClassroomByUserVM>>> GetClassroomByUser(Guid guid)
        {
            ResponseHandler<IEnumerable<ClassroomByUserVM>> classroomByUser = null;
            using (var response = await httpClient.GetAsync(request + "classroom?guid=" + guid))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                classroomByUser = JsonConvert.DeserializeObject<ResponseHandler<IEnumerable<ClassroomByUserVM>>>(apiResponse);
            }
            return classroomByUser;
        }
    }
}
