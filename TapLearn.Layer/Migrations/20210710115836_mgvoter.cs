using Microsoft.EntityFrameworkCore.Migrations;

namespace TapLearn.Layer.Migrations
{
    public partial class mgvoter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseVoltes_curses_courseid",
                table: "courseVoltes");

            migrationBuilder.RenameColumn(
                name: "courseid",
                table: "courseVoltes",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_courseVoltes_courseid",
                table: "courseVoltes",
                newName: "IX_courseVoltes_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_courseVoltes_curses_CourseId",
                table: "courseVoltes",
                column: "CourseId",
                principalTable: "curses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courseVoltes_curses_CourseId",
                table: "courseVoltes");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "courseVoltes",
                newName: "courseid");

            migrationBuilder.RenameIndex(
                name: "IX_courseVoltes_CourseId",
                table: "courseVoltes",
                newName: "IX_courseVoltes_courseid");

            migrationBuilder.AddForeignKey(
                name: "FK_courseVoltes_curses_courseid",
                table: "courseVoltes",
                column: "courseid",
                principalTable: "curses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
