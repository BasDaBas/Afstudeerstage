using Rovecom.TicketConnector.Domain.Entities.AccountEntity;
using System.Collections.Generic;

namespace Rovecom.TicketConnector.Api.Helpers
{
    /// <inheritdoc />
    /// <summary>
    /// Compares two accounts for equality based on the code property
    /// </summary>
    public class AccountEqualityComparer : IEqualityComparer<IAccount>
    {
        /// <inheritdoc />
        public bool Equals(IAccount x, IAccount y)
        {
            // Check whether the compared objects reference the same data.
            if (ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null.
            if (x is null || y is null)
                return false;

            // Check whether the unique code property is equal
            return x.Code == y.Code && x.Name == y.Name;
        }

        /// <inheritdoc />
        public int GetHashCode(IAccount obj)
        {
            // Check whether the object is null.
            if (obj is null) return 0;

            // Get the hash code for the Name field
            var hashAccountName = obj.Name.GetHashCode();

            // Get the hash code for the Code field.
            var hashAccountCode = obj.Code == null ? 0 : obj.Code.GetHashCode();

            // Calculate the hash code for the product.
            return hashAccountName ^ hashAccountCode;
        }
    }
}