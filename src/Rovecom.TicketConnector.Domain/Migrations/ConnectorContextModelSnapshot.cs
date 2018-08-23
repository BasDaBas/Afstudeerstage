﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Rovecom.TicketConnector.Domain;
using System;

namespace Rovecom.TicketConnector.Domain.Migrations
{
    [DbContext(typeof(ConnectorContext))]
    partial class ConnectorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Rovecom.TicketConnector.Domain.Entities.AccountEntity.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FaxNumber");

                    b.Property<string>("Name");

                    b.Property<string>("TelephoneNumber");

                    b.Property<string>("WebsiteUrl");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Rovecom.TicketConnector.Domain.Entities.EmployeeEntity.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Rovecom.TicketConnector.Domain.Entities.ProjectEntity.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountCode");

                    b.Property<long?>("AccountId");

                    b.Property<string>("Code");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Rovecom.TicketConnector.Domain.Entities.WorklogEntity.Worklog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("EmployeeEmailAddress");

                    b.Property<int?>("EmployeeId");

                    b.Property<double>("KilometresCovered");

                    b.Property<string>("ProjectCode");

                    b.Property<long?>("ProjectId");

                    b.Property<DateTime>("WorkEndedDateTime");

                    b.Property<DateTime>("WorkStartedDateTime");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Worklog");
                });

            modelBuilder.Entity("Rovecom.TicketConnector.Domain.Entities.ProjectEntity.Project", b =>
                {
                    b.HasOne("Rovecom.TicketConnector.Domain.Entities.AccountEntity.Account", "Account")
                        .WithMany("Projects")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("Rovecom.TicketConnector.Domain.Entities.WorklogEntity.Worklog", b =>
                {
                    b.HasOne("Rovecom.TicketConnector.Domain.Entities.EmployeeEntity.Employee")
                        .WithMany("Worklogs")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("Rovecom.TicketConnector.Domain.Entities.ProjectEntity.Project")
                        .WithMany("ConnectorWorklogs")
                        .HasForeignKey("ProjectId");
                });
#pragma warning restore 612, 618
        }
    }
}