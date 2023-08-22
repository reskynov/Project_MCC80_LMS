using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Users;

namespace Client.Contracts
{
    public interface IUserRepository : IGeneralRepository<User, Guid>
    {
        public Task<ResponseHandler<IEnumerable<ClassroomByUserVM>>> GetClassroomByUser(Guid guid);
    }
}
