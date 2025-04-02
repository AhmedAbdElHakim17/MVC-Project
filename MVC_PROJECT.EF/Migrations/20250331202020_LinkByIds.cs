using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class LinkByIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "instructors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_UserId",
                table: "students",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_instructors_UserId",
                table: "instructors",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_instructors_AspNetUsers_UserId",
                table: "instructors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_students_AspNetUsers_UserId",
                table: "students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_instructors_AspNetUsers_UserId",
                table: "instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_students_AspNetUsers_UserId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_students_UserId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_instructors_UserId",
                table: "instructors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "instructors");
        }
    }
}
