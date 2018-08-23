using Rovecom.TicketConnector.Domain.Entities;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using Rovecom.TicketConnector.Domain.SIS.SisEmployeeEntity;
using Rovecom.TicketConnector.Domain.SIS.SisProjectEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Infrastructure.SIS.SisProjectAdapter
{
    /// <inheritdoc cref="IProject" />
    /// <summary>
    /// Adapts the SIS worklog to a worklog implementation the application can handle
    /// </summary>
    public class SisProjectAdapter : Entity, IProject
    {
        private readonly SisProject _project;
        private readonly ISisEmployeeRepository _employeeRepo;

        /// <summary>
        /// Default constructor for the SIS project adapter
        /// </summary>
        /// <param name="project"><see cref="SisProject"/></param>
        /// <param name="employeeRepo"><see cref="ISisEmployeeRepository"/></param>
        public SisProjectAdapter(SisProject project, string AccountCode, ISisEmployeeRepository employeeRepo)
        {
            _project = project;
            _employeeRepo = employeeRepo;
            this.AccountCode = AccountCode;
        }

        /// <inheritdoc />
        public string Code
        {
            get => _project.Code;
            set => _project.Code = value;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<IWorklog> Worklogs => new List<IWorklog>();

        /// <inheritdoc />
        public async void AddWorklog(DateTime workStartedDateTime, DateTime workEndedDateTime, string description, double kilometersCovered,
            string employeeEmail)
        {
            var employeeId = await GetEmployeeIdAsync(employeeEmail);

            if (employeeId == 0)
                throw new Exception("Employee does not exist in SIS");

            _project.AddWorklog(workStartedDateTime, workEndedDateTime, description, kilometersCovered, employeeId);
        }

        /// <inheritdoc />
        public async void RemoveWorklog(IWorklog worklog)
        {
            var employeeId = await GetEmployeeIdAsync(worklog.EmployeeEmailAddress);

            if (employeeId == 0)
                throw new Exception("Employee does not exist in SIS");

            var removedWorklog = _project.Worklogs.FirstOrDefault(wl =>
                worklog.WorkEndedDateTime == wl.WorkEndedDateTime &&
                worklog.WorkStartedDateTime == wl.WorkStartedDateTime &&
                wl.EmployeeId == employeeId);

            if (removedWorklog != null)
                _project.RemoveWorklog(removedWorklog);
        }

        /// <inheritdoc />
        public string AccountCode { get; }

        // Gets the id of an employee throught its emailaddress
        private async Task<int> GetEmployeeIdAsync(string emailAddress)
        {
            var employee = await _employeeRepo.GetByEmailAsync(emailAddress);
            return employee.Id;
        }
    }
}