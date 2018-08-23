namespace Rovecom.TicketConnector.Domain.Entities.AccountEntity
{
    /// <summary>
    /// Base account class
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// Gets or sets the code
        /// </summary>
        string Code { get; set; }
        
        /// <summary>
        /// Gets or sets the email address
        /// </summary>
        string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        string FaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the telephone number
        /// </summary>
        string TelephoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the url of the website
        /// </summary>
        string WebsiteUrl { get; set; }
    }
}