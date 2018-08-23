using MediatR;
using Microsoft.Extensions.Logging;
using Rovecom.TicketConnector.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Api.DomainEventHandlers
{
    /// <inheritdoc />
    /// <summary>
    /// Handles worklog added domain events
    /// </summary>
    public class WorklogAddedEventHandler : INotificationHandler<WorklogAddedEvent>
    {
        private readonly ILogger<WorklogAddedEventHandler> _logger;

        /// <summary>
        /// Default constructor for the event handler for worklog added events
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/></param>
        public WorklogAddedEventHandler(ILogger<WorklogAddedEventHandler> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public Task Handle(WorklogAddedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worklog {workStarted}, {workEnded} with employee {email} added to project {code}", notification.WorkStarted, notification.WorkEnded, notification.EmployeeEmailAddress, notification.ProjectCode);
            return Task.CompletedTask;
        }
    }
}