using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Domain.SIS.SisAccountEntity
{
    /// <summary>
    /// Repository class for accounts in SIS
    /// </summary>
    public interface ISisAccountRepository
    {
        /// <summary>
        /// Gets all ICTS accounts in SIS
        /// </summary>
        Task<IReadOnlyCollection<SisAccount>> GetAllAsync();

        /// <summary>
        /// Gets the account with the matching Id
        /// </summary>
        /// <param name="id">Id of the account</param>
        /// <returns><see cref="SisAccount"/></returns>
        Task<SisAccount> GetByIdAsync(int id);

        /// <summary>
        /// Gets the account with the matching code
        /// </summary>
        /// <param name="code">Code of the account</param>
        /// <returns><see cref="SisAccount"/></returns>
        Task<SisAccount> GetByCodeAsync(string code);
    }
}