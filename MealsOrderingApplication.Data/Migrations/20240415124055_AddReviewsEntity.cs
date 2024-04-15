using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MealsOrderingApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "3567b8ff-18a5-4139-a56c-649627856813");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ec1b6af8-9ed8-455a-9a85-018ded3766f4");

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stars = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "User",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "80dbbd08-cf63-490a-b5a7-2e755cf38c7a", "359347e3-69e7-4bfe-bac2-f55352a27056", "Admin", "ADMIN" },
                    { "87f9fd03-cd43-4728-b4ee-aa861269f98e", "68d2a71b-a980-47e7-b6df-6b904210080b", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId",
                schema: "Product",
                table: "Reviews",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                schema: "Product",
                table: "Reviews",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "Product");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "80dbbd08-cf63-490a-b5a7-2e755cf38c7a");

            migrationBuilder.DeleteData(
                schema: "Security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "87f9fd03-cd43-4728-b4ee-aa861269f98e");

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3567b8ff-18a5-4139-a56c-649627856813", "3f6c9261-5134-4028-8294-ee4946a81327", "Admin", "ADMIN" },
                    { "ec1b6af8-9ed8-455a-9a85-018ded3766f4", "34ebd885-9579-4628-ae9d-a6e2fb6d34e4", "User", "USER" }
                });
        }
    }
}
