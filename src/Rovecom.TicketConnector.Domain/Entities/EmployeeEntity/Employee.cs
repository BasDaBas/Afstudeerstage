using System.Collections.Generic;
using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;

namespace Rovecom.TicketConnector.Domain.Entities.EmployeeEntity
{
    /// <inheritdoc />
    public class Employee : IEmployee
    {
        public int Id { get; set; }

        /// <inheritdoc />
        public string EmailAddress { get; set; }

        /// <inheritdoc />
        public string FirstName { get; set; }

        /// <inheritdoc />
        public string LastName { get; set; }

        public List<Worklog> Worklogs { get; set; }
    }
}