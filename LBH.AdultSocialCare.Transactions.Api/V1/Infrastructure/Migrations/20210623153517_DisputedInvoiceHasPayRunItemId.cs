using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class DisputedInvoiceHasPayRunItemId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DisputedInvoices_InvoiceId_InvoiceItemId",
                table: "DisputedInvoices");

            migrationBuilder.AddColumn<Guid>(
                name: "PayRunItemId",
                table: "DisputedInvoices",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoices_InvoiceId",
                table: "DisputedInvoices",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoices_PayRunItemId_InvoiceId_InvoiceItemId",
                table: "DisputedInvoices",
                columns: new[] { "PayRunItemId", "InvoiceId", "InvoiceItemId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DisputedInvoices_PayRunItems_PayRunItemId",
                table: "DisputedInvoices",
                column: "PayRunItemId",
                principalTable: "PayRunItems",
                principalColumn: "PayRunItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DisputedInvoices_PayRunItems_PayRunItemId",
                table: "DisputedInvoices");

            migrationBuilder.DropIndex(
                name: "IX_DisputedInvoices_InvoiceId",
                table: "DisputedInvoices");

            migrationBuilder.DropIndex(
                name: "IX_DisputedInvoices_PayRunItemId_InvoiceId_InvoiceItemId",
                table: "DisputedInvoices");

            migrationBuilder.DropColumn(
                name: "PayRunItemId",
                table: "DisputedInvoices");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoices_InvoiceId_InvoiceItemId",
                table: "DisputedInvoices",
                columns: new[] { "InvoiceId", "InvoiceItemId" },
                unique: true);
        }
    }
}
