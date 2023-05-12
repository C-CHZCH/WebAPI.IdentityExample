using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.IdentityExample.Migrations
{
    /// <inheritdoc />
    public partial class Fix20230510 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "AspNetUsers",
                newName: "ClassCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassCode",
                table: "AspNetUsers",
                newName: "ClassId");
        }
    }
}
