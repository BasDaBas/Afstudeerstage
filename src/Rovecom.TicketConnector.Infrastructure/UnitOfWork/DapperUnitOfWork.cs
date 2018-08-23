using Rovecom.TicketConnector.Domain.MSP.MspAccountEntity;
using Rovecom.TicketConnector.Domain.MSP.MspProjectEntity;
using Rovecom.TicketConnector.Domain.UnitOfWork;
using System;
using System.Data;
using Rovecom.TicketConnector.Domain.MSP.MspTechnicianEntity;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using Rovecom.TicketConnector.Infrastructure.MSP.Repositories;

namespace Rovecom.TicketConnector.Infrastructure.UnitOfWork
{
    /// <inheritdoc />
    public class DapperUnitOfWork : IDapperUnitOfWork
    {
        private IDbConnection _connection;
        private bool _disposed;
        private IMspAccountRepository _mspAccountRepository;
        private IMspProjectRepository _mspProjectRepository;
        private IMspTechnicianRepository _mspTechnicianRepository;
        private IMspWorklogTariffTypeRepository _mspWorklogTariffTypeRepository;
        private IMspWorklogTypeRepository _mspWorklogTypeRepository;
        private IDbTransaction _transaction;

        /// <summary>
        /// Default constructor for a dapper unit of work
        /// </summary>
        public DapperUnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Destructor for Dapper Unit of Work
        /// </summary>
        ~DapperUnitOfWork()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public IMspAccountRepository MspAccountRepository =>
            _mspAccountRepository ?? (_mspAccountRepository = new MspAccountRepository(_transaction));
        /// <inheritdoc />
        public IMspProjectRepository MspProjectRepository =>
            _mspProjectRepository ?? (_mspProjectRepository = new MspProjectRepository(_transaction));
        /// <inheritdoc />
        public IMspTechnicianRepository MspTechnicianRepository =>
            _mspTechnicianRepository ?? (_mspTechnicianRepository = new MspTechnicianRepository(_transaction));
        /// <inheritdoc />
        public IMspWorklogTariffTypeRepository MspWorklogTariffTypeRepository =>
            _mspWorklogTariffTypeRepository ?? (_mspWorklogTariffTypeRepository = new MspWorklogTariffTypeRepository(_transaction));
        /// <inheritdoc />
        public IMspWorklogTypeRepository MspWorklogTypeRepository =>
            _mspWorklogTypeRepository ?? (_mspWorklogTypeRepository = new MspWorklogTypeRepository(_transaction));

        /// <inheritdoc />
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        /// <summary>
        /// Closes the connection
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }

                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }

            _disposed = true;
        }

        private void ResetRepositories()
        {
            _mspAccountRepository = null;
        }
    }
}