using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class Kanbanrev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedBy",
                table: "KanbanData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedById",
                table: "KanbanData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "KanbanData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedBy",
                table: "KanbanData");

            migrationBuilder.DropColumn(
                name: "AssignedById",
                table: "KanbanData");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "KanbanData");
        }
    }
}
