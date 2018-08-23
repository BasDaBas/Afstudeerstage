using Rovecom.TicketConnector.Api.MSP;
using Rovecom.TicketConnector.Api.Services;
using Rovecom.TicketConnector.Api.SIS;
using Rovecom.TicketConnector.Domain;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using System.Linq;
using System.Threading.Tasks;

namespace Rovecom.TicketConnector.Api.Jobs
{
    /// <summary>
    /// Job for synchronizing the project details
    /// </summary>
    public class ProjectSyncJob
    {
        private readonly ISisProjectService _sisProjectService;
        private readonly IMspProjectService _mspProjectService;
        private readonly IProjectService _projectService;
        private readonly ConnectorContext _context;

        /// <summary>
        /// Initializes the job synchronizing the project details
        /// </summary>
        /// <param name="projectService"><see cref="IProjectService"/></param>
        /// <param name="sisProjectService"><see cref="ISisProjectService"/></param>
        /// <param name="mspProjectService"><see cref="IMspProjectService"/></param>
        /// <param name="context"><see cref="ConnectorContext"/></param>
        public ProjectSyncJob(IProjectService projectService, ISisProjectService sisProjectService, IMspProjectService mspProjectService, ConnectorContext context)
        {
            _sisProjectService = sisProjectService;
            _mspProjectService = mspProjectService;
            _projectService = projectService;
            _context = context;
        }

        /// <summary>
        /// Synchronises the project
        /// </summary>
        public async Task SyncProjectAsync()
        {
            var sisProjects = await _sisProjectService.GetProjectsAsAdaptedAsync();
            var mspProjects = _mspProjectService.GetProjectsAsAdapted().ToList();
            var newProjects = sisProjects.ToList().Except(mspProjects);
            _mspProjectService.CreateProjects(newProjects);
        }

        /// <summary>
        /// Synchronises the project details
        /// </summary>
        public async Task SyncProjectDetailsAsync()
        {
            // Get the new data from SIS and MSP
            var sisProjects = await _sisProjectService.GetProjectsAsAdaptedAsync();
            var newSisProjects = _projectService.GetProjectsAsChangeable(sisProjects);
            var newMspProjects = _projectService.GetProjectsAsChangeable(_mspProjectService.GetProjectsAsAdapted());

            //Get old project data
            var oldProjects = _context.Projects.ToList<IProject>();

            // Generate changes on the projects
            var changedSisProjects = _projectService.GenerateChanges(newMspProjects, newSisProjects, oldProjects).ToList();
            var changedMspProjects = _projectService.GenerateChanges(newSisProjects, newMspProjects, oldProjects).ToList();

            // Apply the changes to the projects
            _projectService.ApplyChanges(changedSisProjects);
            _projectService.ApplyChanges(changedMspProjects);

            // Persist the updated projects
            _sisProjectService.UpdateProjects(changedSisProjects);
            _mspProjectService.UpdateProjects(changedMspProjects);

            // Create new shapshot of the current projects
            _context.Projects.RemoveRange(_context.Projects);
            _context.Projects.AddRange(newSisProjects.Select(x => _projectService.Copy(x)));
        }
    }
}