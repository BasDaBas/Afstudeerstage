using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Rovecom.TicketConnector.Api.ConfigOptions;

namespace Rovecom.TicketConnector.Api.Services
{
    /// <inheritdoc />
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly SmtpConfigOptions _smtpConfig;

        /// <summary>
        /// Default constructor for the email service
        /// </summary>
        public EmailService(ILogger<EmailService> logger, IOptions<SmtpConfigOptions> smtpConfigAccessor)
        {
            _logger = logger;
            _smtpConfig = smtpConfigAccessor.Value;
        }

        /// <inheritdoc />
        public void SendEmail(string subject, string message, string emailAddressRecipient)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("Test", "test@test.nl"));
            mimeMessage.To.Add(new MailboxAddress("Recipient", "recipientTestCom"));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpConfig.Host, _smtpConfig.Port);
                client.Authenticate(_smtpConfig.UserName, _smtpConfig.Password);
                client.Send(mimeMessage);
                _logger.LogInformation("Send email succesfully to {0} with subject {1}", emailAddressRecipient, subject);
                client.Disconnect(true);
            }
        }
    }
}