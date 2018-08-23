namespace Rovecom.TicketConnector.Api.Services
{
    /// <summary>
    /// Service for sending email
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email message
        /// </summary>
        /// <param name="subject">Subject of the email message</param>
        /// <param name="message">Body of the email message</param>
        /// <param name="emailAddressRecipient">Email address of the recipient</param>
        void SendEmail(string subject, string message, string emailAddressRecipient);
    }
}