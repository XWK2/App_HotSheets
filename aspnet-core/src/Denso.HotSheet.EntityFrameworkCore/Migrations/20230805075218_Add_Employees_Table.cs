using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Add_Employees_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "DensoPlants",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine1",
                table: "DensoPlants",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DensoEmployees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Credential = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Surnames = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Rfc = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    Nss = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Curp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    EmployeeType = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    PlantId = table.Column<int>(type: "int", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "date", nullable: false),
                    PositionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extras = table.Column<bool>(type: "bit", nullable: false),
                    NotRequiredAHE = table.Column<bool>(type: "bit", nullable: false),
                    Supervisor = table.Column<bool>(type: "bit", nullable: false),
                    Subsidy = table.Column<bool>(type: "bit", nullable: false),
                    PositionLevel = table.Column<bool>(type: "bit", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoEmployees_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoEmployees_UserId",
                table: "DensoEmployees",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensoEmployees");

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine2",
                table: "DensoPlants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine1",
                table: "DensoPlants",
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
