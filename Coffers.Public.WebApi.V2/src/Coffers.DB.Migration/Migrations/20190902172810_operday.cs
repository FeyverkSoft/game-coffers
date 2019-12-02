using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffers.DB.Migrations.Migrations
{
    public partial class operday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperDay",
                columns: table => new
                {
                    GuildId = table.Column<byte[]>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    Tax = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    UsersBalance = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    LoansBalance = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    UserCount = table.Column<int>(nullable: false, defaultValue: 0),
                    PenaltyAmount = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperDay", x => new { x.GuildId, x.Date });
                    table.ForeignKey(
                        name: "FK_OperDay_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperDay_GuildId",
                table: "OperDay",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_OperDay_GuildId_Date",
                table: "OperDay",
                columns: new[] { "GuildId", "Date" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperDay");
        }
    }
}
