using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace DataMapper.Migrations
{
    public partial class MoneyTypeConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value_Currency",
                table: "Products",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value_Currency",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
