using Microsoft.Extensions.Configuration;
using Npgsql;
using Rovecom.TicketConnector.Domain.UnitOfWork;

namespace Rovecom.TicketConnector.Infrastructure.UnitOfWork
{
    /// <inheritdoc />
    public class DapperUnitOfWorkFactory : IDapperUnitOfWorkFactory
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructor for the Dapper Unit of Work factory
        /// </summary>
        public DapperUnitOfWorkFactory(IConfiguration config)
        {
            _config = config;
        }

        /// <inheritdoc />
        public IDapperUnitOfWork Create()
        {
            var connectionString = _config.GetConnectionString("MspConnection");
            return new DapperUnitOfWork(new NpgsqlConnection(connectionString));
        }
    }
}