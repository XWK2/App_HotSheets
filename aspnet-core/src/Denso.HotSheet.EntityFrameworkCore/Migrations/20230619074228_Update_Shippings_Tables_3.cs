using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_HotSheets_Tables_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description1",
                table: "DensoPartNumbers");

            migrationBuilder.DropColumn(
                name: "Description2",
                table: "DensoPartNumbers");

            migrationBuilder.RenameColumn(
                name: "UnitMeasure",
                table: "DensoPartNumbers",
                newName: "UnitMeasureId");

            migrationBuilder.RenameColumn(
                name: "ProductKeySAT",
                table: "DensoPartNumbers",
                newName: "ProductCodeSAT");

            migrationBuilder.RenameColumn(
                name: "DescriptionSpanish2",
                table: "DensoPartNumbers",
                newName: "DescriptionSpanish");

            migrationBuilder.RenameColumn(
                name: "DescriptionSpanish1",
                table: "DensoPartNumbers",
                newName: "Description");

            migrationBuilder.AddColumn<bool>(
                name: "ExportedCigma",
                table: "DensoHotSheet",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExportedCigmaDate",
                table: "DensoHotSheet",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PoNumber",
                table: "DensoHotSheetShipProducts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maker",
                table: "DensoHotSheetShipProducts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "DensoHotSheetShipProducts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Serial",
                table: "DensoHotSheetShipProducts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TechInfo",
                table: "DensoHotSheetShipProducts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExportedCigma",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "ExportedCigmaDate",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "Maker",
                table: "DensoHotSheetShipProducts");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "DensoHotSheetShipProducts");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "DensoHotSheetShipProducts");

            migrationBuilder.DropColumn(
                name: "TechInfo",
                table: "DensoHotSheetShipProducts");

            migrationBuilder.RenameColumn(
                name: "UnitMeasureId",
                table: "DensoPartNumbers",
                newName: "UnitMeasure");

            migrationBuilder.RenameColumn(
                name: "ProductCodeSAT",
                table: "DensoPartNumbers",
                newName: "ProductKeySAT");

            migrationBuilder.RenameColumn(
                name: "DescriptionSpanish",
                table: "DensoPartNumbers",
                newName: "DescriptionSpanish2");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "DensoPartNumbers",
                newName: "DescriptionSpanish1");

            migrationBuilder.AlterColumn<string>(
                name: "PoNumber",
                table: "DensoHotSheetShipProducts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description1",
                table: "DensoPartNumbers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description2",
                table: "DensoPartNumbers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
