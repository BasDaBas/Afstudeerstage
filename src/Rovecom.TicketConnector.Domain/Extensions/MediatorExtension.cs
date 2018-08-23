using System.Linq;
using MediatR;
using Rovecom.TicketConnector.Domain.Entities;

namespace Rovecom.TicketConnector.Domain.Extensions
{
    /// <summary>
    /// Extension for mediator library
    /// </summary>
    public static class MediatorExtension
    {
        /// <summary>
        /// Dispatches the domain events 
        /// </summary>
        /// <param name="mediator"><see cref="IMediator"/></param>
        /// <param name="ctx"><see cref="ConnectorContext"/></param>
        public static void DispatchDomainEvents(this IMediator mediator, ConnectorContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                mediator.Publish(domainEvent);
            }
        }
    }
}
