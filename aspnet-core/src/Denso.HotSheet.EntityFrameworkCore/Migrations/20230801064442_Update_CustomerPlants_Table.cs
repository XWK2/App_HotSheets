using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_CustomerPlants_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "DensoCustomerPlants",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "DensoCustomerPlants",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxId",
                table: "DensoCustomerPlants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "DensoCustomerPlants",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "DensoCustomerPlants");

            migrationBuilder.DropColumn(
                name: "State",
                table: "DensoCustomerPlants");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "DensoCustomerPlants");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "DensoCustomerPlants");
        }
    }
}
