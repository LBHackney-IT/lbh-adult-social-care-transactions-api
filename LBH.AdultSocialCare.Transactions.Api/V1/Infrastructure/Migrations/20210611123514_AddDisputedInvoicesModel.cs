using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class AddDisputedInvoicesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisputedInvoices",
                columns: table => new
                {
                    DisputedInvoiceId = table.Column<Guid>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: false),
                    InvoiceItemId = table.Column<Guid>(nullable: true),
                    ActionRequiredFromId = table.Column<int>(nullable: false),
                    ReasonForHolding = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisputedInvoices", x => x.DisputedInvoiceId);
                    table.ForeignKey(
                        name: "FK_DisputedInvoices_Departments_ActionRequiredFromId",
                        column: x => x.ActionRequiredFromId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputedInvoices_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputedInvoices_InvoiceItems_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "InvoiceItems",
                        principalColumn: "InvoiceItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoices_ActionRequiredFromId",
                table: "DisputedInvoices",
                column: "ActionRequiredFromId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoices_InvoiceItemId",
                table: "DisputedInvoices",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoices_InvoiceId_InvoiceItemId",
                table: "DisputedInvoices",
                columns: new[] { "InvoiceId", "InvoiceItemId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisputedInvoices");
        }
    }
}
