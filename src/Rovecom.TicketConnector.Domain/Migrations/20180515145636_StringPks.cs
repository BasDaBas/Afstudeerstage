using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rovecom.TicketConnector.Domain.Migrations
{
    public partial class StringPks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Worklog_MspTechnician_MspTechnicianId",
                table: "Worklog");

            migrationBuilder.DropForeignKey(
                name: "FK_Worklog_Projects_ProjectId",
                table: "Worklog");

            migrationBuilder.DropTable(
                name: "MspTechnician");

            migrationBuilder.DropIndex(
                name: "IX_Worklog_MspTechnicianId",
                table: "Worklog");

            migrationBuilder.DropIndex(
                name: "IX_Worklog_ProjectId",
                table: "Worklog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "MspTechnicianId",
                table: "Worklog");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Worklog");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeEmailAddress",
                table: "Worklog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "Worklog",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountCode",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Code");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    FaxNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    WebsiteUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmailAddress = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmailAddress);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Worklog_ProjectCode",
                table: "Worklog",
                column: "ProjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AccountCode",
                table: "Projects",
                column: "AccountCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Accounts_AccountCode",
                table: "Projects",
                column: "AccountCode",
                principalTable: "Accounts",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Worklog_Projects_ProjectCode",
                table: "Worklog",
                column: "ProjectCode",
                principalTable: "Projects",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Accounts_AccountCode",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Worklog_Projects_ProjectCode",
                table: "Worklog");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Worklog_ProjectCode",
                table: "Worklog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AccountCode",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EmployeeEmailAddress",
                table: "Worklog");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "Worklog");

            migrationBuilder.DropColumn(
                name: "AccountCode",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "MspTechnicianId",
                table: "Worklog",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Worklog",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Projects",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Worklog_MspTechnicianId",
                table: "Worklog",
                column: "MspTechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Worklog_ProjectId",
                table: "Worklog",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Worklog_MspTechnician_MspTechnicianId",
                table: "Worklog",
                column: "MspTechnicianId",
                principalTable: "MspTechnician",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Worklog_Projects_ProjectId",
                table: "Worklog",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}