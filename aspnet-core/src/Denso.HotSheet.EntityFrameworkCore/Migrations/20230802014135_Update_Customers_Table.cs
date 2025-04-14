using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_Customers_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zip",
                table: "DensoCustomers");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "DensoCustomers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "DensoCustomers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxId",
                table: "DensoCustomers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "DensoCustomers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "DensoCustomers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "DensoCustomers");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "DensoCustomers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "DensoCustomers");

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "DensoCustomers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
