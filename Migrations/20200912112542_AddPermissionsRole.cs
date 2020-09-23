using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HelperDesk.API.Migrations
{
    public partial class AddPermissionsRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "add",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "delete",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "edit",
                table: "Roles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Document_PositionId",
                table: "Document",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_DepartmentId",
                table: "Position",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Position_UserReportedId",
                table: "Position",
                column: "UserReportedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "add",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "delete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "edit",
                table: "Roles");
        }
    }
}
