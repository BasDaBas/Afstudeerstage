using Rovecom.TicketConnector.Domain.Entities;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using Rovecom.TicketConnector.Domain.UnitOfWork;
using Rovecom.TicketConnector.Infrastructure.SIS.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Api.Jobs
{
    /// <summary>
    /// Job for worklog types
    /// </summary>
    public class WorklogTypeJob
    {
        private readonly SisWorklogTypeRepository _sisWorklogTypeRepo;
        private readonly IDapperUnitOfWorkFactory _dapperUowFactory;

        /// <summary>
        /// Defauly constructor for the worklog type
        /// </summary>
        /// <param name="sisWorklogTypeRepo"><see cref="SisWorklogTypeRepository"/></param>
        /// <param name="dapperUowFactory"><see cref="IDapperUnitOfWorkFactory"/></param>
        public WorklogTypeJob(SisWorklogTypeRepository sisWorklogTypeRepo, IDapperUnitOfWorkFactory dapperUowFactory)
        {
            _sisWorklogTypeRepo = sisWorklogTypeRepo;
            _dapperUowFactory = dapperUowFactory;
        }

        /// <summary>
        /// Synchronizes the MSP worklog types to match the SIS worklog types
        /// </summary>
        public async Task SyncWorklogTypesAsync()
        {
            // Get current worklog types in both systems
            var sisWorklogTypes = await _sisWorklogTypeRepo.GetAllAsync();
            List<ITypeEntity> mspWorklogTariffTypes;
            using (var uow = _dapperUowFactory.Create())
            {
                mspWorklogTariffTypes = uow.MspWorklogTypeRepository.GetAllActive().ToList<ITypeEntity>();
                uow.Commit();
            }

            // Compare worklog types in both systems
            var newTypes = sisWorklogTypes.Except(mspWorklogTariffTypes);
            var removedTypes = mspWorklogTariffTypes.Except(sisWorklogTypes);

            using (var uow = _dapperUowFactory.Create())
            {
                // Add new worklog types
                foreach (var worklogType in newTypes)
                {
                    uow.MspWorklogTypeRepository.Add(new MspWorklogType { Description = worklogType.Description, IsRemoved = false });
                }

                // Remove deleted worklog type
                foreach (var worklogType in removedTypes)
                {
                    var mspWorklogType = worklogType as MspWorklogType;
                    uow.MspWorklogTypeRepository.Remove(mspWorklogType);
                }
                uow.Commit();
            }
        }
    }
}