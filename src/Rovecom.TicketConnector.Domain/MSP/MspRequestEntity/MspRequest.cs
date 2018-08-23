using Rovecom.TicketConnector.Domain.MSP.MspAccountEntity;
using Rovecom.TicketConnector.Domain.MSP.MspWorklogEntity;
using System.Collections.Generic;
using System.Linq;

namespace Rovecom.TicketConnector.Domain.MSP.MspRequestEntity
{
    /// <summary>
    /// A request in MSP
    /// </summary>
    public class MspRequest
    {
        private List<MspWorklog> _worklogs;

        public MspRequest(string title, string description, long siteId)
        {
            Title = title;
            Description = description;
            SiteId = siteId;

            _worklogs = new List<MspWorklog>();
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the site id
        /// </summary>
        public long SiteId { get; set; }

        /// <summary>
        /// Gets or sets the worklog collection
        /// </summary>
        public IReadOnlyCollection<MspWorklog> Worklogs
        {
            get => _worklogs;
            set => _worklogs = value.ToList();
        }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Adds a worklog to the request
        /// </summary>
        /// <param name="worklog"></param>
        public void AddWorklog(MspWorklog worklog)
        {
            if (_worklogs.Contains(worklog, new MspWorklogComparer()))
                return;

            _worklogs.Add(worklog);
        }

        /// <summary>
        /// Removes a worklog from the request
        /// </summary>
        /// <param name="worklog"></param>
        public void RemoveWorklog(MspWorklog worklog)
        {
            var wlEqC = new MspWorklogComparer();
            var removedWorklog = Worklogs.FirstOrDefault(x => wlEqC.Equals(x, worklog));

            if (removedWorklog != null)
                _worklogs.Remove(removedWorklog);
        }
    }
}