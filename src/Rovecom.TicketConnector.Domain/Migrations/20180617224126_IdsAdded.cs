using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Rovecom.TicketConnector.Domain.Migrations
{
    public partial class IdsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Accounts_AccountCode",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Worklog_Projects_ProjectCode",
                table: "Worklog");

            migrationBuilder.DropIndex(
                name: "IX_Worklog_ProjectCode",
                table: "Worklog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AccountCode",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectCode",
                table: "Worklog",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Worklog",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "Worklog",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountCode",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Projects",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Projects",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Employees",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Accounts",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Accounts",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Worklog_EmployeeId",
                table: "Worklog",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Worklog_ProjectId",
                table: "Worklog",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AccountId",
                table: "Projects",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Accounts_AccountId",
                table: "Projects",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Worklog_Employees_EmployeeId",
                table: "Worklog",
                column: "EmployeeId",
                principalTable: "Employees",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Accounts_AccountId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Worklog_Employees_EmployeeId",
                table: "Worklog");

            migrationBuilder.DropForeignKey(
                name: "FK_Worklog_Projects_ProjectId",
                table: "Worklog");

            migrationBuilder.DropIndex(
                name: "IX_Worklog_EmployeeId",
                table: "Worklog");

            migrationBuilder.DropIndex(
                name: "IX_Worklog_ProjectId",
                table: "Worklog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AccountId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Worklog");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Worklog");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectCode",
                table: "Worklog",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountCode",
                table: "Projects",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "EmailAddress");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Code");

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
    }
}
