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
        public bool IsNotExist(string value)
        {
            return _context.Set<User>()
                            .SingleOrDefault(e => e.Email.Contains(value)
                            || e.PhoneNumber.Contains(value)) is null;
        }
    }
}
