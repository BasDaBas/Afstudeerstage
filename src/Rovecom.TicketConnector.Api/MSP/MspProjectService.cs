using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Rovecom.TicketConnector.Domain.Entities.ProjectEntity;
using Rovecom.TicketConnector.Domain.MSP.MspProjectEntity;
using Rovecom.TicketConnector.Domain.UnitOfWork;
using Rovecom.TicketConnector.Infrastructure.MSP.Adapters;

namespace Rovecom.TicketConnector.Api.MSP
{
    /// <inheritdoc />
    public class MspProjectService : IMspProjectService
    {
        private readonly IDapperUnitOfWorkFactory _dapperUowFactory;
        private readonly ILogger<MspProjectService> _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dapperUowFactory"><see cref="IDapperUnitOfWorkFactory"/></param>
        /// <param name="logger"><see cref="ILogger"/></param>
        public MspProjectService(IDapperUnitOfWorkFactory dapperUowFactory, ILogger<MspProjectService> logger)
        {
            _dapperUowFactory = dapperUowFactory;
            _logger = logger;
        }

        /// <inheritdoc />
        public IEnumerable<MspProject> GetAll()
        {
            using (var uow = _dapperUowFactory.Create())
            {
                return uow.MspProjectRepository.GetAll();
            }
        }

        /// <inheritdoc />
        public IEnumerable<IProject> GetProjectsAsAdapted()
        {
            var msProjects = new List<IProject>();
            var projects = GetAll();

            foreach (var project in projects)
            {
                if (!HasSameAccount(project))
                {
                    _logger.LogWarning("Requests belonging to same project {1} reference different accounts", project.Code);
                    continue;
                }

                msProjects.Add(new MspProjectAdapter(project, _dapperUowFactory));
            }
            return msProjects;
        }

        /// <inheritdoc />
        public void CreateProjects(IEnumerable<IProject> newProjects)
        {
            using (var uow = _dapperUowFactory.Create())
            {
                foreach (var project in newProjects)
                {
                    uow.MspProjectRepository.Add(new MspProject(0, project.Code));
                }
            }
        }

        /// <inheritdoc />
        public void UpdateProjects(IEnumerable<IProject> updatedMspProjects)
        {
            using (var uow = _dapperUowFactory.Create())
            {
                foreach (var mspProject in updatedMspProjects)
                {
                    uow.MspProjectRepository.Update(mspProject);
                }

                uow.Commit();
            }
        }

        // Sets the accounts on all requests
        private static bool HasSameAccount(MspProject project)
        {
            var siteId = project.Requests.First().SiteId;
            return project.Requests.All(x => x.SiteId == siteId);
        }
    }
}