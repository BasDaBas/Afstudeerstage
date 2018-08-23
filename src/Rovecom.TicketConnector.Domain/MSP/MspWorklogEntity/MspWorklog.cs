using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using System;

namespace Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity
{
    public class MspWorklog
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Default constructor for MSP Worklog
        /// </summary>
        /// <param name="workStartedDateTime"><see cref="WorkStartedDateTime"/></param>
        /// <param name="workEndedDateTime"><see cref="WorkEndedDateTime"/></param>
        /// <param name="description">see<see cref="Description"/></param>
        /// <param name="kilometresCovered"><see cref="KilometresCovered"/></param>
        /// <param name="technicianId"><see cref="MspTechnicianId"/></param>
        public MspWorklog(
            DateTime workStartedDateTime,
            DateTime workEndedDateTime,
            string description,
            double kilometresCovered,
            long technicianId
        )
        {
            WorkStartedDateTime = workStartedDateTime;
            WorkEndedDateTime = workEndedDateTime;
            Description = description;
            KilometresCovered = kilometresCovered;
            MspTechnicianId = technicianId;
        }

        /// <summary>
        /// The moment work ended
        /// </summary>
        public DateTime WorkEndedDateTime { get; }

        /// <summary>
        /// The moment work started
        /// </summary>
        public DateTime WorkStartedDateTime { get; }

        /// <summary>
        /// A description of the work done
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Distance traveled for the work
        /// </summary>
        public double KilometresCovered { get; }

        /// <summary>
        /// Gets the id of the employee that did the work
        /// </summary>
        public long MspTechnicianId { get; }
    }
}