using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TapLearn.Layer.Migrations
{
    public partial class InitialIgnoreChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseComment");

            migrationBuilder.DropTable(
                name: "courseEpisods");

            migrationBuilder.DropTable(
                name: "detail");

            migrationBuilder.DropTable(
                name: "rolePermissions");

            migrationBuilder.DropTable(
                name: "useCourses");

            migrationBuilder.DropTable(
                name: "userDiscountCodes");

            migrationBuilder.DropTable(
                name: "userRoles");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "curses");

            migrationBuilder.DropTable(
                name: "disCounts");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "walletTypes");

            migrationBuilder.DropTable(
                name: "courseGroups");

            migrationBuilder.DropTable(
                name: "courseLevels");

            migrationBuilder.DropTable(
                name: "courseStatuses");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
