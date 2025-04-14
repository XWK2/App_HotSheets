using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_HotSheets_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Warning1CompanyId",
                table: "DensoPaymentTerms");

            migrationBuilder.DropColumn(
                name: "Warning2CompanyId",
                table: "DensoPaymentTerms");

            migrationBuilder.AddColumn<long>(
                name: "FreightPaidByDepartmentId",
                table: "DensoHotSheet",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FreightPaidByOther",
                table: "DensoHotSheet",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Warning1CompanyIds",
                table: "DensoPaymentTerms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Warning2CompanyIds",
                table: "DensoPaymentTerms",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreightPaidByDepartmentId",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "FreightPaidByOther",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "Warning1CompanyIds",
                table: "DensoPaymentTerms");

            migrationBuilder.DropColumn(
                name: "Warning2CompanyIds",
                table: "DensoPaymentTerms");

            migrationBuilder.AddColumn<int>(
                name: "Warning1CompanyId",
                table: "DensoPaymentTerms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Warning2CompanyId",
                table: "DensoPaymentTerms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
