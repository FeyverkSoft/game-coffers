using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffers.DB.Migrations.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guild",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TariffId = table.Column<Guid>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 512, nullable: false),
                    Status = table.Column<string>(maxLength: 32, nullable: false),
                    RecruitmentStatus = table.Column<string>(maxLength: 32, nullable: false),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guild", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tariff",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LoanTax = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    ExpiredLoanTax = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    Tax = table.Column<string>(maxLength: 4096, nullable: false, defaultValue: "{}")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GuildId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Rank = table.Column<string>(maxLength: 32, nullable: false),
                    Status = table.Column<string>(maxLength: 32, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Login = table.Column<string>(maxLength: 64, nullable: false),
                    Password = table.Column<string>(maxLength: 128, nullable: true),
                    Roles = table.Column<string>(maxLength: 512, nullable: true),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<String>(nullable: false),
                    GuildId = table.Column<Guid>(nullable: false),
                    TariffId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.Id, x.GuildId });
                    table.ForeignKey(
                        name: "FK_UserRole_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    ClassName = table.Column<string>(maxLength: 64, nullable: false),
                    IsMain = table.Column<bool>(nullable: false, defaultValue: false),
                    Status = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    TariffId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    BorrowDate = table.Column<DateTime>(nullable: false),
                    ExpiredDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    TaxAmount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    PenaltyAmount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    LoanStatus = table.Column<string>(maxLength: 32, nullable: false),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loan_Tariff_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loan_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Penalty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PenaltyStatus = table.Column<string>(maxLength: 32, nullable: false),
                    Description = table.Column<string>(maxLength: 2048, nullable: true),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalty_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(maxLength: 128, nullable: true),
                    ConcurrencyTokens = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Session_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GuildId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ParentOperationId = table.Column<Guid>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    Type = table.Column<string>(maxLength: 32, nullable: false),
                    DocumentId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    LoanId = table.Column<Guid>(nullable: true),
                    PenaltyId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operation_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operation_Loan_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operation_Penalty_PenaltyId",
                        column: x => x.PenaltyId,
                        principalTable: "Penalty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operation_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Guild",
                columns: new[] { "Id", "ConcurrencyTokens", "CreateDate", "Name", "RecruitmentStatus", "Status", "TariffId", "UpdateDate" },
                values: new object[] { new Guid("00000000-0000-4000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 27, 7, 35, 9, 697, DateTimeKind.Utc).AddTicks(8813), "Admins", "Close", "Active", null, new DateTime(2019, 12, 27, 7, 35, 9, 697, DateTimeKind.Utc).AddTicks(9423) });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ConcurrencyTokens", "CreateDate", "DateOfBirth", "DeletedDate", "GuildId", "Login", "Name", "Password", "Rank", "Roles", "Status", "UpdateDate" },
                values: new object[] { new Guid("d9676ccc-1a40-4cd7-9fe6-cdc3666bbdc4"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2019, 12, 27, 7, 35, 9, 733, DateTimeKind.Utc).AddTicks(7871), new DateTime(2019, 12, 27, 7, 35, 9, 734, DateTimeKind.Utc).AddTicks(4209), null, new Guid("00000000-0000-4000-0000-000000000001"), "Admin", "Admin", null, "Leader", "[\"admin\"]", "Active", new DateTime(2019, 12, 27, 7, 35, 9, 734, DateTimeKind.Utc).AddTicks(1291) });

            migrationBuilder.CreateIndex(
                name: "IX_Character_Id",
                table: "Character",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_UserId",
                table: "Character",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Guild_Id",
                table: "Guild",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loan_Id",
                table: "Loan",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loan_TariffId",
                table: "Loan",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_UserId",
                table: "Loan",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_GuildId",
                table: "Operation",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_Id",
                table: "Operation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operation_LoanId",
                table: "Operation",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_PenaltyId",
                table: "Operation",
                column: "PenaltyId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_UserId",
                table: "Operation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalty_Id",
                table: "Penalty",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Penalty_UserId",
                table: "Penalty",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_SessionId",
                table: "Session",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Session_UserId",
                table: "Session",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tariff_Id",
                table: "Tariff",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_GuildId",
                table: "User",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Id",
                table: "User",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_GuildId",
                table: "UserRole",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Id_GuildId",
                table: "UserRole",
                columns: new[] { "Id", "GuildId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "Penalty");

            migrationBuilder.DropTable(
                name: "Tariff");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Guild");
        }
    }
}
