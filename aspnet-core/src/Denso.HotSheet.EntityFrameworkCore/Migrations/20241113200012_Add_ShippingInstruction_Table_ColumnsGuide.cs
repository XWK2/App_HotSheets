using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Add_HotSheet_Table_ColumnsGuide : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GuideCost",
                table: "DensoHotSheet",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "GuideCurrency",
                table: "DensoHotSheet",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);            

            migrationBuilder.AddColumn<string>(
                name: "GuideReference",
                table: "DensoHotSheet",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuideStatus",
                table: "DensoHotSheet",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuideStatusDetail",
                table: "DensoHotSheet",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuideCost",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "GuideCurrency",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "GuideReference",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "GuideStatus",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "GuideStatusDetail",
                table: "DensoHotSheet");
        }
    }
}
