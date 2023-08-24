using Client.Contracts;
using Client.ViewModels.UserClassrooms;

namespace Client.Repositories;

public class UserClassroomRepository : GeneralRepository<UserClassroomVM, Guid>, IUserClassroomRepository
{
    public UserClassroomRepository(string request = "userclassrooms/") : base(request)
    {
    }
}
