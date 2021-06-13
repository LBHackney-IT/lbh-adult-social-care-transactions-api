using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class SupplierTaxRateUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Bills_BillId",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BillStatuses_BillStatusId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_BillStatusId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_BillItems_BillId",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillStatusId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DateDue",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DateEntered",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Ref",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BillItems");

            migrationBuilder.AlterColumn<long>(
                name: "SupplierId",
                table: "Bills",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "BillId",
                table: "Bills",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "BillDueDate",
                table: "Bills",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "BillPaymentStatusId",
                table: "Bills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateBilled",
                table: "Bills",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "PackageTypeId",
                table: "Bills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ServiceFromDate",
                table: "Bills",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ServiceToDate",
                table: "Bills",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "SupplierRef",
                table: "Bills",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalBilled",
                table: "Bills",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "BillItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "BillItemId",
                table: "BillItems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "BillItemStatusId",
                table: "BillItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BillPaymentStatus",
                table: "BillItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "HackneySupplierBillId",
                table: "BillItems",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ItemDescription",
                table: "BillItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "BillItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageTypeId",
                table: "BillItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TaxRatePercentage",
                table: "BillItems",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<long>(
                name: "BillId",
                table: "BillFiles",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "BillPayments",
                columns: table => new
                {
                    BillPaymentId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BillItemId = table.Column<long>(nullable: false),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    RemainingBalance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPayments", x => x.BillPaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierName = table.Column<string>(nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    UpdaterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierCreditNotes",
                columns: table => new
                {
                    CreditNoteId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    AmountOverPaid = table.Column<decimal>(nullable: false),
                    BillPaymentFromId = table.Column<long>(nullable: false),
                    AmountRemaining = table.Column<decimal>(nullable: false),
                    BillPaymentPaidTo = table.Column<long>(nullable: false),
                    DatePaidForward = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierCreditNotes", x => x.CreditNoteId);
                    table.ForeignKey(
                        name: "FK_SupplierCreditNotes_BillPayments_BillPaymentFromId",
                        column: x => x.BillPaymentFromId,
                        principalTable: "BillPayments",
                        principalColumn: "BillPaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierTaxRates",
                columns: table => new
                {
                    TaxRateId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VATPercentage = table.Column<float>(nullable: false),
                    SupplierId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierTaxRates", x => x.TaxRateId);
                    table.ForeignKey(
                        name: "FK_SupplierTaxRates_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillPaymentStatusId",
                table: "Bills",
                column: "BillPaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PackageTypeId",
                table: "Bills",
                column: "PackageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_HackneySupplierBillId",
                table: "BillItems",
                column: "HackneySupplierBillId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierCreditNotes_BillPaymentFromId",
                table: "SupplierCreditNotes",
                column: "BillPaymentFromId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTaxRates_SupplierId",
                table: "SupplierTaxRates",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Bills_HackneySupplierBillId",
                table: "BillItems",
                column: "HackneySupplierBillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BillStatuses_BillPaymentStatusId",
                table: "Bills",
                column: "BillPaymentStatusId",
                principalTable: "BillStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_PackageType_PackageTypeId",
                table: "Bills",
                column: "PackageTypeId",
                principalTable: "PackageType",
                principalColumn: "PackageTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Bills_HackneySupplierBillId",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BillStatuses_BillPaymentStatusId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_PackageType_PackageTypeId",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "SupplierCreditNotes");

            migrationBuilder.DropTable(
                name: "SupplierTaxRates");

            migrationBuilder.DropTable(
                name: "BillPayments");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Bills_BillPaymentStatusId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PackageTypeId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_BillItems_HackneySupplierBillId",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "BillDueDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillPaymentStatusId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "DateBilled",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PackageTypeId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ServiceFromDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ServiceToDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "SupplierRef",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "TotalBilled",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillItemStatusId",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "BillPaymentStatus",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "HackneySupplierBillId",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "ItemDescription",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "PackageTypeId",
                table: "BillItems");

            migrationBuilder.DropColumn(
                name: "TaxRatePercentage",
                table: "BillItems");

            migrationBuilder.AlterColumn<int>(
                name: "SupplierId",
                table: "Bills",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<Guid>(
                name: "BillId",
                table: "Bills",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Bills",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "Bills",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "BillStatusId",
                table: "Bills",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateDue",
                table: "Bills",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateEntered",
                table: "Bills",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Ref",
                table: "Bills",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "BillItems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<Guid>(
                name: "BillItemId",
                table: "BillItems",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "BillId",
                table: "BillItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BillItems",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BillId",
                table: "BillFiles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillStatusId",
                table: "Bills",
                column: "BillStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Bills_BillId",
                table: "BillItems",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BillStatuses_BillStatusId",
                table: "Bills",
                column: "BillStatusId",
                principalTable: "BillStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
