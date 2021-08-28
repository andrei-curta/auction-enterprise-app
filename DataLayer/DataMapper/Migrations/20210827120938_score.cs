using System;
using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace DataMapper.Migrations
{
    public partial class score : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedToUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssignedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ScoreValue = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_Users_AssignedByUserId",
                        column: x => x.AssignedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scores_Users_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_AssignedByUserId",
                table: "Scores",
                column: "AssignedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_AssignedToUserId",
                table: "Scores",
                column: "AssignedToUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
