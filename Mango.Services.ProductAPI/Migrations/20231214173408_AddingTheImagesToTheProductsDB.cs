using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddingTheImagesToTheProductsDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://th.bing.com/th/id/R.4e7f53fcc6f1412363c9aa683dd1426c?rik=1KrgIdIJqrLnhQ&pid=ImgRaw&r=0&sres=1&sresct=1", "Sambosa" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/R.2d6497c8af8a41b39d52391c605141dc?rik=PZXUnfSWfAhYUg&pid=ImgRaw&r=0");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/OIP.U2VOKNY3f8Os9ISq6PoG8gHaHU?w=628&h=621&rs=1&pid=ImgDetMain");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/R.7d1656d63d1f2cd751cd8cb145cbbcc2?rik=L2PKxpb9MrlXzQ&pid=ImgRaw&r=0");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 5, "Main Dish", "Roast Duck has tender and juicy meat, crispy skin, and it's glazed with the honey-balsamic glaze to give the duck a beautiful roasted look.", "https://th.bing.com/th/id/OIP.Qbkxc5tV-o4BXXgu05QThgHaEL?rs=1&pid=ImgDetMain", "Roasted Duck", 20.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://placehold.co/603x403", "Samosa" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://placehold.co/602x402");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://placehold.co/601x401");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://placehold.co/600x400");
        }
    }
}
