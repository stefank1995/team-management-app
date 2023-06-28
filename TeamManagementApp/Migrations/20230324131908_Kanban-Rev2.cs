using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class KanbanRev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estimate",
                table: "KanbanData");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "KanbanData");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "KanbanData");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "KanbanData");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "KanbanData");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Estimate",
                table: "KanbanData",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "KanbanData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "KanbanData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "KanbanData",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "KanbanData",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
