using MediatR;
using Microsoft.EntityFrameworkCore;
using Rovecom.TicketConnector.Domain.Entities.AccountEntity;
using Rovecom.TicketConnector.Domain.Entities.EmployeeEntity;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.Extensions;

namespace Rovecom.TicketConnector.Domain
{
    /// <inheritdoc />
    /// <summary>
    /// The context of the connector database
    /// </summary>
    public class ConnectorContext : DbContext
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public ConnectorContext(DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Accounts
        /// </summary>
        public virtual DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Projects
        /// </summary>
        public virtual DbSet<Project> Projects { get; set; }

        /// <summary>
        /// Employees
        /// </summary>
        public virtual DbSet<Employee> Employees { get; set; }

        /// <inheritdoc />
        public override int SaveChanges()
        {
            // Dispatch Domain Events collection.
            _mediator.DispatchDomainEvents(this);

            return base.SaveChanges();
        }
    }
}