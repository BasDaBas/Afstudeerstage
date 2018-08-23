using Rovecom.TicketConnector.Domain.Entities.AccountEntity;

namespace Rovecom.TicketConnector.Domain.MSP.MspAccountEntity
{
    /// <inheritdoc />
    /// <summary>
    /// An account entity in MSP
    /// </summary>
    public class MspAccount : IAccount
    {
        /// <inheritdoc />
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the default site id
        /// </summary>
        public long DefaultSiteId { get; set; }
        
        /// <inheritdoc />
        public string EmailAddress { get; set; }

        /// <inheritdoc />
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public long Id { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string TelephoneNumber { get; set; }

        /// <inheritdoc />
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MspAccount()
        {
        }

        /// <summary>
        /// Create a MSP account from another account
        /// </summary>
        /// <param name="account"><see cref="IAccount"/></param>
        public MspAccount(IAccount account)
        {
            Code = account.Code;
            EmailAddress = account.EmailAddress;
            FaxNumber = account.FaxNumber;
            Name = account.Name;
            TelephoneNumber = account.TelephoneNumber;
            WebsiteUrl = account.WebsiteUrl;
        }
    }
}