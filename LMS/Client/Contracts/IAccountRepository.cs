using Client.Models;
using Client.Utilities.Handlers;
using Client.ViewModels.Accounts;

namespace Client.Contracts
{
    public interface IAccountRepository : IGeneralRepository<Account, Guid>
    {
        Task<ResponseHandler<TokenVM>> Login(LoginVM login);
        Task<ResponseHandler<RegisterVM>> Register(RegisterVM register);
        Task<ResponseHandler<ForgotPasswordVM>> ForgotPassword(ForgotPasswordVM forgotPassword);
        Task<ResponseHandler<ChangePasswordVM>> ChangePassword(ChangePasswordVM changePassword);
    }
}
