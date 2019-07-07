using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffers.DB.Migrations.Migrations
{
    public partial class Session : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Gamer",
                maxLength: 512,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    SessionId = table.Column<byte[]>(nullable: false),
                    GamerId = table.Column<byte[]>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.SessionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Session_SessionId",
                table: "Session",
                column: "SessionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Gamer");
        }
    }
}
