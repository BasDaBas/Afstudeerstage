using System.Collections.Generic;

namespace Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity
{
    /// <summary>
    /// Repository for MSP worklog types
    /// </summary>
    public interface IMspWorklogTypeRepository
    {
        /// <summary>
        /// Gets all MSP worklog types
        /// </summary>
        /// <returns>Collection of MSP worklog tariff types</returns>
        IEnumerable<MspWorklogType> GetAllActive();

        /// <summary>
        /// Gets all MSP worklog types, including removed ones
        /// </summary>
        /// <returns>Collection of MSP worklog tariff types</returns>
        IEnumerable<MspWorklogType> GetAll();

        /// <summary>
        /// Adds a worklog type
        /// </summary>
        /// <param name="worklogType"><see cref="MspWorklogType"/></param>
        void Add(MspWorklogType worklogType);

        /// <summary>
        /// Removes a worklog type
        /// </summary>
        /// <param name="worklogType"><see cref="MspWorklogType"/></param>
        void Remove(MspWorklogType worklogType);
    }
}
