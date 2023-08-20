using API.DTOs.Accounts;

namespace API.Contracts
{
    public interface IEmailHandler
    {
        void SendEmail(EmailMessageDto emailMessageDto);
    }
}
