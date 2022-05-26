#pragma warning disable CS1591
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZenMoneyPlus.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Instrument = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: true),
                    Private = table.Column<bool>(type: "INTEGER", nullable: false),
                    Savings = table.Column<bool>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    InBalance = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "TEXT", nullable: false),
                    StartBalance = table.Column<decimal>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    Company = table.Column<string>(type: "TEXT", nullable: true),
                    Archive = table.Column<bool>(type: "INTEGER", nullable: false),
                    EnableCorrection = table.Column<bool>(type: "INTEGER", nullable: false),
                    BalanceCorrectionType = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<string>(type: "TEXT", nullable: true),
                    Capitalization = table.Column<bool>(type: "INTEGER", nullable: true),
                    Percent = table.Column<decimal>(type: "TEXT", nullable: true),
                    SyncId = table.Column<string>(type: "TEXT", nullable: true),
                    EnableSms = table.Column<bool>(type: "INTEGER", nullable: false),
                    EndDateOffset = table.Column<int>(type: "INTEGER", nullable: true),
                    EndDateOffsetInterval = table.Column<string>(type: "TEXT", nullable: true),
                    PayoffStep = table.Column<int>(type: "INTEGER", nullable: true),
                    PayoffInterval = table.Column<string>(type: "TEXT", nullable: true),
                    User = table.Column<long>(type: "INTEGER", nullable: false),
                    Changed = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: true),
                    BudgetIncome = table.Column<bool>(type: "INTEGER", nullable: false),
                    BudgetOutcome = table.Column<bool>(type: "INTEGER", nullable: false),
                    Required = table.Column<bool>(type: "INTEGER", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    Picture = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    ShowIncome = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShowOutcome = table.Column<bool>(type: "INTEGER", nullable: false),
                    Parent = table.Column<string>(type: "TEXT", nullable: true),
                    User = table.Column<long>(type: "INTEGER", nullable: false),
                    Changed = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Tags_Parent",
                        column: x => x.Parent,
                        principalTable: "Tags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Income = table.Column<decimal>(type: "TEXT", nullable: false),
                    Outcome = table.Column<decimal>(type: "TEXT", nullable: false),
                    IncomeInstrument = table.Column<long>(type: "INTEGER", nullable: false),
                    OutcomeInstrument = table.Column<long>(type: "INTEGER", nullable: false),
                    Created = table.Column<string>(type: "TEXT", nullable: false),
                    OriginalPayee = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Viewed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Hold = table.Column<bool>(type: "INTEGER", nullable: true),
                    QrCode = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    Payee = table.Column<string>(type: "TEXT", nullable: true),
                    OpIncome = table.Column<decimal>(type: "TEXT", nullable: true),
                    OpOutcome = table.Column<decimal>(type: "TEXT", nullable: true),
                    OpIncomeInstrument = table.Column<string>(type: "TEXT", nullable: true),
                    OpOutcomeInstrument = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: true),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: true),
                    Merchant = table.Column<string>(type: "TEXT", nullable: true),
                    IncomeBankId = table.Column<string>(type: "TEXT", nullable: true),
                    OutcomeBankId = table.Column<string>(type: "TEXT", nullable: true),
                    ReminderMarker = table.Column<string>(type: "TEXT", nullable: true),
                    GetReceiptFailed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IncomeAccountId = table.Column<string>(type: "TEXT", nullable: true),
                    OutcomeAccountId = table.Column<string>(type: "TEXT", nullable: true),
                    User = table.Column<long>(type: "INTEGER", nullable: false),
                    Changed = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_IncomeAccountId",
                        column: x => x.IncomeAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_OutcomeAccountId",
                        column: x => x.OutcomeAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sum = table.Column<decimal>(type: "TEXT", nullable: true),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<string>(type: "TEXT", nullable: false),
                    Payee = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    CardSum = table.Column<decimal>(type: "TEXT", nullable: true),
                    CashSum = table.Column<decimal>(type: "TEXT", nullable: true),
                    Inn = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagTransaction",
                columns: table => new
                {
                    TagsId = table.Column<string>(type: "TEXT", nullable: false),
                    TransactionsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTransaction", x => new { x.TagsId, x.TransactionsId });
                    table.ForeignKey(
                        name: "FK_TagTransaction_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTransaction_Transactions_TransactionsId",
                        column: x => x.TransactionsId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Sum = table.Column<decimal>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: true),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: true),
                    ReceiptId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptItems_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItems_ReceiptId",
                table: "ReceiptItems",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_TransactionId",
                table: "Receipts",
                column: "TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Parent",
                table: "Tags",
                column: "Parent");

            migrationBuilder.CreateIndex(
                name: "IX_TagTransaction_TransactionsId",
                table: "TagTransaction",
                column: "TransactionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IncomeAccountId",
                table: "Transactions",
                column: "IncomeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OutcomeAccountId",
                table: "Transactions",
                column: "OutcomeAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptItems");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "TagTransaction");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
