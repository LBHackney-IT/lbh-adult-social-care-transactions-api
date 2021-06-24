using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class MarkInvoiceApprovalStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "ApprovalStatus",
                value: true);

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "ApprovalStatus",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "ApprovalStatus",
                value: false);

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 5,
                column: "ApprovalStatus",
                value: false);
        }
    }
}
