using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coffers.DB.Migrations.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    ConcurrencyTokens = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tariff",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    LoanTax = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    ExpiredLoanTax = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    Tax = table.Column<string>(maxLength: 4096, nullable: false, defaultValue: "{}")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tariff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    OperationDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    AccountId = table.Column<byte[]>(nullable: true),
                    Type = table.Column<string>(maxLength: 32, nullable: false),
                    DocumentId = table.Column<byte[]>(nullable: true),
                    Description = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operation_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GuildTariff",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LeaderTariffId = table.Column<byte[]>(nullable: true),
                    OfficerTariffId = table.Column<byte[]>(nullable: true),
                    VeteranTariffId = table.Column<byte[]>(nullable: true),
                    SoldierTariffId = table.Column<byte[]>(nullable: true),
                    BeginnerTariffId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildTariff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuildTariff_Tariff_BeginnerTariffId",
                        column: x => x.BeginnerTariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuildTariff_Tariff_LeaderTariffId",
                        column: x => x.LeaderTariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuildTariff_Tariff_OfficerTariffId",
                        column: x => x.OfficerTariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuildTariff_Tariff_SoldierTariffId",
                        column: x => x.SoldierTariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GuildTariff_Tariff_VeteranTariffId",
                        column: x => x.VeteranTariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Guild",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    TariffId = table.Column<byte[]>(nullable: true),
                    GuildAccountId = table.Column<byte[]>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 512, nullable: false),
                    Status = table.Column<string>(maxLength: 32, nullable: false),
                    RecruitmentStatus = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guild", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guild_Account_GuildAccountId",
                        column: x => x.GuildAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guild_GuildTariff_TariffId",
                        column: x => x.TariffId,
                        principalTable: "GuildTariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gamer",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    GuildId = table.Column<byte[]>(nullable: true),
                    DefaultAccountId = table.Column<byte[]>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    Rank = table.Column<string>(maxLength: 32, nullable: false),
                    Status = table.Column<string>(maxLength: 32, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    Login = table.Column<string>(maxLength: 64, nullable: false),
                    Password = table.Column<string>(maxLength: 128, nullable: true),
                    Roles = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gamer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gamer_Account_DefaultAccountId",
                        column: x => x.DefaultAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gamer_Guild_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    ClassName = table.Column<string>(maxLength: 64, nullable: false),
                    Status = table.Column<string>(maxLength: 32, nullable: false),
                    GamerId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Gamer_GamerId",
                        column: x => x.GamerId,
                        principalTable: "Gamer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    GamerId = table.Column<byte[]>(nullable: true),
                    Action = table.Column<string>(maxLength: 1024, nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_History_Gamer_GamerId",
                        column: x => x.GamerId,
                        principalTable: "Gamer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    GamerId = table.Column<byte[]>(nullable: true),
                    TariffId = table.Column<byte[]>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    BorrowDate = table.Column<DateTime>(nullable: false),
                    ExpiredDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    RepaymentAmount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    TaxAmount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    PenaltyAmount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    LoanStatus = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loan_Gamer_GamerId",
                        column: x => x.GamerId,
                        principalTable: "Gamer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loan_Tariff_TariffId",
                        column: x => x.TariffId,
                        principalTable: "Tariff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Penalty",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    GamerId = table.Column<byte[]>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    RepaymentAmount = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    PenaltyStatus = table.Column<string>(maxLength: 32, nullable: false),
                    Description = table.Column<string>(maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalty_Gamer_GamerId",
                        column: x => x.GamerId,
                        principalTable: "Gamer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    SessionId = table.Column<byte[]>(nullable: false),
                    GamerId = table.Column<byte[]>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.SessionId);
                    table.ForeignKey(
                        name: "FK_Session_Gamer_GamerId",
                        column: x => x.GamerId,
                        principalTable: "Gamer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Id",
                table: "Account",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_GamerId",
                table: "Character",
                column: "GamerId");

            migrationBuilder.CreateIndex(
                name: "IX_Character_Id",
                table: "Character",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gamer_DefaultAccountId",
                table: "Gamer",
                column: "DefaultAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamer_GuildId",
                table: "Gamer",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamer_Id",
                table: "Gamer",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guild_GuildAccountId",
                table: "Guild",
                column: "GuildAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Guild_Id",
                table: "Guild",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guild_TariffId",
                table: "Guild",
                column: "TariffId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTariff_BeginnerTariffId",
                table: "GuildTariff",
                column: "BeginnerTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTariff_Id",
                table: "GuildTariff",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GuildTariff_LeaderTariffId",
                table: "GuildTariff",
                column: "LeaderTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTariff_OfficerTariffId",
                table: "GuildTariff",
                column: "OfficerTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTariff_SoldierTariffId",
                table: "GuildTariff",
                column: "SoldierTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildTariff_VeteranTariffId",
                table: "GuildTariff",
                column: "VeteranTariffId");

            migrationBuilder.CreateIndex(
                name: "IX_History_GamerId",
                table: "History",
                column: "GamerId");

            migrationBuilder.CreateIndex(
                name: "IX_History_Id",
                table: "History",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loan_GamerId",
                table: "Loan",
                column: "GamerId");

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
                name: "IX_Operation_AccountId",
                table: "Operation",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Operation_Id",
                table: "Operation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Penalty_GamerId",
                table: "Penalty",
                column: "GamerId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalty_Id",
                table: "Penalty",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Session_GamerId",
                table: "Session",
                column: "GamerId");

            migrationBuilder.CreateIndex(
                name: "IX_Session_SessionId",
                table: "Session",
                column: "SessionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tariff_Id",
                table: "Tariff",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "Penalty");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Gamer");

            migrationBuilder.DropTable(
                name: "Guild");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "GuildTariff");

            migrationBuilder.DropTable(
                name: "Tariff");
        }
    }
}
