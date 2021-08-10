using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class AddInvoiceRejectedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InvoiceStatuses",
                columns: new[] { "Id", "ApprovalStatus", "DisplayName", "StatusName" },
                values: new object[] { 8, true, "Reject", "Rejected" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
