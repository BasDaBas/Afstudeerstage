using System;

namespace Rovecom.TicketConnector.Domain
{
    /// <inheritdoc />
    public class DomainException : Exception
    {
        /// <inheritdoc />
        public DomainException()
        {
        }

        /// <inheritdoc />
        public DomainException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public DomainException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}