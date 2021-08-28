using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace DataMapper.Migrations
{
    public partial class bidUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Bids",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Bids_UserId",
                table: "Bids",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_UserId",
                table: "Bids",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Users_UserId",
                table: "Bids");

            migrationBuilder.DropIndex(
                name: "IX_Bids_UserId",
                table: "Bids");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Bids",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
