using Hangfire;
using Hangfire.MemoryStorage;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rovecom.TicketConnector.Api.Jobs;
using Rovecom.TicketConnector.Api.Services;
using Rovecom.TicketConnector.Domain;
using Rovecom.TicketConnector.Domain.Entities.WorklogChangeEntity;
using Rovecom.TicketConnector.Domain.MSP.MspAccountEntity;
using Rovecom.TicketConnector.Domain.SIS.SisProjectEntity;
using Rovecom.TicketConnector.Domain.UnitOfWork;
using Rovecom.TicketConnector.Infrastructure.MSP.Repositories;
using Rovecom.TicketConnector.Infrastructure.SIS.Repositories;
using Rovecom.TicketConnector.Infrastructure.UnitOfWork;
using System;
using System.Net;
using System.Net.Http;
using Rovecom.TicketConnector.Api.ConfigOptions;
using Rovecom.TicketConnector.Api.MSP;
using Rovecom.TicketConnector.Api.SIS;
using Rovecom.TicketConnector.Domain.Services;
using Rovecom.TicketConnector.Domain.SIS.SisAccountEntity;
using Rovecom.TicketConnector.Domain.SIS.SisEmployeeEntity;
using Rovecom.TicketConnector.Domain.SIS.SisWorklogEntity;
using Rovecom.TicketConnector.Infrastructure.SIS;

namespace Rovecom.TicketConnector.Api
{
    /// <summary>
    /// Startup class to define services and configuration.
    /// </summary>
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// Constructor used by Program.cs to add environment variables and load appsettings.json.
        /// </summary>
        /// <param name="env">Environment to run in (Development, Staging, Production)</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();

            _env = env;
        }

        /// <summary>
        /// Configuration property to read appsettings.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Collection of defined services (Dependency Injection)</param>

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:MspConnection"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Failed to get connection string from configuration");
            }

            services.AddOptions();
            services.Configure<SmtpConfigOptions>(Configuration.GetSection("SmtpConfig"));
            services.Configure<SisApiConfig>(Configuration.GetSection("SisApiConfig"));

            services.AddMvc();
            services.AddSingleton(Configuration);

            services.AddDbContext<ConnectorContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ConnectorConnection")));

            services.AddMediatR(typeof(Startup));

            services.AddHangfire(x => x.UseMemoryStorage());

            // Configure and start http client for SIS communication
            var endPointA = new Uri(Configuration.GetSection("SisApiConfig").GetValue<string>("Url"));
            var httpClient = new HttpClient
            {
                BaseAddress = endPointA,
            };
            ServicePointManager.FindServicePoint(endPointA).ConnectionLeaseTimeout = 60000; // sixty seconds
            services.AddSingleton(httpClient);

            services.AddTransient<IDapperUnitOfWorkFactory, DapperUnitOfWorkFactory>();
            services.AddTransient<IDapperUnitOfWork, DapperUnitOfWork>();
            services.AddTransient<ISisProjectRepository, SisProjectRepository>();
            services.AddTransient<IMspAccountRepository, MspAccountRepository>();
            services.AddTransient<IWorklogChangeFactory, WorklogChangeFactory>();
            services.AddTransient<IMspProjectService, MspProjectService>();
            services.AddTransient<ISisProjectService, SisProjectService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IProjectChangeService, ProjectChangeService>();
            services.AddTransient<ISisAccountRepository, SisAccountRepository>();
            services.AddTransient<ISisEmployeeRepository, SisEmployeeRepository>();
            services.AddTransient<ISisApiClient, SisApiClient>();
            services.AddTransient<ISisWorklogTariffTypeRepository, SisWorklogTariffTypeRepository>();
            services.AddTransient<ISisWorklogTypeRepository, SisWorklogTypeRepository>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application builder to define middleware.</param>
        /// <param name="env">Environment to run in (Development, Staging, Production)</param>

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireServer();
            app.UseHangfireDashboard();
            app.UseMvc();


            // Fire every 5 minutes
            RecurringJob.AddOrUpdate<AccountSyncJob>(x => x.SyncAccountsAsync(), "5 * * * *");
            RecurringJob.AddOrUpdate<ProjectSyncJob>(x => x.SyncProjectAsync(), "5 * * * *");
            RecurringJob.AddOrUpdate<WorklogTariffTypeJob>(x => x.SyncWorklogTariffTypes(), "5 * * * *");
            RecurringJob.AddOrUpdate<WorklogTypeJob>(x => x.SyncWorklogTypesAsync(), "5 * * * *");

            // Fire the job at 01:00 on a daily basis
            RecurringJob.AddOrUpdate<ProjectSyncJob>(x => x.SyncProjectDetailsAsync(), "0 1 * * *");
        }
    }
}