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

        //get user by email
        public User? GetByEmail(string email)
        {
            return _context.Set<User>().SingleOrDefault(e => e.Email.Contains(email));
        }

        public bool IsNotExist(string value)
        {
            //return true if null
            return _context.Set<User>()
                           .SingleOrDefault(e => e.Email.Contains(value)
                                               || e.PhoneNumber.Contains(value)) is null;
        }

        //Check data input is same as existing data
        public bool IsDataUnique(string data)
        {
            var existingUser = _context.Set<User>().SingleOrDefault(u => u.Email == data || u.PhoneNumber == data);

            if (existingUser is null)
            {
                return true;
            }

            else
            {
                bool isSameAsExistingData = existingUser.Email == data || existingUser.PhoneNumber == data;
                return isSameAsExistingData;
            }
        }
    }
}
