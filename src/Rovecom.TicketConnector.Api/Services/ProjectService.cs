using System;
using Microsoft.Extensions.Logging;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace Rovecom.TicketConnector.Api.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Service class for projects
    /// </summary>
    public class ProjectService : IProjectService
    {
        private readonly IProjectChangeService _projectChangeService;
        private readonly ILogger<IProjectService> _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="projectChangeService"><see cref="IProjectChangeService"/>></param>
        /// <param name="logger"><see cref="ILogger"/></param>
        public ProjectService(IProjectChangeService projectChangeService, ILogger<IProjectService> logger)
        {
            _projectChangeService = projectChangeService;
            _logger = logger;
        }

        /// <inheritdoc />
        public List<IChangeableProject> GetProjectsAsChangeable(IEnumerable<IProject> projects)
        {
            return projects.Select(x => new ChangeableProject(x)).ToList<IChangeableProject>();
        }

        /// <inheritdoc />
        public IEnumerable<IChangeableProject> GenerateChanges(IEnumerable<IChangeableProject> targetProjects,
            IEnumerable<IChangeableProject> newComparableProjects,
            IEnumerable<IProject> oldComparableProjects)
        {
            var changedProjects = new List<IChangeableProject>();
            var targetProjectsDict = targetProjects.ToDictionary(x => x.Code);
            var newProjectsDict = newComparableProjects.ToDictionary(x => x.Code);

            // Get changes
            foreach (var oldProject in oldComparableProjects)
            {
                // Find newer version of project
                if (!newProjectsDict.TryGetValue(oldProject.Code, out var newProject))
                {
                    _logger.LogWarning("Cannot generate changes, newer version of {1} {2} cannot be found", oldProject.GetType(), oldProject.Code);
                    continue;
                }

                // Find similar project in collection from other system
                if (!targetProjectsDict.TryGetValue(newProject.Code, out var targetProject))
                {
                    _logger.LogWarning("Cannot generate changes on target project, target project for {0)} {1} was not found in other data source",
                        newProject.GetType(), oldProject.Code);
                    continue;
                }

                // Make sure both projects are the same, otherwise skip this project
                if (!string.Equals(newProject.AccountCode, targetProject.AccountCode))
                {
                    _logger.LogWarning("Account code {0} of target project differs from account code {1} of source project",
                        newProject.AccountCode, targetProject.AccountCode);
                    continue;
                }

                var hasChanges = _projectChangeService.TryGenerateChanges(targetProject, oldProject, newProject);

                if (hasChanges)
                    changedProjects.Add(targetProject);
            }

            return changedProjects;
        }

        /// <inheritdoc />
        public void ApplyChanges(IEnumerable<IChangeableProject> changedProjects)
        {
            foreach (var project in changedProjects)
            {
                project.ApplyChanges();
            }
        }

        /// <inheritdoc />
        public Project Copy(IProject project)
        {
            var copiedProject = new Project(project.Code, project.AccountCode);
            foreach (var projectWorklog in project.Worklogs)
            {
                copiedProject.AddWorklog(projectWorklog.WorkStartedDateTime, projectWorklog.WorkEndedDateTime, projectWorklog.Description,
                    projectWorklog.KilometresCovered, projectWorklog.EmployeeEmailAddress);
            }

            return copiedProject;
        }
    }
}