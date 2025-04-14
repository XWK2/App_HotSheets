using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_CustomerPlants_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerPlantContactId",
                table: "DensoHotSheet",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DensoCustomerPlantContacts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerPlantId = table.Column<long>(type: "bigint", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DepartmentOrSection = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NetNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoCustomerPlantContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoCustomerPlantContacts_DensoCustomerPlants_CustomerPlantId",
                        column: x => x.CustomerPlantId,
                        principalTable: "DensoCustomerPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_CustomerPlantContactId",
                table: "DensoHotSheet",
                column: "CustomerPlantContactId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoCustomerPlantContacts_CustomerPlantId",
                table: "DensoCustomerPlantContacts",
                column: "CustomerPlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheet_DensoCustomerPlantContacts_CustomerPlantContactId",
                table: "DensoHotSheet",
                column: "CustomerPlantContactId",
                principalTable: "DensoCustomerPlantContacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheet_DensoCustomerPlantContacts_CustomerPlantContactId",
                table: "DensoHotSheet");

            migrationBuilder.DropTable(
                name: "DensoCustomerPlantContacts");

            migrationBuilder.DropIndex(
                name: "IX_DensoHotSheet_CustomerPlantContactId",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "CustomerPlantContactId",
                table: "DensoHotSheet");
        }
    }
}
