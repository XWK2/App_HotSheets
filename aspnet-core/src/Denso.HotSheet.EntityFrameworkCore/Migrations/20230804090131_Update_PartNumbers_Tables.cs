using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_PartNumbers_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UnitMeasureId",
                table: "DensoPartNumbers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPartNumbers_UnitMeasureId",
                table: "DensoPartNumbers",
                column: "UnitMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoPartNumbers_DensoUnitMeasures_UnitMeasureId",
                table: "DensoPartNumbers",
                column: "UnitMeasureId",
                principalTable: "DensoUnitMeasures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoPartNumbers_DensoUnitMeasures_UnitMeasureId",
                table: "DensoPartNumbers");

            migrationBuilder.DropIndex(
                name: "IX_DensoPartNumbers_UnitMeasureId",
                table: "DensoPartNumbers");

            migrationBuilder.AlterColumn<int>(
                name: "UnitMeasureId",
                table: "DensoPartNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
