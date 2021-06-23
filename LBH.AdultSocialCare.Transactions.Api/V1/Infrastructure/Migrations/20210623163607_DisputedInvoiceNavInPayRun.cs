using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class DisputedInvoiceNavInPayRun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoices_PayRunItemId",
                table: "DisputedInvoices",
                column: "PayRunItemId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DisputedInvoices_PayRunItemId",
                table: "DisputedInvoices");
        }
    }
}
