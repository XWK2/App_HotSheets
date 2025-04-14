using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_PartNumbers_Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPrices_DensoPartNumbers_PartNumberId",
                table: "DensoPartNumberPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoPartNumbersInternal_PartNumberInternalId",
                table: "DensoPartNumberPricesInternal");

            migrationBuilder.DropIndex(
                name: "IX_DensoPartNumberPrices_PartNumberId",
                table: "DensoPartNumberPrices");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoPartNumbers_PartNumberInternalId",
                table: "DensoPartNumberPricesInternal",
                column: "PartNumberInternalId",
                principalTable: "DensoPartNumbers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoPartNumbers_PartNumberInternalId",
                table: "DensoPartNumberPricesInternal");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPartNumberPrices_PartNumberId",
                table: "DensoPartNumberPrices",
                column: "PartNumberId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPrices_DensoPartNumbers_PartNumberId",
                table: "DensoPartNumberPrices",
                column: "PartNumberId",
                principalTable: "DensoPartNumbers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoPartNumbersInternal_PartNumberInternalId",
                table: "DensoPartNumberPricesInternal",
                column: "PartNumberInternalId",
                principalTable: "DensoPartNumbersInternal",
                principalColumn: "Id");
        }
    }
}
