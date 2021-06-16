using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "BillStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "example_table",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_example_table", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItemPaymentStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItemPaymentStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceNumbers",
                columns: table => new
                {
                    InvoiceNumberId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Prefix = table.Column<string>(nullable: true),
                    CurrentInvoiceNumber = table.Column<long>(nullable: false),
                    PostFix = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceNumbers", x => x.InvoiceNumberId);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ledgers",
                columns: table => new
                {
                    LedgerId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    DateEntered = table.Column<DateTimeOffset>(nullable: false),
                    MoneyIn = table.Column<decimal>(nullable: false),
                    PayRunItemId = table.Column<long>(nullable: false),
                    BillPaymentId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ledgers", x => x.LedgerId);
                });

            migrationBuilder.CreateTable(
                name: "PackageType",
                columns: table => new
                {
                    PackageTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    PackageTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageType", x => x.PackageTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PayRunStatuses",
                columns: table => new
                {
                    PayRunStatusId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRunStatuses", x => x.PayRunStatusId);
                });

            migrationBuilder.CreateTable(
                name: "PayRunTypes",
                columns: table => new
                {
                    PayRunTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRunTypes", x => x.PayRunTypeId);
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
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    PackageTypeId = table.Column<int>(nullable: false),
                    PackageId = table.Column<Guid>(nullable: false),
                    SupplierRef = table.Column<string>(nullable: true),
                    SupplierId = table.Column<long>(nullable: false),
                    ServiceFromDate = table.Column<DateTimeOffset>(nullable: false),
                    ServiceToDate = table.Column<DateTimeOffset>(nullable: false),
                    DateBilled = table.Column<DateTimeOffset>(nullable: false),
                    BillDueDate = table.Column<DateTimeOffset>(nullable: false),
                    TotalBilled = table.Column<decimal>(nullable: false),
                    BillPaymentStatusId = table.Column<int>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    UpdaterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bills_BillStatuses_BillPaymentStatusId",
                        column: x => x.BillPaymentStatusId,
                        principalTable: "BillStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_PackageType_PackageTypeId",
                        column: x => x.PackageTypeId,
                        principalTable: "PackageType",
                        principalColumn: "PackageTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierName = table.Column<string>(nullable: true),
                    PackageTypeId = table.Column<int>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    UpdaterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_Suppliers_PackageType_PackageTypeId",
                        column: x => x.PackageTypeId,
                        principalTable: "PackageType",
                        principalColumn: "PackageTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayRunSubTypes",
                columns: table => new
                {
                    PayRunSubTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubTypeName = table.Column<string>(nullable: true),
                    PayRunTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRunSubTypes", x => x.PayRunSubTypeId);
                    table.ForeignKey(
                        name: "FK_PayRunSubTypes_PayRunTypes_PayRunTypeId",
                        column: x => x.PayRunTypeId,
                        principalTable: "PayRunTypes",
                        principalColumn: "PayRunTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillFiles",
                columns: table => new
                {
                    BillFileId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    BillId = table.Column<long>(nullable: false),
                    FileUrl = table.Column<string>(nullable: true),
                    OriginalFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillFiles", x => x.BillFileId);
                    table.ForeignKey(
                        name: "FK_BillFiles_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillItems",
                columns: table => new
                {
                    BillItemId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    HackneySupplierBillId = table.Column<long>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    ItemDescription = table.Column<string>(nullable: true),
                    Quantity = table.Column<float>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    PackageTypeId = table.Column<int>(nullable: false),
                    TaxRatePercentage = table.Column<float>(nullable: false),
                    BillItemStatusId = table.Column<int>(nullable: false),
                    BillPaymentStatus = table.Column<int>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    UpdaterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.BillItemId);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_HackneySupplierBillId",
                        column: x => x.HackneySupplierBillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    SupplierId = table.Column<long>(nullable: false),
                    PackageTypeId = table.Column<int>(nullable: false),
                    ServiceUserId = table.Column<Guid>(nullable: false),
                    DateInvoiced = table.Column<DateTimeOffset>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    SupplierVATPercent = table.Column<float>(nullable: false),
                    InvoiceStatusId = table.Column<int>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    UpdaterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceStatuses_InvoiceStatusId",
                        column: x => x.InvoiceStatusId,
                        principalTable: "InvoiceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_PackageType_PackageTypeId",
                        column: x => x.PackageTypeId,
                        principalTable: "PackageType",
                        principalColumn: "PackageTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
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

            migrationBuilder.CreateTable(
                name: "PayRuns",
                columns: table => new
                {
                    PayRunId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    PayRunNumber = table.Column<long>(nullable: false),
                    PayRunTypeId = table.Column<int>(nullable: false),
                    PayRunSubTypeId = table.Column<int>(nullable: true),
                    PayRunStatusId = table.Column<int>(nullable: false),
                    DateFrom = table.Column<DateTimeOffset>(nullable: false),
                    DateTo = table.Column<DateTimeOffset>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    UpdaterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRuns", x => x.PayRunId);
                    table.ForeignKey(
                        name: "FK_PayRuns_PayRunStatuses_PayRunStatusId",
                        column: x => x.PayRunStatusId,
                        principalTable: "PayRunStatuses",
                        principalColumn: "PayRunStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayRuns_PayRunSubTypes_PayRunSubTypeId",
                        column: x => x.PayRunSubTypeId,
                        principalTable: "PayRunSubTypes",
                        principalColumn: "PayRunSubTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayRuns_PayRunTypes_PayRunTypeId",
                        column: x => x.PayRunTypeId,
                        principalTable: "PayRunTypes",
                        principalColumn: "PayRunTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceItemId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: false),
                    InvoiceItemPaymentStatusId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    PricePerUnit = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    VatAmount = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    SupplierReturnItemId = table.Column<Guid>(nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    UpdaterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.InvoiceItemId);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_InvoiceItemPaymentStatuses_InvoiceItemPaymentS~",
                        column: x => x.InvoiceItemPaymentStatusId,
                        principalTable: "InvoiceItemPaymentStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "PayRunItems",
                columns: table => new
                {
                    PayRunItemId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    PayRunId = table.Column<Guid>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: false),
                    InvoiceItemId = table.Column<Guid>(nullable: true),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    RemainingBalance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRunItems", x => x.PayRunItemId);
                    table.ForeignKey(
                        name: "FK_PayRunItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayRunItems_InvoiceItems_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "InvoiceItems",
                        principalColumn: "InvoiceItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayRunItems_PayRuns_PayRunId",
                        column: x => x.PayRunId,
                        principalTable: "PayRuns",
                        principalColumn: "PayRunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DisputedInvoiceChats",
                columns: table => new
                {
                    DisputedInvoiceChatId = table.Column<Guid>(nullable: false),
                    DisputedInvoiceId = table.Column<Guid>(nullable: false),
                    MessageRead = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    MessageFromId = table.Column<int>(nullable: true),
                    ActionRequiredFromId = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisputedInvoiceChats", x => x.DisputedInvoiceChatId);
                    table.ForeignKey(
                        name: "FK_DisputedInvoiceChats_Departments_ActionRequiredFromId",
                        column: x => x.ActionRequiredFromId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputedInvoiceChats_DisputedInvoices_DisputedInvoiceId",
                        column: x => x.DisputedInvoiceId,
                        principalTable: "DisputedInvoices",
                        principalColumn: "DisputedInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputedInvoiceChats_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DisputedInvoiceChats_Departments_MessageFromId",
                        column: x => x.MessageFromId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BillStatuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 1, "Outstanding" },
                    { 2, "Paid" },
                    { 3, "Overdue" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "Brokerage" },
                    { 2, "Finance" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItemPaymentStatuses",
                columns: new[] { "StatusId", "DisplayName", "StatusName" },
                values: new object[,]
                {
                    { 5, "Release", "Released" },
                    { 4, "Pay", "Paid" },
                    { 6, "In New Pay Run", "In New Pay Run" },
                    { 2, "Not Started", "Not Started" },
                    { 1, "New", "New" },
                    { 3, "Hold", "Held" }
                });

            migrationBuilder.InsertData(
                table: "InvoiceNumbers",
                columns: new[] { "InvoiceNumberId", "CurrentInvoiceNumber", "PostFix", "Prefix" },
                values: new object[] { 1, 1L, null, "INV" });

            migrationBuilder.InsertData(
                table: "InvoiceStatuses",
                columns: new[] { "Id", "StatusName" },
                values: new object[,]
                {
                    { 1, "Draft" },
                    { 2, "Held" },
                    { 3, "Accepted" },
                    { 4, "Released" }
                });

            migrationBuilder.InsertData(
                table: "PayRunStatuses",
                columns: new[] { "PayRunStatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Draft" },
                    { 2, "SubmittedForApproval" },
                    { 3, "Approved" }
                });

            migrationBuilder.InsertData(
                table: "PayRunTypes",
                columns: new[] { "PayRunTypeId", "TypeName" },
                values: new object[,]
                {
                    { 2, "Direct Payments" },
                    { 1, "Residential Recurring" },
                    { 3, "Home Care" }
                });

            migrationBuilder.InsertData(
                table: "PayRunSubTypes",
                columns: new[] { "PayRunSubTypeId", "PayRunTypeId", "SubTypeName" },
                values: new object[,]
                {
                    { 1, 1, "Residential Release Holds" },
                    { 2, 2, "Direct Payments Release Holds" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillFiles_BillId",
                table: "BillFiles",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_HackneySupplierBillId",
                table: "BillItems",
                column: "HackneySupplierBillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillPaymentStatusId",
                table: "Bills",
                column: "BillPaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PackageTypeId",
                table: "Bills",
                column: "PackageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoiceChats_ActionRequiredFromId",
                table: "DisputedInvoiceChats",
                column: "ActionRequiredFromId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoiceChats_DisputedInvoiceId",
                table: "DisputedInvoiceChats",
                column: "DisputedInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoiceChats_InvoiceId",
                table: "DisputedInvoiceChats",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoiceChats_MessageFromId",
                table: "DisputedInvoiceChats",
                column: "MessageFromId");

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

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceItemPaymentStatusId",
                table: "InvoiceItems",
                column: "InvoiceItemPaymentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceStatusId",
                table: "Invoices",
                column: "InvoiceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PackageTypeId",
                table: "Invoices",
                column: "PackageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SupplierId",
                table: "Invoices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunItems_InvoiceId",
                table: "PayRunItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunItems_InvoiceItemId",
                table: "PayRunItems",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunItems_PayRunId_InvoiceId_InvoiceItemId",
                table: "PayRunItems",
                columns: new[] { "PayRunId", "InvoiceId", "InvoiceItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayRuns_PayRunStatusId",
                table: "PayRuns",
                column: "PayRunStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRuns_PayRunSubTypeId",
                table: "PayRuns",
                column: "PayRunSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRuns_PayRunTypeId",
                table: "PayRuns",
                column: "PayRunTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunSubTypes_PayRunTypeId",
                table: "PayRunSubTypes",
                column: "PayRunTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierCreditNotes_BillPaymentFromId",
                table: "SupplierCreditNotes",
                column: "BillPaymentFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_PackageTypeId",
                table: "Suppliers",
                column: "PackageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierTaxRates_SupplierId",
                table: "SupplierTaxRates",
                column: "SupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillFiles");

            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropTable(
                name: "DisputedInvoiceChats");

            migrationBuilder.DropTable(
                name: "example_table");

            migrationBuilder.DropTable(
                name: "InvoiceNumbers");

            migrationBuilder.DropTable(
                name: "Ledgers");

            migrationBuilder.DropTable(
                name: "PayRunItems");

            migrationBuilder.DropTable(
                name: "SupplierCreditNotes");

            migrationBuilder.DropTable(
                name: "SupplierTaxRates");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "DisputedInvoices");

            migrationBuilder.DropTable(
                name: "PayRuns");

            migrationBuilder.DropTable(
                name: "BillPayments");

            migrationBuilder.DropTable(
                name: "BillStatuses");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "PayRunStatuses");

            migrationBuilder.DropTable(
                name: "PayRunSubTypes");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "InvoiceItemPaymentStatuses");

            migrationBuilder.DropTable(
                name: "PayRunTypes");

            migrationBuilder.DropTable(
                name: "InvoiceStatuses");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "PackageType");
        }
    }
}
