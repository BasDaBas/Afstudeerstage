using Rovecom.TicketConnector.Domain.Entities;

namespace Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity
{
    /// <inheritdoc />
    /// <summary>
    /// Possible tariff type of a SIS worklog
    /// </summary>
    public class SisWorklogTariffType : ITypeEntity
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public long Id { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }
    }
}
