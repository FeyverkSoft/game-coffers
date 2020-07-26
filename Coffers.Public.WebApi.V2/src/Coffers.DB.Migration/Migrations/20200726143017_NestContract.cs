using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffers.DB.Migrations.Migrations
{
    public partial class NestContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reward",
                table: "NestContract",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "NestContract",
                maxLength: 16,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reward",
                table: "NestContract");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NestContract");
        }
    }
}
