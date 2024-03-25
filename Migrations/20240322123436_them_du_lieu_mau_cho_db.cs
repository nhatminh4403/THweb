using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanHang_Lab3.Migrations
{
    /// <inheritdoc />
    public partial class them_du_lieu_mau_cho_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "Categories",
               columns: ["Id", "Name"],
               values: [1, "Điện thoại"]);
            migrationBuilder.InsertData(
               table: "Categories",
               columns: ["Id", "Name"],
               values: [2, "Laptop"]);
            migrationBuilder.InsertData(
               table: "Products",
               columns: ["Id", "Name", "Price", "Description", "ImageUrl", "CategoryId"],
               values: [1, "Galaxy S24 Ultra",10000,"Xịn",
                   "https://cdn.tgdd.vn/Products/Images/42/307174/samsung-galaxy-s24-ultra-xam-1.jpg",1]);
            migrationBuilder.InsertData(
               table: "Products",
               columns: ["Id", "Name", "Price", "Description", "ImageUrl", "CategoryId"],
               values: [2, "Asus ROG Mothership", 10000, "Xịn",
                   "https://gstatic.gvn360.com/2019/12/A_02-1.jpg", 2]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
