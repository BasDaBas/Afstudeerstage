using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.SIS.SisAccountEntity;
using Rovecom.TicketConnector.Domain.SIS.SisEmployeeEntity;
using Rovecom.TicketConnector.Domain.SIS.SisProjectEntity;
using Rovecom.TicketConnector.Infrastructure.SIS.SisProjectAdapter;

namespace Rovecom.TicketConnector.Api.SIS
{
    /// <inheritdoc />
    public class SisProjectService : ISisProjectService
    {
        private readonly ISisProjectRepository _projectRepo;
        private readonly ISisAccountRepository _accountRepo;
        private readonly ISisEmployeeRepository _employeeRepo;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="projectRepo"><see cref="ISisProjectRepository"/></param>
        /// <param name="accountRepo"><see cref="ISisAccountRepository"/></param>
        /// <param name="employeeRepo"><see cref="ISisEmployeeRepository"/></param>
        public SisProjectService(ISisProjectRepository projectRepo, ISisAccountRepository accountRepo, ISisEmployeeRepository employeeRepo)
        {
            _projectRepo = projectRepo;
            _accountRepo = accountRepo;
            _employeeRepo = employeeRepo;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<IProject>> GetProjectsAsAdaptedAsync()
        {
            var projects = await GetSisProjectsAsync();
            var adaptedProjectsTasks = projects.Select(async x => new SisProjectAdapter(x, await GetAccountCodeAsync(x), _employeeRepo));
            var adaptedProjects = await Task.WhenAll(adaptedProjectsTasks);
            return adaptedProjects.ToList<IProject>();
        }

        /// <inheritdoc />
        public void UpdateProjects(List<IChangeableProject> changedSisProjects)
        {
            foreach (var sisProject in changedSisProjects)
            {
                _projectRepo.Update(sisProject);
            }
        }

        // Gets the code from the account associated to the project
        private async Task<string> GetAccountCodeAsync(SisProject project)
        {
            var account = await _accountRepo.GetByIdAsync(project.AccountId);
            return account.Code;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SisProject>> GetSisProjectsAsync()
        {
            return await _projectRepo.GetAllAsync();
        }
    }
}