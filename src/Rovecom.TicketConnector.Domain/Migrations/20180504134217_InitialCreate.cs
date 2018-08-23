using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Rovecom.TicketConnector.Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MspTechnician",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MspTechnician", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Worklog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    KilometresCovered = table.Column<double>(nullable: false),
                    MspTechnicianId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    WorkEndedDateTime = table.Column<DateTime>(nullable: false),
                    WorkStartedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worklog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Worklog_MspTechnician_MspTechnicianId",
                        column: x => x.MspTechnicianId,
                        principalTable: "MspTechnician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Worklog_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Worklog_MspTechnicianId",
                table: "Worklog",
                column: "MspTechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Worklog_ProjectId",
                table: "Worklog",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Worklog");

            migrationBuilder.DropTable(
                name: "MspTechnician");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}