using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace DataMapper.Migrations
{
    public partial class AuctionStartPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Products_ProductId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "Value_Amount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Value_Currency",
                table: "Products");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Auctions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StartPrice_Amount",
                table: "Auctions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartPrice_Currency",
                table: "Auctions",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Products_ProductId",
                table: "Auctions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Products_ProductId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "StartPrice_Amount",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "StartPrice_Currency",
                table: "Auctions");

            migrationBuilder.AddColumn<decimal>(
                name: "Value_Amount",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value_Currency",
                table: "Products",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                table: "Auctions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Products_ProductId",
                table: "Auctions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
