using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class LimitPrecisionOnDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RemainingBalance",
                table: "PayRunItems",
                type: "decimal(7, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaidAmount",
                table: "PayRunItems",
                type: "decimal(7, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Invoices",
                type: "decimal(7, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "VatAmount",
                table: "InvoiceItems",
                type: "decimal(7, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "InvoiceItems",
                type: "decimal(7, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "InvoiceItems",
                type: "decimal(7, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "InvoiceItems",
                type: "decimal(7, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerUnit",
                table: "InvoiceItems",
                type: "decimal(7, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RemainingBalance",
                table: "PayRunItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaidAmount",
                table: "PayRunItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "VatAmount",
                table: "InvoiceItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "InvoiceItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SubTotal",
                table: "InvoiceItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "InvoiceItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerUnit",
                table: "InvoiceItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)");
        }
    }
}
