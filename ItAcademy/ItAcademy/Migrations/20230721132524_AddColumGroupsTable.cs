using Microsoft.EntityFrameworkCore.Migrations;

namespace ItAcademy.Migrations
{
    public partial class AddColumGroupsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Groups");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "Groups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "Groups");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
