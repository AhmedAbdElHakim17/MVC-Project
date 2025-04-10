﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class nullableInsId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_instructors_InsID",
                table: "courses");

            migrationBuilder.AlterColumn<int>(
                name: "InsID",
                table: "courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_instructors_InsID",
                table: "courses",
                column: "InsID",
                principalTable: "instructors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_instructors_InsID",
                table: "courses");

            migrationBuilder.AlterColumn<int>(
                name: "InsID",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_instructors_InsID",
                table: "courses",
                column: "InsID",
                principalTable: "instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
