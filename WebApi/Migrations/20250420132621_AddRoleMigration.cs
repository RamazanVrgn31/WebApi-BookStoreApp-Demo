using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c6c1e40-b43d-4e0b-bae1-cb84ce745553", null, "Admin", "ADMİN" },
                    { "82e1588f-3150-4da4-b1b7-6add679a4b1e", null, "Editor", "EDITOR" },
                    { "861f51d3-a4e4-40c9-982e-eca4d00c5c5a", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c6c1e40-b43d-4e0b-bae1-cb84ce745553");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82e1588f-3150-4da4-b1b7-6add679a4b1e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "861f51d3-a4e4-40c9-982e-eca4d00c5c5a");
        }
    }
}
