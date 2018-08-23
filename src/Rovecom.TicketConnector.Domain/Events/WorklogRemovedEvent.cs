using System;
using MediatR;

namespace Rovecom.TicketConnector.Domain.Events
{
    /// <summary>
    /// Event of a worklog removed from a project
    /// </summary>
    public class WorklogRemovedEvent : INotification
    {
        /// <summary>
        /// Gets the code of the project the worklog was removed from
        /// </summary>
        public string ProjectCode { get; }

        /// <summary>
        /// Date and time work ended
        /// </summary>
        public DateTime WorkEnded { get; }

        /// <summary>
        /// Gets the date and time work started
        /// </summary>
        public DateTime WorkStarted { get; }

        /// <summary>
        /// Gets the description of the work done
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the distance traveled for the work in kilometers
        /// </summary>
        public double KilometersCovered { get; }

        /// <summary>
        /// Gets the email address of the employee that did the work
        /// </summary>
        public string EmployeeEmailAddress { get; }

        /// <summary>
        /// Default constructor for worklog removed event
        /// </summary>
        /// <param name="projectCode"><see cref="ProjectCode"/></param>
        /// <param name="workEnded"><see cref="WorkEnded"/></param>
        /// <param name="workstarted"><see cref="WorkStarted"/></param>
        /// <param name="description"><see cref="Description"/></param>
        /// <param name="kilometersCovered"><see cref="KilometersCovered"/></param>
        /// <param name="employeeEmailAddress"><see cref="EmployeeEmailAddress"/></param>
        public WorklogRemovedEvent(string projectCode, DateTime workEnded, DateTime workstarted, string description, double kilometersCovered,
            string employeeEmailAddress)
        {
            ProjectCode = projectCode;
            WorkStarted = workstarted;
            WorkEnded = workEnded;
            Description = description;
            KilometersCovered = kilometersCovered;
            EmployeeEmailAddress = employeeEmailAddress;
        }
    }
}