using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class SeedSuppliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "Suppliers");

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "PackageTypeId", "SupplierName" },
                values: new object[,]
                {
                    { 1L, 1, "ABC Limited" },
                    { 2L, 2, "XYZ Ltd" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 2L);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Suppliers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdaterId",
                table: "Suppliers",
                type: "uuid",
                nullable: true);
        }
    }
}
