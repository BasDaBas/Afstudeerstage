using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rovecom.TicketConnector.Domain.Entities.AccountEntity
{
    /// <inheritdoc />
    public class Account : IAccount
    {
        public long Id { get; set; }

        /// <inheritdoc />
        public string FaxNumber { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string TelephoneNumber { get; set; }

        /// <inheritdoc />
        public string WebsiteUrl { get; set; }

        /// <inheritdoc />
        public string Code { get; set; }

        /// <inheritdoc />
        public string EmailAddress { get; set; }

        /// <summary>
        /// Sets or gets the list of projects that belong tot the account
        /// </summary>
        public List<Project> Projects { get; set; }
    }
}