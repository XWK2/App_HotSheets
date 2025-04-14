using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_Employees_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoEmployees_AbpUsers_UserId",
                table: "DensoEmployees");

            migrationBuilder.DropIndex(
                name: "IX_DensoEmployees_UserId",
                table: "DensoEmployees");

            migrationBuilder.DropColumn(
                name: "PositionCode",
                table: "DensoEmployees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DensoEmployees");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "DensoEmployees",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "EmployeeType",
                table: "DensoEmployees",
                newName: "PositionId");

            migrationBuilder.AlterColumn<long>(
                name: "PlantId",
                table: "DensoEmployees",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "DensoEmployees",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DensoEmployees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "DensoEmployees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "AbpUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DensoEmployeeLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoEmployeeLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoEmployeePositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoEmployeePositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoEmployeeTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoEmployeeTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoEmployees_DepartmentId",
                table: "DensoEmployees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoEmployees_LevelId",
                table: "DensoEmployees",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoEmployees_PlantId",
                table: "DensoEmployees",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoEmployees_PositionId",
                table: "DensoEmployees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoEmployees_TypeId",
                table: "DensoEmployees",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_EmployeeId",
                table: "AbpUsers",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_DensoEmployees_EmployeeId",
                table: "AbpUsers",
                column: "EmployeeId",
                principalTable: "DensoEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoEmployees_DensoDepartments_DepartmentId",
                table: "DensoEmployees",
                column: "DepartmentId",
                principalTable: "DensoDepartments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoEmployees_DensoEmployeeLevels_LevelId",
                table: "DensoEmployees",
                column: "LevelId",
                principalTable: "DensoEmployeeLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoEmployees_DensoEmployeePositions_PositionId",
                table: "DensoEmployees",
                column: "PositionId",
                principalTable: "DensoEmployeePositions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoEmployees_DensoEmployeeTypes_TypeId",
                table: "DensoEmployees",
                column: "TypeId",
                principalTable: "DensoEmployeeTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoEmployees_DensoPlants_PlantId",
                table: "DensoEmployees",
                column: "PlantId",
                principalTable: "DensoPlants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_DensoEmployees_EmployeeId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoEmployees_DensoDepartments_DepartmentId",
                table: "DensoEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoEmployees_DensoEmployeeLevels_LevelId",
                table: "DensoEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoEmployees_DensoEmployeePositions_PositionId",
                table: "DensoEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoEmployees_DensoEmployeeTypes_TypeId",
                table: "DensoEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoEmployees_DensoPlants_PlantId",
                table: "DensoEmployees");

            migrationBuilder.DropTable(
                name: "DensoEmployeeLevels");

            migrationBuilder.DropTable(
                name: "DensoEmployeePositions");

            migrationBuilder.DropTable(
                name: "DensoEmployeeTypes");

            migrationBuilder.DropIndex(
                name: "IX_DensoEmployees_DepartmentId",
                table: "DensoEmployees");

            migrationBuilder.DropIndex(
                name: "IX_DensoEmployees_LevelId",
                table: "DensoEmployees");

            migrationBuilder.DropIndex(
                name: "IX_DensoEmployees_PlantId",
                table: "DensoEmployees");

            migrationBuilder.DropIndex(
                name: "IX_DensoEmployees_PositionId",
                table: "DensoEmployees");

            migrationBuilder.DropIndex(
                name: "IX_DensoEmployees_TypeId",
                table: "DensoEmployees");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_EmployeeId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "DensoEmployees");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DensoEmployees");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "DensoEmployees");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AbpUsers");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "DensoEmployees",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "DensoEmployees",
                newName: "EmployeeType");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "DensoEmployees",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PositionCode",
                table: "DensoEmployees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "DensoEmployees",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DensoEmployees_UserId",
                table: "DensoEmployees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoEmployees_AbpUsers_UserId",
                table: "DensoEmployees",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }
    }
}
