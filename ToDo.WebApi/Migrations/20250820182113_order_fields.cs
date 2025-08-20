using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class order_fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompleteDate",
                table: "UserTasks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "UserTasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "UserTasks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderDirection",
                table: "UserTaskLists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderType",
                table: "UserTaskLists",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteDate",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "OrderDirection",
                table: "UserTaskLists");

            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "UserTaskLists");
        }
    }
}
