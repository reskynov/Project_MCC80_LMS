using API.Models;

namespace API.Contracts
{
    public interface IUserRepository : IGeneralRepository<User>
    {
        bool IsNotExist(string value);
    }
}
