using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class UpdatePayRunStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PayRunStatuses",
                keyColumn: "PayRunStatusId",
                keyValue: 2,
                column: "StatusName",
                value: "SubmittedForApproval");

            migrationBuilder.InsertData(
                table: "PayRunStatuses",
                columns: new[] { "PayRunStatusId", "StatusName" },
                values: new object[] { 3, "Approved" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PayRunStatuses",
                keyColumn: "PayRunStatusId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "PayRunStatuses",
                keyColumn: "PayRunStatusId",
                keyValue: 2,
                column: "StatusName",
                value: "Approved");
        }
    }
}
