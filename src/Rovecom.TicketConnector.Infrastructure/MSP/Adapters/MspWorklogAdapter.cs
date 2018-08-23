using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using Rovecom.TicketConnector.Domain.UnitOfWork;
using System;

namespace Rovecom.TicketConnector.Infrastructure.MSP.Adapters
{
    /// <inheritdoc />
    /// <summary>
    /// Adapts the MSP worklog to a worklog implementation the application can use
    /// </summary>
    public class MspWorklogAdapter : IWorklog
    {
        private readonly MspWorklog _worklog;
        private readonly IDapperUnitOfWorkFactory _dapperUnitOfWorkFactory;

        /// <summary>
        /// Default constructor for the MSP worklog
        /// </summary>
        /// <param name="worklog"><see cref="MspWorklog"/></param>
        /// <param name="dapperUnitOfWorkFactory"><see cref="IDapperUnitOfWorkFactory"/></param>
        public MspWorklogAdapter(MspWorklog worklog, IDapperUnitOfWorkFactory dapperUnitOfWorkFactory)
        {
            _worklog = worklog;
            _dapperUnitOfWorkFactory = dapperUnitOfWorkFactory;
            EmployeeEmailAddress = GetTechnicianEmail(worklog.MspTechnicianId);
        }

        /// <inheritdoc />
        public DateTime WorkEndedDateTime => _worklog.WorkEndedDateTime;

        /// <inheritdoc />
        public DateTime WorkStartedDateTime => _worklog.WorkStartedDateTime;

        /// <inheritdoc />
        public string Description => _worklog.Description;

        /// <inheritdoc />
        public double KilometresCovered => _worklog.KilometresCovered;

        /// <inheritdoc />
        public string EmployeeEmailAddress { get; }

        // Retrieves the emailaddress of an employee
        private string GetTechnicianEmail(long employeeId)
        {
            string emailAddress;
            using (var uow = _dapperUnitOfWorkFactory.Create())
            {
                emailAddress = uow.MspTechnicianRepository.GetById(employeeId).EmailAddress;
            }

            return emailAddress;
        }
    }
}