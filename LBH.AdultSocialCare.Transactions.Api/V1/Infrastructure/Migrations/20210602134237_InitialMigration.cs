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
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    Ref = table.Column<string>(nullable: true),
                    PackageId = table.Column<Guid>(nullable: false),
                    DateEntered = table.Column<DateTimeOffset>(nullable: false),
                    DateDue = table.Column<DateTimeOffset>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    AmountPaid = table.Column<decimal>(nullable: false),
                    BillStatusId = table.Column<int>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    UpdaterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bills_BillStatuses_BillStatusId",
                        column: x => x.BillStatusId,
                        principalTable: "BillStatuses",
                        principalColumn: "Id",
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
                        name: "FK_PayRuns_PayRunTypes_PayRunTypeId",
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
                    BillId = table.Column<Guid>(nullable: false),
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
                    BillItemId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    BillId = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    UpdaterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.BillItemId);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "BillId",
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
                table: "InvoiceItemPaymentStatuses",
                columns: new[] { "StatusId", "DisplayName", "StatusName" },
                values: new object[,]
                {
                    { 1, "Not Started", "Not Started" },
                    { 2, "Hold", "Held" },
                    { 3, "Pay", "Paid" }
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
                    { 3, "Held" },
                    { 2, "Paid" },
                    { 1, "Draft" }
                });

            migrationBuilder.InsertData(
                table: "PayRunStatuses",
                columns: new[] { "PayRunStatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Draft" },
                    { 2, "Approved" }
                });

            migrationBuilder.InsertData(
                table: "PayRunTypes",
                columns: new[] { "PayRunTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Residential Recurring" },
                    { 2, "Direct Payments" },
                    { 3, "Home Care" },
                    { 4, "Residential Release Holds" },
                    { 5, "Direct Payments Release Holds" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillFiles_BillId",
                table: "BillFiles",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillStatusId",
                table: "Bills",
                column: "BillStatusId");

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
                name: "IX_PayRuns_PayRunStatusId",
                table: "PayRuns",
                column: "PayRunStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRuns_PayRunTypeId",
                table: "PayRuns",
                column: "PayRunTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillFiles");

            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropTable(
                name: "example_table");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "InvoiceNumbers");

            migrationBuilder.DropTable(
                name: "PayRuns");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "InvoiceItemPaymentStatuses");

            migrationBuilder.DropTable(
                name: "PayRunStatuses");

            migrationBuilder.DropTable(
                name: "PayRunTypes");

            migrationBuilder.DropTable(
                name: "BillStatuses");

            migrationBuilder.DropTable(
                name: "InvoiceStatuses");

            migrationBuilder.DropTable(
                name: "PackageType");
        }
    }
}
