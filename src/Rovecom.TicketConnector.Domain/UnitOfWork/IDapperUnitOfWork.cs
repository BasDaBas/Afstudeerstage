using Rovecom.TicketConnector.Domain.MSP.MspAccountEntity;
using Rovecom.TicketConnector.Domain.MSP.MspProjectEntity;
using Rovecom.TicketConnector.Domain.MSP.MspTechnicianEntity;
using System;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity;

namespace Rovecom.TicketConnector.Domain.UnitOfWork
{
    /// <inheritdoc />
    /// <summary>
    /// Keeps track of all dapper related business transactions and coordinates them
    /// </summary>
    public interface IDapperUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the MSP account repository
        /// </summary>
        IMspAccountRepository MspAccountRepository { get; }

        /// <summary>
        /// Gets the MSP project repository
        /// </summary>
        IMspProjectRepository MspProjectRepository { get; }

        /// <summary>
        /// Gets the MSP technican repository
        /// </summary>
        IMspTechnicianRepository MspTechnicianRepository { get; }

        /// <summary>
        /// Gets the MSP worklog tariff type repository
        /// </summary>
        IMspWorklogTariffTypeRepository MspWorklogTariffTypeRepository { get; }

        /// <summary>
        /// Gets the MSP worklog type repository
        /// </summary>
        IMspWorklogTypeRepository MspWorklogTypeRepository { get; }

        /// <summary>
        /// Commit transaction
        /// </summary>
        void Commit();
    }
}