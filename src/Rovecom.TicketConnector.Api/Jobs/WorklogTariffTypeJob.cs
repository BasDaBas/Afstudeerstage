using Rovecom.TicketConnector.Domain.Entities;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity;
using Rovecom.TicketConnector.Domain.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Api.Jobs
{
    /// <summary>
    /// Job for worklog tariff types
    /// </summary>
    public class WorklogTariffTypeJob
    {
        private readonly ISisWorklogTariffTypeRepository _sisWorklogTariffTypeRepo;
        private readonly IDapperUnitOfWorkFactory _dapperUowFactory;

        /// <summary>
        /// Default constructor for worklog tariff type job
        /// </summary>
        /// <param name="sisWorklogTariffTypeRepo"><see cref="ISisWorklogTariffTypeRepository"/></param>
        /// <param name="dapperUowFactory"><see cref="IDapperUnitOfWorkFactory"/></param>
        public WorklogTariffTypeJob(ISisWorklogTariffTypeRepository sisWorklogTariffTypeRepo, IDapperUnitOfWorkFactory dapperUowFactory)
        {
            _sisWorklogTariffTypeRepo = sisWorklogTariffTypeRepo;
            _dapperUowFactory = dapperUowFactory;
        }

        /// <summary>
        /// Synchronizes the MSP worklog tariff types to match the SIS worklog tariff types
        /// </summary>
        public async Task SyncWorklogTariffTypes()
        {
            var sisWorklogTariffTypes = await _sisWorklogTariffTypeRepo.GetAllAsync();
            List<ITypeEntity> mspWorklogTariffTypes;
            using (var uow = _dapperUowFactory.Create())
            {
                mspWorklogTariffTypes = uow.MspWorklogTariffTypeRepository.GetAllActive().ToList<ITypeEntity>();
                uow.Commit();
            }

            var newTypes = sisWorklogTariffTypes.Except(mspWorklogTariffTypes);
            var removedTypes = mspWorklogTariffTypes.Except(sisWorklogTariffTypes);

            using (var uow = _dapperUowFactory.Create())
            {
                foreach (var worklogTariffType in newTypes)
                {
                    uow.MspWorklogTariffTypeRepository.Add(new MspWorklogTariffType(0, worklogTariffType.Description));
                }

                // Removed
                foreach (var worklogTariffType in removedTypes)
                {
                    var mspWorklogTariffType = worklogTariffType as MspWorklogTariffType;
                    uow.MspWorklogTariffTypeRepository.Remove(mspWorklogTariffType);
                }
                uow.Commit();
            }
        }
    }
}