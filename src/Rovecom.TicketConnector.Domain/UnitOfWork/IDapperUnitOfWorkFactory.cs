namespace Rovecom.TicketConnector.Domain.UnitOfWork
{
    /// <summary>
    /// Factory for Dapper Unit of Work
    /// </summary>
    public interface IDapperUnitOfWorkFactory
    {
        /// <summary>
        /// Creates a Dapper Unit of Work
        /// </summary>
        /// <returns></returns>
        IDapperUnitOfWork Create();
    }
}