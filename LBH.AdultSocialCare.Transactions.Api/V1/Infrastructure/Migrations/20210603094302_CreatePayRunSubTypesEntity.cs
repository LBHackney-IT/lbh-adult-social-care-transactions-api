using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class CreatePayRunSubTypesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PayRunTypes",
                keyColumn: "PayRunTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PayRunTypes",
                keyColumn: "PayRunTypeId",
                keyValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "PayRunSubTypeId",
                table: "PayRuns",
                nullable: true);

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

            migrationBuilder.InsertData(
                table: "PayRunSubTypes",
                columns: new[] { "PayRunSubTypeId", "PayRunTypeId", "SubTypeName" },
                values: new object[,]
                {
                    { 1, 1, "Residential Release Holds" },
                    { 2, 2, "Direct Payments Release Holds" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayRuns_PayRunSubTypeId",
                table: "PayRuns",
                column: "PayRunSubTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayRunSubTypes_PayRunTypeId",
                table: "PayRunSubTypes",
                column: "PayRunTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayRuns_PayRunSubTypes_PayRunSubTypeId",
                table: "PayRuns",
                column: "PayRunSubTypeId",
                principalTable: "PayRunSubTypes",
                principalColumn: "PayRunSubTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayRuns_PayRunSubTypes_PayRunSubTypeId",
                table: "PayRuns");

            migrationBuilder.DropTable(
                name: "PayRunSubTypes");

            migrationBuilder.DropIndex(
                name: "IX_PayRuns_PayRunSubTypeId",
                table: "PayRuns");

            migrationBuilder.DropColumn(
                name: "PayRunSubTypeId",
                table: "PayRuns");

            migrationBuilder.InsertData(
                table: "PayRunTypes",
                columns: new[] { "PayRunTypeId", "TypeName" },
                values: new object[,]
                {
                    { 4, "Residential Release Holds" },
                    { 5, "Direct Payments Release Holds" }
                });
        }
    }
}
