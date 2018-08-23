using System;
using System.Collections.Generic;
using System.Text;
using Rovecom.TicketConnector.Domain.Entities;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;

namespace Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity
{
    /// <inheritdoc />
    /// <summary>
    /// Possible tariff type for a MSP worklog
    /// </summary>
    public class MspWorklogTariffType : ITypeEntity
    {
        /// <summary>
        /// Default constructor for MSP worklog tariff type
        /// </summary>
        /// <param name="id">Id of the MSP worklog tariff type</param>
        /// <param name="description">Description of the MSP worklog tariff type</param>
        public MspWorklogTariffType(long id, string description)
        {
            Id = id;
            Description = description;
        }

        /// <summary>
        /// Gets or sets the id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets whether the worklog tariff type is removed
        /// </summary>
        public bool IsRemoved { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }
    }
}