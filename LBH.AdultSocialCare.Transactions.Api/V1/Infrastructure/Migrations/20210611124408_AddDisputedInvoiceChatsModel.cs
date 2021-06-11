using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LBH.AdultSocialCare.Transactions.Api.V1.Infrastructure.Migrations
{
    public partial class AddDisputedInvoiceChatsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisputedInvoiceChats",
                columns: table => new
                {
                    DisputedInvoiceChatId = table.Column<Guid>(nullable: false),
                    DisputedInvoiceId = table.Column<Guid>(nullable: false),
                    MessageRead = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    MessageFromId = table.Column<int>(nullable: true),
                    ActionRequiredFromId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisputedInvoiceChats", x => x.DisputedInvoiceChatId);
                    table.ForeignKey(
                        name: "FK_DisputedInvoiceChats_Departments_ActionRequiredFromId",
                        column: x => x.ActionRequiredFromId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputedInvoiceChats_DisputedInvoices_DisputedInvoiceId",
                        column: x => x.DisputedInvoiceId,
                        principalTable: "DisputedInvoices",
                        principalColumn: "DisputedInvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisputedInvoiceChats_Departments_MessageFromId",
                        column: x => x.MessageFromId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoiceChats_ActionRequiredFromId",
                table: "DisputedInvoiceChats",
                column: "ActionRequiredFromId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoiceChats_DisputedInvoiceId",
                table: "DisputedInvoiceChats",
                column: "DisputedInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DisputedInvoiceChats_MessageFromId",
                table: "DisputedInvoiceChats",
                column: "MessageFromId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisputedInvoiceChats");
        }
    }
}
