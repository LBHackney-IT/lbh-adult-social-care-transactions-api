using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class InvoicePaymentStatusTrackUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_InvoiceItemPaymentStatuses_InvoiceItemPaymentS~",
                table: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "InvoiceItemPaymentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_InvoiceItemPaymentStatusId",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "InvoiceItemPaymentStatusId",
                table: "InvoiceItems");

            migrationBuilder.AlterColumn<string>(
                name: "StatusName",
                table: "InvoiceStatuses",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ApprovalStatus",
                table: "InvoiceStatuses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "InvoiceStatuses",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DisplayName", "StatusName" },
                values: new object[] { "Draft", "New" });

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DisplayName", "StatusName" },
                values: new object[] { "Approve", "Approved" });

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DisplayName", "StatusName" },
                values: new object[] { "In pay Run", "In Pay Run" });

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DisplayName", "StatusName" },
                values: new object[] { "Hold", "Held" });

            migrationBuilder.InsertData(
                table: "InvoiceStatuses",
                columns: new[] { "Id", "ApprovalStatus", "DisplayName", "StatusName" },
                values: new object[,]
                {
                    { 5, false, "Accept", "Accepted" },
                    { 6, false, "Release", "Released" },
                    { 7, false, "Paid", "Paid" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "InvoiceStatuses");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "InvoiceStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "StatusName",
                table: "InvoiceStatuses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "InvoiceItemPaymentStatusId",
                table: "InvoiceItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InvoiceItemPaymentStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    StatusName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItemPaymentStatuses", x => x.StatusId);
                });

            migrationBuilder.InsertData(
                table: "InvoiceItemPaymentStatuses",
                columns: new[] { "StatusId", "DisplayName", "StatusName" },
                values: new object[,]
                {
                    { 1, "New", "New" },
                    { 2, "Not Started", "Not Started" },
                    { 3, "Hold", "Held" },
                    { 4, "Pay", "Paid" },
                    { 5, "Release", "Released" },
                    { 6, "In New Pay Run", "In New Pay Run" }
                });

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusName",
                value: "Draft");

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 2,
                column: "StatusName",
                value: "Held");

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 3,
                column: "StatusName",
                value: "Accepted");

            migrationBuilder.UpdateData(
                table: "InvoiceStatuses",
                keyColumn: "Id",
                keyValue: 4,
                column: "StatusName",
                value: "Released");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceItemPaymentStatusId",
                table: "InvoiceItems",
                column: "InvoiceItemPaymentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_InvoiceItemPaymentStatuses_InvoiceItemPaymentS~",
                table: "InvoiceItems",
                column: "InvoiceItemPaymentStatusId",
                principalTable: "InvoiceItemPaymentStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
