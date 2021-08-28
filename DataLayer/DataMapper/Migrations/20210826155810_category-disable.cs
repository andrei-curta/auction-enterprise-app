using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace DataMapper.Migrations
{
    public partial class categorydisable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId1",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentId1",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentId1",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId1",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    ProductsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId1",
                table: "Categories",
                column: "ParentId1");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                table: "CategoryProduct",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId1",
                table: "Categories",
                column: "ParentId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
