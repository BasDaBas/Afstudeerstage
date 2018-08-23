using MediatR;

namespace Rovecom.TicketConnector.Domain.Entities
{
    /// <summary>
    /// Interface for a root entity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Adds a domain event to the entity
        /// </summary>
        /// <param name="eventItem">An event</param>
        void AddDomainEvent(INotification eventItem);

        /// <summary>
        /// Removes a domain event from the entity
        /// </summary>
        /// <param name="eventItem">The event to be removed</param>
        void RemoveDomainEvent(INotification eventItem);

        /// <summary>
        /// Clears the collections of domain events
        /// </summary>
        void ClearDomainEvents();
    }
}
