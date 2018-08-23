using Rovecom.TicketConnector.Domain.Entities.WorklogEntity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rovecom.TicketConnector.Domain.Entities.ProjectEntity
{
    /// <inheritdoc cref="IChangeableProject" />
    public class ChangeableProject : Entity, IChangeableProject
    {
        private readonly List<IChange> _changes;
        private readonly IProject _project;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="project">The original project</param>
        public ChangeableProject(IProject project)
        {
            _project = project;
            _changes = new List<IChange>();
        }

        /// <inheritdoc />
        public string Code
        {
            get => _project.Code;
            set => _project.Code = value;
        }

        /// <inheritdoc />
        public IReadOnlyCollection<IWorklog> Worklogs => _project.Worklogs;

        /// <inheritdoc />
        public void AddWorklog(DateTime workStartedDateTime, DateTime workEndedDateTime, string description, double kilometersCovered,
            string employeeEmail)
        {
            _project.AddWorklog(workStartedDateTime, workEndedDateTime, description, kilometersCovered, employeeEmail);
        }

        /// <inheritdoc />
        public void RemoveWorklog(IWorklog worklog)
        {
            _project.RemoveWorklog(worklog);
        }

        /// <inheritdoc />
        public string AccountCode => _project.AccountCode;

        /// <inheritdoc />
        public void AddChanges(IEnumerable<IChange> changes)
        {
            _changes.AddRange(changes);
        }

        /// <inheritdoc />
        public IEnumerable<IChange> GetConflictingChangesWith(IEnumerable<IChange> changes)
        {
            return changes.Where(HasConflictingChangeWith);
        }

        // Checks if projects has changes that conflict with the given change
        private bool HasConflictingChangeWith(IChange change)
        {
            var conflictingChanges = _changes.Where(x => x.IsConflicting(change)).ToList();
            foreach (var conflictingChange in conflictingChanges)
            {
                var conflictingChangeEvent = conflictingChange.CreateConflictingChangeEvent(Code);
                AddDomainEvent(conflictingChangeEvent);
            }

            return conflictingChanges.Any();
        }

        /// <inheritdoc />
        public void ClearChanges()
        {
            _changes.Clear();
        }

        /// <inheritdoc />
        public void ApplyChanges()
        {
            foreach (var change in _changes)
            {
                change.Apply(this);
            }
            _changes.Clear();
        }
    }
}