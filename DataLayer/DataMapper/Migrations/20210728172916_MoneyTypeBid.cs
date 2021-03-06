using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace DataMapper.Migrations
{
    public partial class MoneyTypeBid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BidValue_Amount",
                table: "Bids",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BidValue_Currency",
                table: "Bids",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidValue_Amount",
                table: "Bids");

            migrationBuilder.DropColumn(
                name: "BidValue_Currency",
                table: "Bids");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
