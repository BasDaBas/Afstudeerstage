using System;
using System.Collections.Generic;

namespace Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity
{
    public class MspWorklogComparer : IEqualityComparer<MspWorklog>
    {
        /// <inheritdoc />
        /// <summary>
        /// Compares worklogs based on values
        /// </summary>
        /// <param name="x">A worklog</param>
        /// <param name="y">Another worklog</param>
        /// <returns></returns>
        public bool Equals(MspWorklog x, MspWorklog y)
        {
            return x.WorkStartedDateTime == y.WorkStartedDateTime &&
                   x.WorkEndedDateTime == y.WorkEndedDateTime &&
                   Math.Abs(x.KilometresCovered - y.KilometresCovered) < 0.01 &&
                   x.MspTechnicianId == y.MspTechnicianId;
        }

        /// <inheritdoc />
        public int GetHashCode(MspWorklog obj)
        {
            //Check whether the object is null
            if (obj is null) return 0;

            //Get hash code for the Employee field if it is not null.
            var hashEmployee = obj.MspTechnicianId.GetHashCode();

            //Get hash code for the Datetime fields.
            var hashStarted = obj.WorkStartedDateTime.GetHashCode();
            var hashEnded = obj.WorkEndedDateTime.GetHashCode();

            //Calculate the hash code for the product.
            var hash = hashEmployee ^ hashStarted ^ hashEnded;
            return hash;
        }
    }
}