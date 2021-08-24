using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class InvoiceItemReclaimFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "InvoiceItems",
                type: "decimal(7, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(13, 2)");

            migrationBuilder.AddColumn<string>(
                name: "ClaimedBy",
                table: "InvoiceItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceEffect",
                table: "InvoiceItems",
                nullable: false,
                defaultValue: "Add");

            migrationBuilder.AddColumn<string>(
                name: "ReclaimedFrom",
                table: "InvoiceItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimedBy",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "PriceEffect",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "ReclaimedFrom",
                table: "InvoiceItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "InvoiceItems",
                type: "decimal(13, 2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7, 2)");
        }
    }
}
