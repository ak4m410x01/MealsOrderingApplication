using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealsOrderingApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "44d511e7-983a-4433-bc46-ec59dca5a9be");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "eadf9de8-1a69-4602-a814-b10e5f127eed");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f2e67fe0-0e12-453b-98b4-a074b98ad3f8");

            migrationBuilder.AddColumn<string>(
                name: "DriverId",
                schema: "Product",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Drivers",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drivers_Users_Id",
                        column: x => x.Id,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39d4e4fe-ca69-42af-8ebe-182cd643283f", "4a4677fe-1004-4b7d-a4f7-ec17dda70149", "User", "USER" },
                    { "6a304e86-b6b7-4bd3-9fc5-5eff9b8cd20c", "6fd12620-9be0-4f27-95f0-6c4de8150a77", "Admin", "ADMIN" },
                    { "bc870900-12a8-4f7d-b90d-d003d696ce80", "f52dab0e-84d3-4def-a496-a7f17528c33f", "Driver", "DRIVER" },
                    { "d976f103-a854-488f-83d6-e5f6915ded8f", "c644c244-af15-494e-b792-083a1d7d7279", "Customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DriverId",
                schema: "Product",
                table: "Orders",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Drivers_DriverId",
                schema: "Product",
                table: "Orders",
                column: "DriverId",
                principalSchema: "User",
                principalTable: "Drivers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Drivers_DriverId",
                schema: "Product",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Drivers",
                schema: "User");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DriverId",
                schema: "Product",
                table: "Orders");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "39d4e4fe-ca69-42af-8ebe-182cd643283f");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6a304e86-b6b7-4bd3-9fc5-5eff9b8cd20c");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "bc870900-12a8-4f7d-b90d-d003d696ce80");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "d976f103-a854-488f-83d6-e5f6915ded8f");

            migrationBuilder.DropColumn(
                name: "DriverId",
                schema: "Product",
                table: "Orders");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44d511e7-983a-4433-bc46-ec59dca5a9be", "ffe9075f-8462-4360-82b6-edeac334234f", "Customer", "CUSTOMER" },
                    { "eadf9de8-1a69-4602-a814-b10e5f127eed", "46d1e609-b704-4b47-ad08-790ce6146dbc", "User", "USER" },
                    { "f2e67fe0-0e12-453b-98b4-a074b98ad3f8", "6c05109b-688d-4ae8-9207-7c1ba8cd0edb", "Admin", "ADMIN" }
                });
        }
    }
}
