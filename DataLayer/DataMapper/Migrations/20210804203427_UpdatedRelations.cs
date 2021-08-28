using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace DataMapper.Migrations
{
    public partial class UpdatedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Products_ProductId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "ParentId1");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentId1");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Auctions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ClosedByOwner",
                table: "Auctions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Auctions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Auctions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId1",
                table: "Products",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_UserId1",
                table: "Auctions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Products_ProductId",
                table: "Auctions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_UserId1",
                table: "Auctions",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId1",
                table: "Categories",
                column: "ParentId1",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId1",
                table: "Products",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Products_ProductId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_UserId1",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId1",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_UserId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_UserId1",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "ParentId1",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentId1",
                table: "Categories",
                newName: "IX_Categories_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Auctions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<bool>(
                name: "ClosedByOwner",
                table: "Auctions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Products_ProductId",
                table: "Auctions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
