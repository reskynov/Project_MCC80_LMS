using Client.Utilities.Handlers;

namespace Client.Contracts
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
        Task<ResponseHandler<TokenDto>> Login(LoginDto login);
    }
}
