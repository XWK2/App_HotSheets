using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_HotSheet_Tables_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DensoUnitMeasures");

            migrationBuilder.DropColumn(
                name: "Origin",
                table: "DensoPartNumbers");

            migrationBuilder.DropColumn(
                name: "ProductCodeSAT",
                table: "DensoPartNumbers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DensoUnitMeasures",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DensoCode",
                table: "DensoUnitMeasures",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SatCode",
                table: "DensoUnitMeasures",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SegroveCode",
                table: "DensoUnitMeasures",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OriginCountryId",
                table: "DensoPartNumbers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductCodeSATId",
                table: "DensoPartNumbers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Payment",
                table: "DensoCustomers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_DensoPartNumbers_OriginCountryId",
                table: "DensoPartNumbers",
                column: "OriginCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPartNumbers_ProductCodeSATId",
                table: "DensoPartNumbers",
                column: "ProductCodeSATId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumbers_DensoCountries_OriginCountryId",
                table: "DensoPartNumbers",
                column: "OriginCountryId",
                principalTable: "DensoCountries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumbers_DensoProductCodesSAT_ProductCodeSATId",
                table: "DensoPartNumbers",
                column: "ProductCodeSATId",
                principalTable: "DensoProductCodesSAT",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumbers_DensoCountries_OriginCountryId",
                table: "DensoPartNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumbers_DensoProductCodesSAT_ProductCodeSATId",
                table: "DensoPartNumbers");

            migrationBuilder.DropIndex(
                name: "IX_DensoPartNumbers_OriginCountryId",
                table: "DensoPartNumbers");

            migrationBuilder.DropIndex(
                name: "IX_DensoPartNumbers_ProductCodeSATId",
                table: "DensoPartNumbers");

            migrationBuilder.DropColumn(
                name: "DensoCode",
                table: "DensoUnitMeasures");

            migrationBuilder.DropColumn(
                name: "SatCode",
                table: "DensoUnitMeasures");

            migrationBuilder.DropColumn(
                name: "SegroveCode",
                table: "DensoUnitMeasures");

            migrationBuilder.DropColumn(
                name: "OriginCountryId",
                table: "DensoPartNumbers");

            migrationBuilder.DropColumn(
                name: "ProductCodeSATId",
                table: "DensoPartNumbers");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "DensoCustomers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DensoUnitMeasures",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DensoUnitMeasures",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "DensoPartNumbers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductCodeSAT",
                table: "DensoPartNumbers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
