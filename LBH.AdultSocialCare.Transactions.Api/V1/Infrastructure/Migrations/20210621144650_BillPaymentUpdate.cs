using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class BillPaymentUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BillItemId",
                table: "BillPayments",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "BillId",
                table: "BillPayments",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_BillPayments_BillId",
                table: "BillPayments",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayments_Bills_BillId",
                table: "BillPayments",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "BillId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayments_Bills_BillId",
                table: "BillPayments");

            migrationBuilder.DropIndex(
                name: "IX_BillPayments_BillId",
                table: "BillPayments");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "BillPayments");

            migrationBuilder.AlterColumn<long>(
                name: "BillItemId",
                table: "BillPayments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
