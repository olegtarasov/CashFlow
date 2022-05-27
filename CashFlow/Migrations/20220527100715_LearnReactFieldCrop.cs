#pragma warning disable CS1591
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashFlow.Migrations
{
    public partial class LearnReactFieldCrop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "GetReceiptFailed",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Hold",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IncomeBankId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IncomeInstrument",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Merchant",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OpIncome",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OpIncomeInstrument",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OpOutcome",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OpOutcomeInstrument",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OriginalPayee",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OutcomeBankId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OutcomeInstrument",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "QrCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ReminderMarker",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BudgetIncome",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "BudgetOutcome",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Required",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "Inn",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "Archive",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BalanceCorrectionType",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Capitalization",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CreditLimit",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EnableCorrection",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EnableSms",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EndDateOffset",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EndDateOffsetInterval",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Instrument",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PayoffInterval",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PayoffStep",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Percent",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Private",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Savings",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "StartBalance",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SyncId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GetReceiptFailed",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hold",
                table: "Transactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncomeBankId",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IncomeInstrument",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Merchant",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OpIncome",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpIncomeInstrument",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OpOutcome",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpOutcomeInstrument",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalPayee",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutcomeBankId",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OutcomeInstrument",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "QrCode",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReminderMarker",
                table: "Transactions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "User",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "BudgetIncome",
                table: "Tags",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BudgetOutcome",
                table: "Tags",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Tags",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Tags",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Tags",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "Tags",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "User",
                table: "Tags",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Receipts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inn",
                table: "Receipts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Archive",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BalanceCorrectionType",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Capitalization",
                table: "Accounts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditLimit",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "EnableCorrection",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableSms",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EndDateOffset",
                table: "Accounts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndDateOffsetInterval",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Instrument",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PayoffInterval",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PayoffStep",
                table: "Accounts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Percent",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Private",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Savings",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "StartBalance",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SyncId",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "User",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

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
        }
    }
}

#pragma warning restore CS1591