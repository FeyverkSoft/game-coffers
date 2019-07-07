using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffers.DB.Migrations.Migrations
{
    public partial class MoveCreateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Tariff");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "GuildTariff",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "GuildTariff");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Tariff",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
