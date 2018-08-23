namespace Rovecom.TicketConnector.Api.ConfigOptions
{
    /// <summary>
    /// SMTP Client options
    /// </summary>
    public class SmtpConfigOptions
    {
        /// <summary>
        /// Gets or sets the host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }
    }
}