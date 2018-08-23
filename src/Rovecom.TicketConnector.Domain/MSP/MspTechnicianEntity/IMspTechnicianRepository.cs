using System.Collections.Generic;

namespace Rovecom.TicketConnector.Domain.MSP.MspTechnicianEntity
{
    public interface IMspTechnicianRepository
    {
        /// <summary>
        /// Gets a Msp Technician by email address
        /// </summary>
        /// <param name="emailAddress">The email address of the Msp Technician</param>
        /// <returns><see cref="MspTechnician"/></returns>
        MspTechnician GetByEmailAddress(string emailAddress);

        /// <summary>
        /// Gets all MSP Technicians
        /// </summary>
        /// <returns>All MSP Technicians</returns>
        IEnumerable<MspTechnician> GetAll();

        /// <summary>
        /// Gets a MSP Technician by its id
        /// </summary>
        /// <param name="id">The id of the MSP Technician</param>
        /// <returns><see cref="MspTechnician"/></returns>
        MspTechnician GetById(long id);
    }
}