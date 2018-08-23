using System.Collections.Generic;
using MediatR;

namespace Rovecom.TicketConnector.Domain.Entities
{
    public abstract class Entity : IEntity
    {
        public List<INotification> DomainEvents { get; private set; }

        public void AddDomainEvent(INotification eventItem)
        {
            DomainEvents = DomainEvents ?? new List<INotification>();
            DomainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            DomainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            DomainEvents?.Clear();
        }
    }
}
