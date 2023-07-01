using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class KanbanRev3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssigneeId",
                table: "KanbanData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "KanbanData");
        }
    }
}
