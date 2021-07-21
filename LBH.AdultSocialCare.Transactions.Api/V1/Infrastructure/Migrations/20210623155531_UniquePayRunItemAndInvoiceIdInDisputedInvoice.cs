using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class UniquePayRunItemAndInvoiceIdInDisputedInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DisputedInvoices_PayRunItemId_InvoiceId_InvoiceItemId",
                table: "DisputedInvoices");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoices_PayRunItemId_InvoiceId",
                table: "DisputedInvoices",
                columns: new[] { "PayRunItemId", "InvoiceId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DisputedInvoices_PayRunItemId_InvoiceId",
                table: "DisputedInvoices");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoices_PayRunItemId_InvoiceId_InvoiceItemId",
                table: "DisputedInvoices",
                columns: new[] { "PayRunItemId", "InvoiceId", "InvoiceItemId" },
                unique: true);
        }
    }
}
