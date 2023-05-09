using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.IdentityExample.Migrations.SchoolDb
{
    /// <inheritdoc />
    public partial class SchoolDbContextUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClassesId1",
                table: "IdentityUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LasTime",
                table: "homeworks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "homeworks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "classes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsLockout",
                table: "classes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateOn",
                table: "classes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_ClassesId1",
                table: "IdentityUser",
                column: "ClassesId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUser_classes_ClassesId1",
                table: "IdentityUser",
                column: "ClassesId1",
                principalTable: "classes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUser_classes_ClassesId1",
                table: "IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUser_ClassesId1",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "ClassesId1",
                table: "IdentityUser");

            migrationBuilder.DropColumn(
                name: "LasTime",
                table: "homeworks");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "homeworks");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "classes");

            migrationBuilder.DropColumn(
                name: "IsLockout",
                table: "classes");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "classes");

            migrationBuilder.DropColumn(
                name: "UpdateOn",
                table: "classes");
        }
    }
}
