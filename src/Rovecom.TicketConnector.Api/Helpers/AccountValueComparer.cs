using Rovecom.TicketConnector.Domain.Entities.AccountEntity;
using System.Collections.Generic;

namespace Rovecom.TicketConnector.Api.Helpers
{
    /// <inheritdoc />
    /// <summary>
    /// Compares two accounts for equality based on their properties
    /// </summary>
    public class AccountValueComparer : IEqualityComparer<IAccount>
    {
        /// <inheritdoc />
        public bool Equals(IAccount x, IAccount y)
        {
            // Check whether the compared objects reference the same data.
            if (ReferenceEquals(x, y)) return true;

            // Check whether any of the compared objects is null.
            if (x is null || y is null)
                return false;

            // Check whether the accounts properties are equal
            return x.Name == y.Name &&
                   x.Code == y.Code &&
                   x.EmailAddress == y.EmailAddress &&
                   x.FaxNumber == y.FaxNumber &&
                   x.TelephoneNumber == y.TelephoneNumber &&
                   x.WebsiteUrl == y.WebsiteUrl;
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