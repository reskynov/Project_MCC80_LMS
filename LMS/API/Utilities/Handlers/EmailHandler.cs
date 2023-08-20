using API.Contracts;
using API.DTOs.Accounts;
using System.Net.Mail;

namespace API.Utilities.Handlers
{
    public class EmailHandler : IEmailHandler
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromEmailAddress;

        public EmailHandler(string smtpServer, int smtpPort, string fromEmailAddress)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _fromEmailAddress = fromEmailAddress;
        }

        public void SendEmail(EmailMessageDto emailMessageDto)
        {
            var message = new MailMessage
            {
                From = new MailAddress(_fromEmailAddress),
                Subject = emailMessageDto.Subject,
                Body = emailMessageDto.Message,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(emailMessageDto.ToEmail));

            using var client = new SmtpClient(_smtpServer, _smtpPort);
            client.Send(message);
        }
    }
}
