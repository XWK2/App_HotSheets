using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_CustomersEmployees_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DensoEmployeeId",
                table: "DensoEmployees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DensoCustomerId",
                table: "DensoCustomers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DensoEmployees_DensoEmployeeId_Credential",
                table: "DensoEmployees",
                columns: new[] { "DensoEmployeeId", "Credential" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DensoCustomers_DensoCustomerId_Payment",
                table: "DensoCustomers",
                columns: new[] { "DensoCustomerId", "Payment" },
                unique: true,
                filter: "[DensoCustomerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DensoEmployees_DensoEmployeeId_Credential",
                table: "DensoEmployees");

            migrationBuilder.DropIndex(
                name: "IX_DensoCustomers_DensoCustomerId_Payment",
                table: "DensoCustomers");

            migrationBuilder.DropColumn(
                name: "DensoEmployeeId",
                table: "DensoEmployees");

            migrationBuilder.DropColumn(
                name: "DensoCustomerId",
                table: "DensoCustomers");
        }
    }
}
