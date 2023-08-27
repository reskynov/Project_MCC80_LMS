using Client.DTOs.Accounts;
using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Users;

namespace Client.Contracts
{
    public interface IUserRepository : IGeneralRepository<User, Guid>
    {
        public Task<ResponseHandler<IEnumerable<ClassroomByUserVM>>> GetClassroomByUser(Guid guid);
        public Task<ResponseHandler<ProfileVM>> GetProfile(Guid guid);
        public Task<ResponseHandler<ProfileChangePasswordVM>> ProfileChangePassword(ProfileChangePasswordVM entity);
    }
}
