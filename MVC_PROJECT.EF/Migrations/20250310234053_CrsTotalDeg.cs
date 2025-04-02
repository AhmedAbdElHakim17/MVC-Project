using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class CrsTotalDeg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalDeg",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDeg",
                table: "courses");
        }
    }
}
