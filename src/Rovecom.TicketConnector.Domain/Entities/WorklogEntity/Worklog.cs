using System;

namespace Rovecom.TicketConnector.Domain.Entities.WorklogEntity
{
    /// <inheritdoc />
    public class Worklog : IWorklog
    {
        /// <summary>
        /// Initializes a new instance of the Worklog class.
        /// </summary>
        /// <param name="workStartedDateTime">The time work started</param>
        /// <param name="workEndedDateTime">The time work ended</param>
        /// <param name="description">Description of the work done</param>
        /// <param name="kilometresCovered">Distance traveled for work</param>
        /// <param name="employeeEmailAddress">EmailAddress address of the employee</param>
        public Worklog(
            DateTime workStartedDateTime,
            DateTime workEndedDateTime,
            string description,
            double kilometresCovered,
            string employeeEmailAddress
        )
        {
            WorkStartedDateTime = workStartedDateTime;
            WorkEndedDateTime = workEndedDateTime;
            Description = description;
            KilometresCovered = kilometresCovered;
            EmployeeEmailAddress = employeeEmailAddress;
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the project code the worklog belongs to
        /// </summary>
        public string ProjectCode { get; set; }

        /// <inheritdoc />
        public DateTime WorkEndedDateTime { get; set; }

        /// <inheritdoc />
        public DateTime WorkStartedDateTime { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public double KilometresCovered { get; set; }

        /// <inheritdoc />
        public string EmployeeEmailAddress { get; set; }
    }
}