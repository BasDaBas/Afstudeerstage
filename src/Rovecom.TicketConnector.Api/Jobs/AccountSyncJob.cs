using Rovecom.TicketConnector.Api.Helpers;
using Rovecom.TicketConnector.Domain.Entities.AccountEntity;
using Rovecom.TicketConnector.Domain.MSP.MspAccountEntity;
using Rovecom.TicketConnector.Domain.SIS.SisAccountEntity;
using Rovecom.TicketConnector.Domain.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Api.Jobs
{
    /// <summary>
    /// Job for synchronizing the MSP accounts
    /// </summary>
    public class AccountSyncJob
    {
        private readonly ISisAccountRepository _sisAccountRepository;
        private readonly IDapperUnitOfWorkFactory _dapperUowFactory;

        /// <summary>
        /// Initializes the job synchronizing the MSP accounts
        /// </summary>
        /// <param name="sisAccountRepository"><see cref="ISisAccountRepository"/></param>
        /// <param name="dapperUowFactory"><see cref="IDapperUnitOfWorkFactory"/></param>
        public AccountSyncJob(ISisAccountRepository sisAccountRepository, IDapperUnitOfWorkFactory dapperUowFactory)
        {
            _sisAccountRepository = sisAccountRepository;
            _dapperUowFactory = dapperUowFactory;
        }

        /// <summary>
        /// Synchronizes the MSP accounts
        /// </summary>
        public async Task SyncAccountsAsync()
        {
            HashSet<IAccount> mspAccounts;
            using (var uow = _dapperUowFactory.Create())
            {
                mspAccounts = uow.MspAccountRepository.GetAll().ToHashSet<IAccount>();
                uow.Commit();
            }

            var sisAccounts = await _sisAccountRepository.GetAllAsync();
            var sisAccountsHashSet = sisAccounts.ToHashSet<IAccount>();

            // Get updated/new accounts in SIS by comparing to MSP accounts
            var changedAccounts2 = sisAccountsHashSet.Except(mspAccounts, new AccountValueComparer()).ToList();

            // Get new accounts by finding all accounts that do not exists
            var newAccounts = changedAccounts2.Except(mspAccounts, new AccountEqualityComparer());

            // Get updated accounts by finding all accounts with a code that does exist in MSP
            var updatedAccounts = changedAccounts2.Intersect(mspAccounts, new AccountEqualityComparer());

            using (var uow = _dapperUowFactory.Create())
            {
                foreach (var account in newAccounts)
                {
                    var mspAccount = new MspAccount(account);
                    uow.MspAccountRepository.Create(mspAccount);
                }
                foreach (var account in updatedAccounts)
                {
                    var mspAccount = new MspAccount(account);
                    uow.MspAccountRepository.Update(mspAccount);
                }
                uow.Commit();
            }
        }
    }
}