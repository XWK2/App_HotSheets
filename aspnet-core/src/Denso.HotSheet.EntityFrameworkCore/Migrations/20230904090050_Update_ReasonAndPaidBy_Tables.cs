using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_ReasonAndPaidBy_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NoPayment",
                table: "DensoHotSheetReasons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Remittence",
                table: "DensoHotSheetReasons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DensoPaidByPaymentTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaidById = table.Column<long>(type: "bigint", nullable: false),
                    PaymentTermId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPaidByPaymentTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoPaidByPaymentTerms_DensoPaidBy_PaidById",
                        column: x => x.PaidById,
                        principalTable: "DensoPaidBy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DensoPaidByHotSheetTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaidById = table.Column<long>(type: "bigint", nullable: false),
                    HotSheetTermId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPaidByHotSheetTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoPaidByHotSheetTerms_DensoPaidBy_PaidById",
                        column: x => x.PaidById,
                        principalTable: "DensoPaidBy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoPaidByPaymentTerms_PaidById",
                table: "DensoPaidByPaymentTerms",
                column: "PaidById");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPaidByHotSheetTerms_PaidById",
                table: "DensoPaidByHotSheetTerms",
                column: "PaidById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensoPaidByPaymentTerms");

            migrationBuilder.DropTable(
                name: "DensoPaidByHotSheetTerms");

            migrationBuilder.DropColumn(
                name: "NoPayment",
                table: "DensoHotSheetReasons");

            migrationBuilder.DropColumn(
                name: "Remittence",
                table: "DensoHotSheetReasons");
        }
    }
}
