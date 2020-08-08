using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffers.DB.Migrations.Migrations
{
    public partial class ExpDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ddc0a1d7-d1c8-4f36-996f-91005fafe11d"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpDate",
                table: "NestContract",
                nullable: true);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ConcurrencyTokens", "CreateDate", "DateOfBirth", "DeletedDate", "Email", "GuildId", "Login", "Name", "Password", "Rank", "Roles", "Status", "UpdateDate" },
                values: new object[] { new Guid("08ec6266-0a51-45ad-a7f0-eb5498434f6d"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc), new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc), null, null, new Guid("00000000-0000-4000-0000-000000000001"), "Admin", "Admin", null, "Leader", "[\"admin\"]", "Active", new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("08ec6266-0a51-45ad-a7f0-eb5498434f6d"));

            migrationBuilder.DropColumn(
                name: "ExpDate",
                table: "NestContract");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ConcurrencyTokens", "CreateDate", "DateOfBirth", "DeletedDate", "Email", "GuildId", "Login", "Name", "Password", "Rank", "Roles", "Status", "UpdateDate" },
                values: new object[] { new Guid("ddc0a1d7-d1c8-4f36-996f-91005fafe11d"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc), new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc), null, null, new Guid("00000000-0000-4000-0000-000000000001"), "Admin", "Admin", null, "Leader", "[\"admin\"]", "Active", new DateTime(2020, 1, 30, 7, 35, 9, 0, DateTimeKind.Utc) });
        }
    }
}
