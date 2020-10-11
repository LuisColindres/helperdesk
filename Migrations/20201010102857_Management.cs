using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HelperDesk.API.Migrations
{
    public partial class Management : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Management",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentId = table.Column<int>(nullable: false),
                    UserCreatedId = table.Column<int>(nullable: false),
                    AssignedUserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    File = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')"),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Management", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Management_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Management_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Management_Users_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // migrationBuilder.CreateIndex(
            //     name: "IX_Users_PositionId",
            //     table: "Users",
            //     column: "PositionId",
            //     unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Management_AssignedUserId",
                table: "Management",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Management_DepartmentId",
                table: "Management",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Management_UserCreatedId",
                table: "Management",
                column: "UserCreatedId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Users_Position_PositionId",
            //     table: "Users",
            //     column: "PositionId",
            //     principalTable: "Position",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Users_Position_PositionId",
            //     table: "Users");

            migrationBuilder.DropTable(
                name: "Management");

            // migrationBuilder.DropIndex(
            //     name: "IX_Users_PositionId",
            //     table: "Users");

            // migrationBuilder.DropColumn(
            //     name: "PositionId",
            //     table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Position_UserReportedId",
                table: "Position",
                column: "UserReportedId");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Position_Users_UserReportedId",
            //     table: "Position",
            //     column: "UserReportedId",
            //     principalTable: "Users",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }
    }
}
