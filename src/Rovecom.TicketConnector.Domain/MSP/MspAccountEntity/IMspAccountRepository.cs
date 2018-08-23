using System.Collections.Generic;

namespace Rovecom.TicketConnector.Domain.MSP.MspAccountEntity
{
    /// <summary>
    /// The MSP account repository
    /// </summary>
    public interface IMspAccountRepository
    {
        /// <summary>
        /// Creates the MspAccount
        /// </summary>
        /// <param name="account"></param>
        void Create(MspAccount account);

        /// <summary>
        /// Gets a Msp Account by id
        /// </summary>
        /// <param name="id">The id of the MSP Account</param>
        /// <returns>A MSP Account</returns>
        MspAccount Get(long id);

        /// <summary>
        /// Gets a Msp Account by default site id
        /// </summary>
        /// <param name="id">The default site id of the MSP Account</param>
        /// <returns>A MSP Account</returns>
        MspAccount GetByDefaultSiteId(long id);

        /// <summary>
        /// Gets all MspAccounts
        /// </summary>
        IEnumerable<MspAccount> GetAll();

        /// <summary>
        /// Gets the account by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        MspAccount GetByCode(string code);

        /// <summary>
        /// Updates the MspAccount
        /// </summary>
        /// <param name="account"></param>
        void Update(MspAccount account);
    }
}