using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangingTheSambossaImageLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/OIP.C30KE50Fs-PjhCCljL9q3QHaLH?rs=1&pid=ImgDetMain");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/R.4e7f53fcc6f1412363c9aa683dd1426c?rik=1KrgIdIJqrLnhQ&pid=ImgRaw&r=0&sres=1&sresct=1");
        }
    }
}
