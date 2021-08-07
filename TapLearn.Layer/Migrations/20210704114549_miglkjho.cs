using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TapLearn.Layer.Migrations
{
    public partial class miglkjho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_curses_Courseid",
                table: "notes");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_userid",
                table: "notes");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_notes_Courseid",
                table: "notes");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "notes",
                newName: "Userid");

            migrationBuilder.RenameColumn(
                name: "Courseid",
                table: "notes",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_notes_userid",
                table: "notes",
                newName: "IX_notes_Userid");

            migrationBuilder.AddColumn<int>(
                name: "curseCourseId",
                table: "notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_notes_curseCourseId",
                table: "notes",
                column: "curseCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_curses_curseCourseId",
                table: "notes",
                column: "curseCourseId",
                principalTable: "curses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_notes_users_Userid",
                table: "notes",
                column: "Userid",
                principalTable: "users",
                principalColumn: "Userid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notes_curses_curseCourseId",
                table: "notes");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_Userid",
                table: "notes");

            migrationBuilder.DropIndex(
                name: "IX_notes_curseCourseId",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "curseCourseId",
                table: "notes");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "notes",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "notes",
                newName: "Courseid");

            migrationBuilder.RenameIndex(
                name: "IX_notes_Userid",
                table: "notes",
                newName: "IX_notes_userid");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Commentid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Commant = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: true),
                    Courseid = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdminRead = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Commentid);
                    table.ForeignKey(
                        name: "FK_Comment_curses_Courseid",
                        column: x => x.Courseid,
                        principalTable: "curses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comment_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "Userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notes_Courseid",
                table: "notes",
                column: "Courseid");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Courseid",
                table: "Comment",
                column: "Courseid");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_userid",
                table: "Comment",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_curses_Courseid",
                table: "notes",
                column: "Courseid",
                principalTable: "curses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_notes_users_userid",
                table: "notes",
                column: "userid",
                principalTable: "users",
                principalColumn: "Userid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
