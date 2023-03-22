using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class IdentityAdded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40b6dd23-e13f-4267-9c1e-ed6527744117");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "913416b1-f19f-4298-9b9f-3cf37c7b6fbe");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8cefeea0-7d42-43af-91d8-f9ce58008866", null, "Admin", "ADMIN" },
                    { "b2feb726-d610-4a39-a0fb-32c3e33bd806", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8cefeea0-7d42-43af-91d8-f9ce58008866");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2feb726-d610-4a39-a0fb-32c3e33bd806");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40b6dd23-e13f-4267-9c1e-ed6527744117", null, "Admin", "ADMIN" },
                    { "913416b1-f19f-4298-9b9f-3cf37c7b6fbe", null, "Member", "MEMBER" }
                });
        }
    }
}
