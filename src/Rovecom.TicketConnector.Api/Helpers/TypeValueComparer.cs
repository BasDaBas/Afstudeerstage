using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rovecom.TicketConnector.Domain.Entities;

namespace Rovecom.TicketConnector.Api.Helpers
{
    /// <inheritdoc />
    public class TypeValueComparer : IEqualityComparer<ITypeEntity>
    {
        /// <inheritdoc />
        public bool Equals(ITypeEntity x, ITypeEntity y)
        {
            // Check whether the compared objects reference the same data.
            if (ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null.
            if (x is null || y is null)
                return false;

            // Check whether the properties are equal
            return string.Equals(x.Description, y.Description);
        }

        /// <inheritdoc />
        public int GetHashCode(ITypeEntity obj)
        {
            // Check whether the object is null, if not return hash code.
            return obj is null ? 0 : obj.Description.GetHashCode();
        }
    }
}
