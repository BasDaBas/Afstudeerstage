using Rovecom.TicketConnector.Domain.Entities;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;

namespace Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity
{
    /// <inheritdoc />
    /// <summary>
    /// Possible type for a MSP worklog
    /// </summary>
    public class MspWorklogType : ITypeEntity
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets whether the worklog type is removed
        /// </summary>
        public bool IsRemoved { get; set; }
    }
}