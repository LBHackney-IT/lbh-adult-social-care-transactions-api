using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class CreatePayRunItemsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayRunItems",
                columns: table => new
                {
                    PayRunItemId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(nullable: false),
                    PayRunId = table.Column<Guid>(nullable: false),
                    InvoiceItemId = table.Column<Guid>(nullable: false),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    RemainingBalance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRunItems", x => x.PayRunItemId);
                    table.ForeignKey(
                        name: "FK_PayRunItems_InvoiceItems_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "InvoiceItems",
                        principalColumn: "InvoiceItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayRunItems_PayRuns_PayRunId",
                        column: x => x.PayRunId,
                        principalTable: "PayRuns",
                        principalColumn: "PayRunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayRunItems_InvoiceItemId",
                table: "PayRunItems",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunItems_PayRunId",
                table: "PayRunItems",
                column: "PayRunId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayRunItems");
        }
    }
}
