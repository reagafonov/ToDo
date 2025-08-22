using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class indeces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_Name_OwnerUserId",
                table: "UserTasks",
                columns: new[] { "Name", "OwnerUserId" },
                unique: true,
                filter: "\"IsDeleted\" = false AND \"IsCompleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_UserTaskLists_Name_OwnerUserId",
                table: "UserTaskLists",
                columns: new[] { "Name", "OwnerUserId" },
                unique: true,
                filter: "\"IsDeleted\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserTasks_Name_OwnerUserId",
                table: "UserTasks");

            migrationBuilder.DropIndex(
                name: "IX_UserTaskLists_Name_OwnerUserId",
                table: "UserTaskLists");
        }
    }
}
