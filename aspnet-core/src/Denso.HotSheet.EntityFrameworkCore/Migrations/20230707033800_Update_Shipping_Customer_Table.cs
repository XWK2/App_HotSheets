using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_Shipping_Customer_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "DensoCustomers",
                newName: "Contact");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DensoCustomers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FedexCta",
                table: "DensoCustomers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "DensoCustomers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "DensoCustomers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FedexCta",
                table: "DensoCustomers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "DensoCustomers");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "DensoCustomers");

            migrationBuilder.RenameColumn(
                name: "Contact",
                table: "DensoCustomers",
                newName: "FullName");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DensoCustomers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);
        }
    }
}
