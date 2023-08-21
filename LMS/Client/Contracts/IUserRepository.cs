using Client.Models;

namespace Client.Contracts
{
    public interface IUserRepository : IGeneralRepository<User, Guid>
    {
    }
}
