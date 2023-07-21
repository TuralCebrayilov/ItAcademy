using Microsoft.EntityFrameworkCore.Migrations;

namespace ItAcademy.Migrations
{
    public partial class AddColumTeachersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Positions_PositionsId",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "PositionsId",
                table: "Teachers",
                newName: "CoursesId");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_PositionsId",
                table: "Teachers",
                newName: "IX_Teachers_CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Courses_CoursesId",
                table: "Teachers",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Courses_CoursesId",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "CoursesId",
                table: "Teachers",
                newName: "PositionsId");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_CoursesId",
                table: "Teachers",
                newName: "IX_Teachers_PositionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Positions_PositionsId",
                table: "Teachers",
                column: "PositionsId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
