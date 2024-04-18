using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealsOrderingApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCustomerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "424e4556-b7d4-4101-be56-b2a6a9cdfcdf");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "4d41c8b4-5c86-45ab-8423-71a9d4bc634d");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ffd29e7-3385-4fca-807d-e404146c1eca", "8937dce7-dfdd-4308-9a97-5a40da530a39", "User", "USER" },
                    { "8d23caaa-cc97-4ffe-924c-1761849f6877", "9524bcc9-290a-4eed-8415-3e80382e3925", "Admin", "ADMIN" },
                    { "d83537d2-340e-41c4-90e8-19d5605a427c", "0eac1b6c-21ad-4541-8334-e2f12322cfba", "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6ffd29e7-3385-4fca-807d-e404146c1eca");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "8d23caaa-cc97-4ffe-924c-1761849f6877");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d83537d2-340e-41c4-90e8-19d5605a427c");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "424e4556-b7d4-4101-be56-b2a6a9cdfcdf", "b1ccccbc-3536-4973-bf5d-e1b1cbfab5f3", "User", "USER" },
                    { "4d41c8b4-5c86-45ab-8423-71a9d4bc634d", "54f8a043-d6e3-42c0-b3f3-08fb887b3fd6", "Admin", "ADMIN" }
                });
        }
    }
}
