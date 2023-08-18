using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LmsDbContext context) : base(context)
        {
        }
    }
}
