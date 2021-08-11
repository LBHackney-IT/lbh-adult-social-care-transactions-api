using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class InvoiceItemUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "InvoiceItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "InvoiceItems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
