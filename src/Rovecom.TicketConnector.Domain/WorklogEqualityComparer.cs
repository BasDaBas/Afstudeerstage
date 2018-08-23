using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using System;
using System.Collections.Generic;

namespace Rovecom.TicketConnector.Domain
{
    /// <inheritdoc />
    /// <summary>
    /// A worklog comparer
    /// </summary>
    public class WorklogEqualityComparer : IEqualityComparer<IWorklog>
    {
        /// <inheritdoc />
        /// <summary>
        /// Compares worklogs based on values
        /// </summary>
        /// <param name="x">A worklog</param>
        /// <param name="y">Another worklog</param>
        /// <returns>True when equal</returns>
        public bool Equals(IWorklog x, IWorklog y)
        {
            return x.WorkStartedDateTime == y.WorkStartedDateTime &&
                        x.WorkEndedDateTime == y.WorkEndedDateTime &&
                        Math.Abs(x.KilometresCovered - y.KilometresCovered) < 0.01 &&
                        string.Equals(x.EmployeeEmailAddress, y.EmployeeEmailAddress);
        }

        /// <inheritdoc />
        public int GetHashCode(IWorklog obj)
        {
            //Check whether the object is null
            if (obj is null) return 0;

            //Get hash code for the Employee field if it is not null.
            var hashEmployee = obj.EmployeeEmailAddress.GetHashCode();

            //Get hash code for the Datetime fields.
            var hashStarted = obj.WorkStartedDateTime.GetHashCode();
            var hashEnded = obj.WorkEndedDateTime.GetHashCode();

            //Calculate the hash code for the product.
            var hash = hashEmployee ^ hashStarted ^ hashEnded;
            return hash;
        }
    }
}