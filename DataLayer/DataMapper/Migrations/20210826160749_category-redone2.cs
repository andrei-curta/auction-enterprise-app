using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace DataMapper.Migrations
{
    public partial class categoryredone2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "CategoryCategory",
                columns: table => new
                {
                    ParentCategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    SubCategoriesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCategory", x => new { x.ParentCategoriesId, x.SubCategoriesId });
                    table.ForeignKey(
                        name: "FK_CategoryCategory_Categories_ParentCategoriesId",
                        column: x => x.ParentCategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCategory_Categories_SubCategoriesId",
                        column: x => x.SubCategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCategory_SubCategoriesId",
                table: "CategoryCategory",
                column: "SubCategoriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryCategory");

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Categories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
