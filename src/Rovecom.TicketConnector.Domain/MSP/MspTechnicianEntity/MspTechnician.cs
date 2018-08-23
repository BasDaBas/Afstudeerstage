using Rovecom.TicketConnector.Domain.Entities;

namespace Rovecom.TicketConnector.Domain.MSP.MspTechnicianEntity
{
    /// <inheritdoc />
    /// <summary>
    /// A technician in MSP
    /// </summary>
    public class MspTechnician : IEmployee
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public int Id { get; set; }

        /// <inheritdoc />
        public string FirstName { get; set; }

        /// <inheritdoc />
        public string LastName { get; set; }

        /// <inheritdoc />
        public string EmailAddress { get; set; }
    }
}