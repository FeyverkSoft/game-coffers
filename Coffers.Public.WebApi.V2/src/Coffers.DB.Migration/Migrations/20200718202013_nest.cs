using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffers.DB.Migrations.Migrations
{
    public partial class nest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b60e10fe-6fc1-4d42-86ab-6be978f29527"));

            migrationBuilder.CreateTable(
                name: "Nest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GuildId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 512, nullable: false),
                    IsHidden = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nest_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NestContract",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    NestId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NestContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NestContract_Nest_NestId",
                        column: x => x.NestId,
                        principalTable: "Nest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NestContract_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nest_GuildId",
                table: "Nest",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Nest_Id",
                table: "Nest",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NestContract_Id",
                table: "NestContract",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NestContract_NestId",
                table: "NestContract",
                column: "NestId");

            migrationBuilder.CreateIndex(
                name: "IX_NestContract_UserId",
                table: "NestContract",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NestContract");

            migrationBuilder.DropTable(
                name: "Nest");
        }
    }
}
