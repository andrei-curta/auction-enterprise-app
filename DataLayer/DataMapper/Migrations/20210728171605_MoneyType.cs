using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace DataMapper.Migrations
{
    public partial class MoneyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Value_Amount",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value_Currency",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value_Amount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Value_Currency",
                table: "Products");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
