using Client.Contracts;
using Client.Models;

namespace Client.Repositories
{
    public class UserRepository : GeneralRepository<User, Guid>, IUserRepository
    {
        public UserRepository(string request = "users/") : base(request)
        {
        }
    }
}
