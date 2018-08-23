using System;

namespace Rovecom.TicketConnector.Domain.Entities.WorklogEntity
{
    /// <summary>
    /// Interface of a worklog entity
    /// </summary>
    public interface IWorklog
    {
        /// <summary>
        /// The email address of the employee that did the work
        /// </summary>
        string EmployeeEmailAddress { get; }

        /// <summary>
        /// The moment work ended
        /// </summary>
        DateTime WorkEndedDateTime { get; }

        /// <summary>
        /// The moment work started
        /// </summary>
        DateTime WorkStartedDateTime { get; }

        /// <summary>
        /// A description of the work done
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Distance traveled for the work
        /// </summary>
        double KilometresCovered { get; }
    }
}