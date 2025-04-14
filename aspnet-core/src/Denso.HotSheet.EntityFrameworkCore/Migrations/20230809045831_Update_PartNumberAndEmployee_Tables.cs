using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_PartNumberAndEmployee_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumbers_DensoCountries_OriginCountryId",
                table: "DensoPartNumbers");

            migrationBuilder.DropIndex(
                name: "IX_DensoPartNumbers_OriginCountryId",
                table: "DensoPartNumbers");

            migrationBuilder.AddColumn<string>(
                name: "OriginCountry",
                table: "DensoPartNumbers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoEmployeeTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "DensoEmployees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoEmployeePositions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoEmployeeLevels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DensoPartNumbersInternal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DescriptionSpanish = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    UnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    ProductCodeSATId = table.Column<long>(type: "bigint", nullable: true),
                    OriginCountryId = table.Column<int>(type: "int", nullable: true),
                    OriginCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Fraction = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPartNumbersInternal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoPartNumbersInternal_DensoProductCodesSAT_ProductCodeSATId",
                        column: x => x.ProductCodeSATId,
                        principalTable: "DensoProductCodesSAT",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DensoPartNumbersInternal_DensoUnitMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "DensoUnitMeasures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DensoPartNumberPricesInternal",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    PartNumberInternalId = table.Column<long>(type: "bigint", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PublishDate = table.Column<DateTime>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPartNumberPricesInternal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoPartNumberPricesInternal_DensoCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "DensoCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DensoPartNumberPricesInternal_DensoPartNumbersInternal_PartNumberInternalId",
                        column: x => x.PartNumberInternalId,
                        principalTable: "DensoPartNumbersInternal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoPartNumberPricesInternal_CustomerId",
                table: "DensoPartNumberPricesInternal",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPartNumberPricesInternal_PartNumberInternalId",
                table: "DensoPartNumberPricesInternal",
                column: "PartNumberInternalId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPartNumbersInternal_ProductCodeSATId",
                table: "DensoPartNumbersInternal",
                column: "ProductCodeSATId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPartNumbersInternal_UnitMeasureId",
                table: "DensoPartNumbersInternal",
                column: "UnitMeasureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensoPartNumberPricesInternal");

            migrationBuilder.DropTable(
                name: "DensoPartNumbersInternal");

            migrationBuilder.DropColumn(
                name: "OriginCountry",
                table: "DensoPartNumbers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoEmployeeTypes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "DensoEmployees");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoEmployeePositions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoEmployeeLevels");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPartNumbers_OriginCountryId",
                table: "DensoPartNumbers",
                column: "OriginCountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumbers_DensoCountries_OriginCountryId",
                table: "DensoPartNumbers",
                column: "OriginCountryId",
                principalTable: "DensoCountries",
                principalColumn: "Id");
        }
    }
}
