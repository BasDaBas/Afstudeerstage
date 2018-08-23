using MediatR;
using Microsoft.Extensions.Logging;
using Rovecom.TicketConnector.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Api.DomainEventHandlers
{
    /// <inheritdoc />
    public class WorklogRemovedEventHandler : INotificationHandler<WorklogRemovedEvent>
    {
        /// <inheritdoc />
        public Task Handle(WorklogRemovedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worklog {workStarted}, {workEnded} with employee {email} added to project {code}", notification.WorkStarted,
                notification.WorkEnded, notification.EmployeeEmailAddress, notification.ProjectCode);
            return Task.CompletedTask;
        }

        private readonly ILogger<WorklogRemovedEventHandler> _logger;

        /// <summary>
        /// Default constructor for the event handler for removed worklog events
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/></param>
        public WorklogRemovedEventHandler(ILogger<WorklogRemovedEventHandler> logger)
        {
            _logger = logger;
        }
    }
}