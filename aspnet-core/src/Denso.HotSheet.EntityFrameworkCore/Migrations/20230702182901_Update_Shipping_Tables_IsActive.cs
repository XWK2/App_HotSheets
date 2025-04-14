using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_Shipping_Tables_IsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code2",
                table: "DensoCountries",
                newName: "SegroveCode");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "DensoCountries",
                newName: "SatCode");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoUnitMeasuresSAT",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoUnitMeasures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoSuppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoSupplierAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoSpecialExpeditedReasons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "OriginCountryId",
                table: "DensoHotSheetShipProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoHotSheetTerms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoHotSheetReasons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoServices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoRMAAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoProductCodesSAT",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoPlants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoPaymentTerms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoPartNumbers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoPaidBy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoPackaging",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoIEStaff",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoDocumentTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoDivisions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoDepartments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoCustomers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoCustomerPlants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DensoCountries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DensoCode",
                table: "DensoCountries",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoCountries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NameSpanish",
                table: "DensoCountries",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoCarriers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoAccountingStaff",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoUnitMeasuresSAT");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoUnitMeasures");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoSuppliers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoSupplierAddresses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoSpecialExpeditedReasons");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoHotSheetTerms");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoHotSheetReasons");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoServices");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoRMAAssignments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoProductCodesSAT");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoPlants");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoPaymentTerms");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoPartNumbers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoPaidBy");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoPackaging");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoIEStaff");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoDocumentTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoDivisions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoDepartments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoCustomers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoCustomerPlants");

            migrationBuilder.DropColumn(
                name: "DensoCode",
                table: "DensoCountries");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoCountries");

            migrationBuilder.DropColumn(
                name: "NameSpanish",
                table: "DensoCountries");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoCarriers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoAccountingStaff");

            migrationBuilder.RenameColumn(
                name: "SegroveCode",
                table: "DensoCountries",
                newName: "Code2");

            migrationBuilder.RenameColumn(
                name: "SatCode",
                table: "DensoCountries",
                newName: "Code");

            migrationBuilder.AlterColumn<int>(
                name: "OriginCountryId",
                table: "DensoHotSheetShipProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DensoCountries",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
