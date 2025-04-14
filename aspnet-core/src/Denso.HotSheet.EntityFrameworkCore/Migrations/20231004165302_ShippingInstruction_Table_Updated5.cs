using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class HotSheet_Table_Updated5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExportedCigma",
                table: "DensoHotSheet");

            migrationBuilder.AddColumn<int>(
                name: "ExportedCigmaStatus",
                table: "DensoHotSheet",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExportedCigmaStatus",
                table: "DensoHotSheet");

            migrationBuilder.AddColumn<bool>(
                name: "ExportedCigma",
                table: "DensoHotSheet",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
