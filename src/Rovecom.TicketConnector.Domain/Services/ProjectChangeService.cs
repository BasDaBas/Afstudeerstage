using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rovecom.TicketConnector.Domain.Entities;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.Entities.WorklogChangeEntity;

namespace Rovecom.TicketConnector.Domain.Services
{
    /// <inheritdoc />
    public class ProjectChangeService : IProjectChangeService
    {
        private readonly IWorklogChangeFactory _worklogChangeFactory;

        /// <summary>
        /// Default constuctor for project change service
        /// </summary>
        /// <param name="worklogChangeFactory"></param>
        public ProjectChangeService(IWorklogChangeFactory worklogChangeFactory)
        {
            _worklogChangeFactory = worklogChangeFactory;
        }

        /// <inheritdoc />
        public bool TryGenerateChanges(IChangeableProject targetProject, IProject oldProject, IChangeableProject newProject)
        {
            var changes = new List<IChange>();

            // Generate remove worklog changes
            var removedWorklogChanges = oldProject.Worklogs.Except(newProject.Worklogs, new WorklogEqualityComparer())
                .Select(x => _worklogChangeFactory.GetWorklogRemovedChange(x)).ToList();
            changes.AddRange(removedWorklogChanges);

            // Generate added worklog changes
            var addedWorklogChanges = newProject.Worklogs.Except(oldProject.Worklogs, new WorklogEqualityComparer())
                .Select(x => _worklogChangeFactory.GetWorklogAddedChange(x)).ToList();
            changes.AddRange(addedWorklogChanges);

            // Check if the generated changes are conflicting with the change of same project in another system
            var conflictingChanges = newProject.GetConflictingChangesWith(changes).ToList();

            // Check if there are conflicts
            if (conflictingChanges.Any())
            {
                foreach (var conflictingChange in conflictingChanges)
                {
                    newProject.AddDomainEvent(conflictingChange.CreateConflictingChangeEvent(newProject.Code));
                }

                // Remove the changes on the other project aswell
                newProject.ClearChanges();

                // Skip this project
                return false;
            }

            targetProject.AddChanges(removedWorklogChanges);
            targetProject.AddChanges(addedWorklogChanges);
            return true;
        }
    }
}
