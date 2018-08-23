using System.Collections.Generic;

namespace Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity
{
    /// <summary>
    /// Repository for MSP worklog tariff types
    /// </summary>
    public interface IMspWorklogTariffTypeRepository
    {
        /// <summary>
        /// Gets all MSP worklog tariff types
        /// </summary>
        /// <returns>Collection of MSP worklog tariff types</returns>
        IEnumerable<MspWorklogTariffType> GetAllActive();

        /// <summary>
        /// Gets all MSP worklog tariff types, including removed ones
        /// </summary>
        /// <returns>Collection of MSP worklog tariff types</returns>
        IEnumerable<MspWorklogTariffType> GetAll();

        /// <summary>
        /// Adds a worklog tariff type
        /// </summary>
        /// <param name="worklogTariffType"><see cref="MspWorklogTariffType"/></param>
        void Add(MspWorklogTariffType worklogTariffType);

        /// <summary>
        /// Removes a worklog tariff type
        /// </summary>
        /// <param name="worklogTariffType"><see cref="MspWorklogTariffType"/></param>
        void Remove(MspWorklogTariffType worklogTariffType);
    }
}
