using Rovecom.TicketConnector.Domain.Entities;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using Rovecom.TicketConnector.Domain.Events;
using Rovecom.TicketConnector.Domain.MSP.MspProjectEntity;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using Rovecom.TicketConnector.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rovecom.TicketConnector.Infrastructure.MSP.Adapters
{
    /// <inheritdoc cref="IProject" />
    /// <summary>
    /// Adapter class for a MSP project to work with SIS projects
    /// </summary>
    public class MspProjectAdapter : Entity, IProject
    {
        private readonly MspProject _mspProject;
        private readonly IDapperUnitOfWorkFactory _dapperUnitOfWorkFactory;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="mspProject">The MSP project</param>
        /// <param name="dapperUnitOfWorkFactory"><see cref="IDapperUnitOfWorkFactory"/></param>
        public MspProjectAdapter(MspProject mspProject, IDapperUnitOfWorkFactory dapperUnitOfWorkFactory)
        {
            _mspProject = mspProject;
            _dapperUnitOfWorkFactory = dapperUnitOfWorkFactory;
            AccountCode = GetAccountCode();
        }

        /// <inheritdoc />
        public string Code
        {
            get => _mspProject.Code;
            set => _mspProject.Code = value;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<IWorklog> Worklogs
        {
            get
            {
                return _mspProject.Requests.SelectMany(x => x.Worklogs).Select(y => new MspWorklogAdapter(y, _dapperUnitOfWorkFactory)).ToList<IWorklog>();
            }
        }

        /// <inheritdoc />
        public void AddWorklog(DateTime workStartedDateTime, DateTime workEndedDateTime, string description, double kilometersCovered,
            string employeeEmail)
        {
            if (!TryGetTechnicianId(employeeEmail, out var employeeId))
            {
                throw new Exception("Employee does not exist in MSP");
            }

            AddDomainEvent(new WorklogAddedEvent(Code, workEndedDateTime, workStartedDateTime, description, kilometersCovered, employeeEmail));

            var newWorklog = new MspWorklog(workStartedDateTime, workEndedDateTime, description,
                kilometersCovered, employeeId);

            _mspProject.AddWorklog(newWorklog, null);
        }

        /// <inheritdoc />
        public void RemoveWorklog(IWorklog worklog)
        {
            if (!TryGetTechnicianId(worklog.EmployeeEmailAddress, out var employeeId))
            {
                throw new Exception("Employee does not exist in MSP");
            }

            AddDomainEvent(new WorklogRemovedEvent(Code, worklog.WorkEndedDateTime, worklog.WorkStartedDateTime, worklog.Description, worklog.KilometresCovered, worklog.EmployeeEmailAddress));

            // Find the worklog and remove it
            foreach (var mspProjectRequest in _mspProject.Requests)
            {
                var removedWorklog = mspProjectRequest.Worklogs.FirstOrDefault(wl =>
                    worklog.WorkEndedDateTime == wl.WorkEndedDateTime &&
                    worklog.WorkStartedDateTime == wl.WorkStartedDateTime &&
                    wl.MspTechnicianId == employeeId);

                if (removedWorklog != null)
                    _mspProject.RemoveWorklog(removedWorklog, mspProjectRequest);
            }
        }

        /// <inheritdoc />
        public string AccountCode { get; }

        private bool TryGetTechnicianId(string emailAddress, out long employeeId)
        {
            using (var uow = _dapperUnitOfWorkFactory.Create())
            {
                employeeId = uow.MspTechnicianRepository.GetByEmailAddress(emailAddress).Id;
                uow.Commit();
            }
            return employeeId != 0;
        }

        private string GetAccountCode()
        {
            string accountCode;
            var request = _mspProject.Requests.FirstOrDefault();
            if (request == null)
                throw new Exception("Project is not attached to an account");

            using (var uow = _dapperUnitOfWorkFactory.Create())
            {
                accountCode = uow.MspAccountRepository.GetByDefaultSiteId(request.SiteId).Code;
                uow.Commit();
            }

            return accountCode;
        }
    }
}