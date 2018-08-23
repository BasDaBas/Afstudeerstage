using System;

namespace Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity
{
    /// <summary>
    /// A worklog in SIS
    /// </summary>
    public class SisWorklog
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="workStartedDateTime"><see cref="WorkStartedDateTime"/></param>
        /// <param name="workEndedDateTime"><see cref="WorkEndedDateTime"/></param>
        /// <param name="description"><see cref="Description"/></param>
        /// <param name="kilometresCovered"><see cref="KilometresCovered"/></param>
        /// <param name="employeeId"><see cref="EmployeeId"/></param>
        public SisWorklog(DateTime workStartedDateTime, DateTime workEndedDateTime, string description, double kilometresCovered,
            int employeeId)
        {
            WorkStartedDateTime = workStartedDateTime;
            WorkEndedDateTime = workEndedDateTime;
            Description = description;
            KilometresCovered = kilometresCovered;
            EmployeeId = employeeId;
        }

        /// <summary>
        /// Gets the datetime the work ended
        /// </summary>
        public DateTime WorkEndedDateTime { get; }

        /// <summary>
        /// Gets the datetime the work started
        /// </summary>
        public DateTime WorkStartedDateTime { get; }

        /// <summary>
        /// Gets the description of the work done
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the distance traveled for the work
        /// </summary>
        public double KilometresCovered { get; }

        /// <summary>
        /// Gets the id of the employee that did the work
        /// </summary>
        public int EmployeeId { get; }
    }
}