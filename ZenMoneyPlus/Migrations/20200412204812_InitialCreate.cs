using Microsoft.EntityFrameworkCore.Migrations;

namespace ZenMoneyPlus.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    User = table.Column<long>(nullable: false),
                    Changed = table.Column<long>(nullable: false),
                    Income = table.Column<decimal>(nullable: false),
                    Outcome = table.Column<decimal>(nullable: false),
                    IncomeInstrument = table.Column<long>(nullable: false),
                    OutcomeInstrument = table.Column<long>(nullable: false),
                    Created = table.Column<long>(nullable: false),
                    OriginalPayee = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Viewed = table.Column<bool>(nullable: false),
                    Hold = table.Column<string>(nullable: true),
                    QrCode = table.Column<string>(nullable: true),
                    IncomeAccount = table.Column<string>(nullable: true),
                    OutcomeAccount = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Payee = table.Column<string>(nullable: true),
                    OpIncome = table.Column<string>(nullable: true),
                    OpOutcome = table.Column<string>(nullable: true),
                    OpIncomeInstrument = table.Column<string>(nullable: true),
                    OpOutcomeInstrument = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Merchant = table.Column<string>(nullable: true),
                    IncomeBankId = table.Column<string>(nullable: true),
                    OutcomeBankId = table.Column<string>(nullable: true),
                    ReminderMarker = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 100, nullable: false),
                    User = table.Column<long>(nullable: false),
                    Changed = table.Column<long>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    BudgetIncome = table.Column<bool>(nullable: false),
                    BudgetOutcome = table.Column<bool>(nullable: false),
                    Required = table.Column<bool>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    ShowIncome = table.Column<bool>(nullable: false),
                    ShowOutcome = table.Column<bool>(nullable: false),
                    Parent = table.Column<string>(nullable: true),
                    TransactionId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Tags_Parent",
                        column: x => x.Parent,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tags_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTags",
                columns: table => new
                {
                    TagId = table.Column<string>(maxLength: 100, nullable: false),
                    TransactionId = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTags", x => new { x.TagId, x.TransactionId });
                    table.ForeignKey(
                        name: "FK_TransactionTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionTags_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Parent",
                table: "Tags",
                column: "Parent");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TransactionId",
                table: "Tags",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTags_TransactionId",
                table: "TransactionTags",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "TransactionTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
