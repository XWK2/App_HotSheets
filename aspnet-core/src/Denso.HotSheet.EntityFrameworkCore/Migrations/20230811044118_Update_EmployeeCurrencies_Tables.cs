using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_EmployeeCurrencies_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DensoEmployees_DensoEmployeeId_Credential",
                table: "DensoEmployees");

            migrationBuilder.AlterColumn<string>(
                name: "Credential",
                table: "DensoEmployees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "DensoCurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DensoCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoCurrencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoEmployees_DensoEmployeeId_Credential",
                table: "DensoEmployees",
                columns: new[] { "DensoEmployeeId", "Credential" },
                unique: true,
                filter: "[Credential] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensoCurrencies");

            migrationBuilder.DropIndex(
                name: "IX_DensoEmployees_DensoEmployeeId_Credential",
                table: "DensoEmployees");

            migrationBuilder.AlterColumn<long>(
                name: "Credential",
                table: "DensoEmployees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DensoEmployees_DensoEmployeeId_Credential",
                table: "DensoEmployees",
                columns: new[] { "DensoEmployeeId", "Credential" },
                unique: true);
        }
    }
}
