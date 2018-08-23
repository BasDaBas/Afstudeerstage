namespace Rovecom.TicketConnector.Infrastructure.SIS
{
    /// <summary>
    /// Configuration options for the SIS API
    /// </summary>
    public class SisApiConfig
    {
        /// <summary>
        /// Gets or sets the code of the app
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the hashkey for the password
        /// </summary>
        public string PasswordHashKey { get; set; }

        /// <summary>
        /// Gets or sets the hashkey for the signature
        /// </summary>
        public string SignatureHashKey { get; set; }

        /// <summary>
        /// Get or sets the url of the SIS API
        /// </summary>
        public string Url { get; set; }
    }
}