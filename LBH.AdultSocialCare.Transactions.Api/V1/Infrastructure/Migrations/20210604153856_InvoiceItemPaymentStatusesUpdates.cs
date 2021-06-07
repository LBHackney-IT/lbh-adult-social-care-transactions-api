using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class InvoiceItemPaymentStatusesUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "InvoiceItemPaymentStatuses",
                keyColumn: "StatusId",
                keyValue: 1,
                columns: new[] { "DisplayName", "StatusName" },
                values: new object[] { "New", "New" });

            migrationBuilder.UpdateData(
                table: "InvoiceItemPaymentStatuses",
                keyColumn: "StatusId",
                keyValue: 2,
                columns: new[] { "DisplayName", "StatusName" },
                values: new object[] { "Not Started", "Not Started" });

            migrationBuilder.UpdateData(
                table: "InvoiceItemPaymentStatuses",
                keyColumn: "StatusId",
                keyValue: 3,
                columns: new[] { "DisplayName", "StatusName" },
                values: new object[] { "Hold", "Held" });

            migrationBuilder.InsertData(
                table: "InvoiceItemPaymentStatuses",
                columns: new[] { "StatusId", "DisplayName", "StatusName" },
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
                values: new object[,]
                {
                    { 4, "Pay", "Paid" },
                    { 5, "Release", "Released" },
                    { 6, "In New Pay Run", "In New Pay Run" }
                });
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceItemPaymentStatuses",
                keyColumn: "StatusId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InvoiceItemPaymentStatuses",
                keyColumn: "StatusId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InvoiceItemPaymentStatuses",
                keyColumn: "StatusId",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "InvoiceItemPaymentStatuses",
                keyColumn: "StatusId",
                keyValue: 1,
                columns: new[] { "DisplayName", "StatusName" },
                values: new object[] { "Not Started", "Not Started" });

            migrationBuilder.UpdateData(
                table: "InvoiceItemPaymentStatuses",
                keyColumn: "StatusId",
                keyValue: 2,
                columns: new[] { "DisplayName", "StatusName" },
                values: new object[] { "Hold", "Held" });

            migrationBuilder.UpdateData(
                table: "InvoiceItemPaymentStatuses",
                keyColumn: "StatusId",
                keyValue: 3,
                columns: new[] { "DisplayName", "StatusName" },
                values: new object[] { "Pay", "Paid" });
        }
    }
}
