using API.Models;

namespace API.Contracts
{
    public interface IUserRepository : IGeneralRepository<User>
    {
        User? GetByEmail(string email);
    }
}
