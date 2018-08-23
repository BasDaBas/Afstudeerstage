using Rovecom.TicketConnector.Domain.Entities;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;

namespace Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity
{
    /// <summary>
    /// Possible type of a worklog in SIS
    /// </summary>
    public class SisWorklogType : ITypeEntity
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }
    }
}