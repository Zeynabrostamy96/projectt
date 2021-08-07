using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TapLearn.Layer.Migrations
{
    public partial class mignotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseComment");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Commentid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Courseid = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: false),
                    Commant = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsAdminRead = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Courseid = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsAdminRead = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_notes_curses_Courseid",
                        column: x => x.Courseid,
                        principalTable: "curses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_notes_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "Userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Courseid",
                table: "Comment",
                column: "Courseid");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_userid",
                table: "Comment",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_notes_Courseid",
                table: "notes",
                column: "Courseid");

            migrationBuilder.CreateIndex(
                name: "IX_notes_userid",
                table: "notes",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.CreateTable(
                name: "CourseComment",
                columns: table => new
                {
                    Commentid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: true),
                    Courseid = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdminRead = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseComment", x => x.Commentid);
                    table.ForeignKey(
                        name: "FK_CourseComment_curses_Courseid",
                        column: x => x.Courseid,
                        principalTable: "curses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseComment_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "Userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseComment_Courseid",
                table: "CourseComment",
                column: "Courseid");

            migrationBuilder.CreateIndex(
                name: "IX_CourseComment_userid",
                table: "CourseComment",
                column: "userid");
        }
    }
}
