using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GShop.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into Products(Id,Name,Price,Description,Stock,ImageURL,CategoryId) Values(UUID(),'Caderno',7.55,'Caderno Espiral',10,'caderno1.jpg','d78ade09-7a55-11ee-81d8-0242ac110002')");

            migrationBuilder.Sql("Insert Into Products(Id,Name,Price,Description,Stock,ImageURL,CategoryId) Values(UUID(),'Lápis',3.45,'Lápis Preto',20,'lapis1.jpg','d78ade09-7a55-11ee-81d8-0242ac110002')");

            migrationBuilder.Sql("Insert Into Products(Id,Name,Price,Description,Stock,ImageURL,CategoryId) Values(UUID(),'Clips',5.33,'Clips para papel',50,'clips1.jpg','d824837e-7a55-11ee-81d8-0242ac110002')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Products");
        }
    }
}
