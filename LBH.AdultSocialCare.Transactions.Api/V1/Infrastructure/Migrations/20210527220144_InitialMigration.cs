using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LBH.AdultSocialCare.Transactions.Api.Migrations
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
                    InvoiceStatusId = table.Column<int>(nullable: false),
                    PayRunId = table.Column<Guid>(nullable: false),
                    InvoiceNumber = table.Column<Guid>(nullable: false),
                    PackageTypeId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    ServiceUserId = table.Column<Guid>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
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
                    ItemName = table.Column<string>(nullable: true),
                    PricePerUnit = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Vat = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    LedgerId = table.Column<Guid>(nullable: false),
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
                });

            //migrationBuilder.InsertData(
            //    table: "BillStatuses",
            //    columns: new[] { "Id", "StatusName" },
            //    values: new object[,]
            //    {
            //        { 1, "Outstanding" },
            //        { 2, "Paid" },
            //        { 3, "Overdue" }
            //    });

            //migrationBuilder.InsertData(
            //    table: "InvoiceStatuses",
            //    columns: new[] { "Id", "StatusName" },
            //    values: new object[,]
            //    {
            //        { 1, "Draft" },
            //        { 2, "Paid" },
            //        { 3, "Held" }
            //    });

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
                name: "IX_Invoices_InvoiceStatusId",
                table: "Invoices",
                column: "InvoiceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PackageTypeId",
                table: "Invoices",
                column: "PackageTypeId");
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
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "BillStatuses");

            migrationBuilder.DropTable(
                name: "InvoiceStatuses");

            migrationBuilder.DropTable(
                name: "PackageType");
        }
    }
}
