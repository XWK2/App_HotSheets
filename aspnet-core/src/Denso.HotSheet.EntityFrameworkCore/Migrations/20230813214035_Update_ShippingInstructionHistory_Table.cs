using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_HotSheetHistory_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoInterfaceLogs_DensoInterfaces_InterfaceId",
                table: "DensoInterfaceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPrices_DensoCustomers_CustomerId",
                table: "DensoPartNumberPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPrices_DensoPartNumbers_PartNumberId",
                table: "DensoPartNumberPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoCustomers_CustomerId",
                table: "DensoPartNumberPricesInternal");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoPartNumbersInternal_PartNumberInternalId",
                table: "DensoPartNumberPricesInternal");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheetApprovals_DensoHotSheet_HotSheetShiptId",
                table: "DensoHotSheetApprovals");

            migrationBuilder.DropIndex(
                name: "IX_DensoStaff_UserId",
                table: "DensoStaff");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "DensoStaff",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PackagingId",
                table: "DensoHotSheetShipPackaging",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserIdNotified",
                table: "DensoHotSheetHistory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "HotSheetShiptId",
                table: "DensoHotSheetApprovals",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PartNumberInternalId",
                table: "DensoPartNumberPricesInternal",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "DensoPartNumberPricesInternal",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PartNumberId",
                table: "DensoPartNumberPrices",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "DensoPartNumberPrices",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "InterfaceId",
                table: "DensoInterfaceLogs",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_DensoStaff_UserId",
                table: "DensoStaff",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoInterfaceLogs_DensoInterfaces_InterfaceId",
                table: "DensoInterfaceLogs",
                column: "InterfaceId",
                principalTable: "DensoInterfaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPrices_DensoCustomers_CustomerId",
                table: "DensoPartNumberPrices",
                column: "CustomerId",
                principalTable: "DensoCustomers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPrices_DensoPartNumbers_PartNumberId",
                table: "DensoPartNumberPrices",
                column: "PartNumberId",
                principalTable: "DensoPartNumbers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoCustomers_CustomerId",
                table: "DensoPartNumberPricesInternal",
                column: "CustomerId",
                principalTable: "DensoCustomers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoPartNumbersInternal_PartNumberInternalId",
                table: "DensoPartNumberPricesInternal",
                column: "PartNumberInternalId",
                principalTable: "DensoPartNumbersInternal",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheetApprovals_DensoHotSheet_HotSheetShiptId",
                table: "DensoHotSheetApprovals",
                column: "HotSheetShiptId",
                principalTable: "DensoHotSheet",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoInterfaceLogs_DensoInterfaces_InterfaceId",
                table: "DensoInterfaceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPrices_DensoCustomers_CustomerId",
                table: "DensoPartNumberPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPrices_DensoPartNumbers_PartNumberId",
                table: "DensoPartNumberPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoCustomers_CustomerId",
                table: "DensoPartNumberPricesInternal");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoPartNumbersInternal_PartNumberInternalId",
                table: "DensoPartNumberPricesInternal");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheetApprovals_DensoHotSheet_HotSheetShiptId",
                table: "DensoHotSheetApprovals");

            migrationBuilder.DropIndex(
                name: "IX_DensoStaff_UserId",
                table: "DensoStaff");

            migrationBuilder.DropColumn(
                name: "UserIdNotified",
                table: "DensoHotSheetHistory");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "DensoStaff",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PackagingId",
                table: "DensoHotSheetShipPackaging",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "HotSheetShiptId",
                table: "DensoHotSheetApprovals",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PartNumberInternalId",
                table: "DensoPartNumberPricesInternal",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "DensoPartNumberPricesInternal",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PartNumberId",
                table: "DensoPartNumberPrices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "DensoPartNumberPrices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "InterfaceId",
                table: "DensoInterfaceLogs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DensoStaff_UserId",
                table: "DensoStaff",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DensoInterfaceLogs_DensoInterfaces_InterfaceId",
                table: "DensoInterfaceLogs",
                column: "InterfaceId",
                principalTable: "DensoInterfaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPrices_DensoCustomers_CustomerId",
                table: "DensoPartNumberPrices",
                column: "CustomerId",
                principalTable: "DensoCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPrices_DensoPartNumbers_PartNumberId",
                table: "DensoPartNumberPrices",
                column: "PartNumberId",
                principalTable: "DensoPartNumbers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoCustomers_CustomerId",
                table: "DensoPartNumberPricesInternal",
                column: "CustomerId",
                principalTable: "DensoCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumberPricesInternal_DensoPartNumbersInternal_PartNumberInternalId",
                table: "DensoPartNumberPricesInternal",
                column: "PartNumberInternalId",
                principalTable: "DensoPartNumbersInternal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheetApprovals_DensoHotSheet_HotSheetShiptId",
                table: "DensoHotSheetApprovals",
                column: "HotSheetShiptId",
                principalTable: "DensoHotSheet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
