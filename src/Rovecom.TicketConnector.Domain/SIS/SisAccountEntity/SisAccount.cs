using System.Runtime.Serialization;
using Rovecom.TicketConnector.Domain.Entities.AccountEntity;

namespace Rovecom.TicketConnector.Domain.SIS.SisAccountEntity
{
    /// <inheritdoc />
    /// <summary>
    /// Standard account entity
    /// </summary>
    [DataContract(Name = "Account")]
    public class SisAccount : IAccount
    {
        /// <inheritdoc />
        [DataMember(Name = "CODE")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the id
        /// </summary>
        [DataMember(Name = "ID")]
        public int Id { get; set; }

        /// <inheritdoc />
        [DataMember(Name = "EMAIL")]
        public string EmailAddress { get; set; }

        /// <inheritdoc />
        [DataMember(Name = "FAX")]
        public string FaxNumber { get; set; }

        /// <inheritdoc />
        [DataMember(Name = "NAAM")]
        public string Name { get; set; }

        /// <inheritdoc />
        [DataMember(Name = "TELEFOON")]
        public string TelephoneNumber { get; set; }

        /// <inheritdoc />
        [DataMember(Name = "WEBSITE")]
        public string WebsiteUrl { get; set; }
    }
}