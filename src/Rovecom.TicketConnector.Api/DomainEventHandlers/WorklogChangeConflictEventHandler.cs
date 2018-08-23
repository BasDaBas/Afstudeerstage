using MediatR;
using Microsoft.Extensions.Logging;
using Rovecom.TicketConnector.Domain.Events;
using System.Threading;
using System.Threading.Tasks;
using Rovecom.TicketConnector.Api.Services;

namespace Rovecom.TicketConnector.Api.DomainEventHandlers
{
    /// <inheritdoc />
    public class WorklogChangeConflictEventHandler : INotificationHandler<WorklogChangeConflictEvent>
    {
        private readonly ILogger<WorklogChangeConflictEventHandler> _logger;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Default constructor for the event handler for removed worklog events
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/></param>
        /// <param name="emailService"><see cref="IEmailService"/></param>
        public WorklogChangeConflictEventHandler(ILogger<WorklogChangeConflictEventHandler> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        /// <inheritdoc />
        public Task Handle(WorklogChangeConflictEvent notification, CancellationToken cancellationToken)
        {
            _emailService.SendEmail("Subject", "Message", "recipient@test.com");

            _logger.LogWarning(
                "Worklog change of type {0} with values {1}, {2}, {3} in project {4} resulted in a conflict",
                notification.WorklogChange.WorklogChangeState.GetType(),
                notification.WorklogChange.Worklog.WorkStartedDateTime,
                notification.WorklogChange.Worklog.WorkEndedDateTime,
                notification.WorklogChange.Worklog.EmployeeEmailAddress,
                notification.ProjectCode
            );
            return Task.CompletedTask;
        }
    }
}