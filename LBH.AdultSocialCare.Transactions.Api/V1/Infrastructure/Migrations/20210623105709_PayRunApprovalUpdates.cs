using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class PayRunApprovalUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayRunItemId",
                table: "Ledgers");

            migrationBuilder.AlterColumn<long>(
                name: "BillPaymentId",
                table: "Ledgers",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<Guid>(
                name: "InvoicePaymentId",
                table: "Ledgers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MoneyOut",
                table: "Ledgers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "PayRunBillId",
                table: "Ledgers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvoicePayments",
                columns: table => new
                {
                    InvoicePaymentId = table.Column<Guid>(nullable: false),
                    PayRunItemId = table.Column<Guid>(nullable: false),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    RemainingBalance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayments", x => x.InvoicePaymentId);
                    table.ForeignKey(
                        name: "FK_InvoicePayments_PayRunItems_PayRunItemId",
                        column: x => x.PayRunItemId,
                        principalTable: "PayRunItems",
                        principalColumn: "PayRunItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayRunSupplierBills",
                columns: table => new
                {
                    PayRunBillId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    SupplierId = table.Column<long>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRunSupplierBills", x => x.PayRunBillId);
                });

            migrationBuilder.CreateTable(
                name: "PayRunSupplierBillItems",
                columns: table => new
                {
                    PayRunBillItemId = table.Column<Guid>(nullable: false),
                    PayRunSupplierBillId = table.Column<Guid>(nullable: false),
                    PayRunItemId = table.Column<Guid>(nullable: false),
                    InvoicePaymentId = table.Column<Guid>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: false),
                    InvoiceItemId = table.Column<Guid>(nullable: true),
                    PaidAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRunSupplierBillItems", x => x.PayRunBillItemId);
                    table.ForeignKey(
                        name: "FK_PayRunSupplierBillItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayRunSupplierBillItems_InvoiceItems_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "InvoiceItems",
                        principalColumn: "InvoiceItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayRunSupplierBillItems_InvoicePayments_InvoicePaymentId",
                        column: x => x.InvoicePaymentId,
                        principalTable: "InvoicePayments",
                        principalColumn: "InvoicePaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayRunSupplierBillItems_PayRunItems_PayRunItemId",
                        column: x => x.PayRunItemId,
                        principalTable: "PayRunItems",
                        principalColumn: "PayRunItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayRunSupplierBillItems_PayRunSupplierBills_PayRunSupplierB~",
                        column: x => x.PayRunSupplierBillId,
                        principalTable: "PayRunSupplierBills",
                        principalColumn: "PayRunBillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_BillPaymentId",
                table: "Ledgers",
                column: "BillPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_PayRunBillId",
                table: "Ledgers",
                column: "PayRunBillId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_PayRunItemId",
                table: "InvoicePayments",
                column: "PayRunItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunSupplierBillItems_InvoiceId",
                table: "PayRunSupplierBillItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunSupplierBillItems_InvoiceItemId",
                table: "PayRunSupplierBillItems",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunSupplierBillItems_InvoicePaymentId",
                table: "PayRunSupplierBillItems",
                column: "InvoicePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunSupplierBillItems_PayRunItemId",
                table: "PayRunSupplierBillItems",
                column: "PayRunItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunSupplierBillItems_PayRunSupplierBillId",
                table: "PayRunSupplierBillItems",
                column: "PayRunSupplierBillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_BillPayments_BillPaymentId",
                table: "Ledgers",
                column: "BillPaymentId",
                principalTable: "BillPayments",
                principalColumn: "BillPaymentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_PayRunSupplierBills_PayRunBillId",
                table: "Ledgers",
                column: "PayRunBillId",
                principalTable: "PayRunSupplierBills",
                principalColumn: "PayRunBillId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_BillPayments_BillPaymentId",
                table: "Ledgers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_PayRunSupplierBills_PayRunBillId",
                table: "Ledgers");

            migrationBuilder.DropTable(
                name: "PayRunSupplierBillItems");

            migrationBuilder.DropTable(
                name: "InvoicePayments");

            migrationBuilder.DropTable(
                name: "PayRunSupplierBills");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_BillPaymentId",
                table: "Ledgers");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_PayRunBillId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "InvoicePaymentId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "MoneyOut",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "PayRunBillId",
                table: "Ledgers");

            migrationBuilder.AlterColumn<long>(
                name: "BillPaymentId",
                table: "Ledgers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PayRunItemId",
                table: "Ledgers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
