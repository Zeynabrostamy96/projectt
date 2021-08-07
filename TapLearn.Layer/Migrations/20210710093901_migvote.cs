using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TapLearn.Layer.Migrations
{
    public partial class migvote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courseVoltes",
                columns: table => new
                {
                    VoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    courseid = table.Column<int>(type: "int", nullable: false),
                    Volte = table.Column<bool>(type: "bit", nullable: false),
                    VoteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courseVoltes", x => x.VoteId);
                    table.ForeignKey(
                        name: "FK_courseVoltes_curses_courseid",
                        column: x => x.courseid,
                        principalTable: "curses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_courseVoltes_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "Userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_courseVoltes_courseid",
                table: "courseVoltes",
                column: "courseid");

            migrationBuilder.CreateIndex(
                name: "IX_courseVoltes_userId",
                table: "courseVoltes",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courseVoltes");
        }
    }
}
