using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_HotSheet_Tables_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheetShipProducts_DensoCountries_OriginCountryId",
                table: "DensoHotSheetShipProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheetShipProducts_DensoUnitMeasures_UnitMeasureId",
                table: "DensoHotSheetShipProducts");

            migrationBuilder.DropTable(
                name: "DensoHotSheetPacking");

            migrationBuilder.AddColumn<int>(
                name: "SpecialExpeditedReasonId",
                table: "DensoHotSheet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code2",
                table: "DensoCountries",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DensoPackaging",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPackaging", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoSpecialExpeditedReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoSpecialExpeditedReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoHotSheetShipPackaging",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotSheetShiptId = table.Column<long>(type: "bigint", nullable: false),
                    PackagingId = table.Column<long>(type: "bigint", nullable: false),
                    DimensionLL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionWA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightPerBox = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    BoxQuantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    NetWeight = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    GrossWeight = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoHotSheetShipPackaging", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetShipPackaging_DensoPackaging_PackagingId",
                        column: x => x.PackagingId,
                        principalTable: "DensoPackaging",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetShipPackaging_DensoHotSheet_HotSheetShiptId",
                        column: x => x.HotSheetShiptId,
                        principalTable: "DensoHotSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_SpecialExpeditedReasonId",
                table: "DensoHotSheet",
                column: "SpecialExpeditedReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetShipPackaging_PackagingId",
                table: "DensoHotSheetShipPackaging",
                column: "PackagingId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetShipPackaging_HotSheetShiptId",
                table: "DensoHotSheetShipPackaging",
                column: "HotSheetShiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheetShipProducts_DensoCountries_OriginCountryId",
                table: "DensoHotSheetShipProducts",
                column: "OriginCountryId",
                principalTable: "DensoCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheetShipProducts_DensoUnitMeasures_UnitMeasureId",
                table: "DensoHotSheetShipProducts",
                column: "UnitMeasureId",
                principalTable: "DensoUnitMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheet_DensoSpecialExpeditedReasons_SpecialExpeditedReasonId",
                table: "DensoHotSheet",
                column: "SpecialExpeditedReasonId",
                principalTable: "DensoSpecialExpeditedReasons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheetShipProducts_DensoCountries_OriginCountryId",
                table: "DensoHotSheetShipProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheetShipProducts_DensoUnitMeasures_UnitMeasureId",
                table: "DensoHotSheetShipProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheet_DensoSpecialExpeditedReasons_SpecialExpeditedReasonId",
                table: "DensoHotSheet");

            migrationBuilder.DropTable(
                name: "DensoHotSheetShipPackaging");

            migrationBuilder.DropTable(
                name: "DensoSpecialExpeditedReasons");

            migrationBuilder.DropTable(
                name: "DensoPackaging");

            migrationBuilder.DropIndex(
                name: "IX_DensoHotSheet_SpecialExpeditedReasonId",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "SpecialExpeditedReasonId",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "Code2",
                table: "DensoCountries");

            migrationBuilder.CreateTable(
                name: "DensoHotSheetPacking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DimensionHA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionLL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionWA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GrossWeight = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    NetWeight = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    HotSheetShiptId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoHotSheetPacking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetPacking_DensoHotSheet_HotSheetShiptId",
                        column: x => x.HotSheetShiptId,
                        principalTable: "DensoHotSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetPacking_HotSheetShiptId",
                table: "DensoHotSheetPacking",
                column: "HotSheetShiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheetShipProducts_DensoCountries_OriginCountryId",
                table: "DensoHotSheetShipProducts",
                column: "OriginCountryId",
                principalTable: "DensoCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheetShipProducts_DensoUnitMeasures_UnitMeasureId",
                table: "DensoHotSheetShipProducts",
                column: "UnitMeasureId",
                principalTable: "DensoUnitMeasures",
                principalColumn: "Id");
        }
    }
}
