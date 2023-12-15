using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class Creating_The_Products_Table2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://s3.amazonaws.com/grazecart/groveladderfarmllc/images/1566559006_5d5fcb1e8100e.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://th.bing.com/th/id/OIP.Qbkxc5tV-o4BXXgu05QThgHaEL?rs=1&pid=ImgDetMain");
        }
    }
}
