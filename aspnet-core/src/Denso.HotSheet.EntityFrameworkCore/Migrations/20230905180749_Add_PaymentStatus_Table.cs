using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Add_PaymentStatus_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "DensoHotSheet");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatusId",
                table: "DensoHotSheet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DensoPaymentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPaymentStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_PaymentStatusId",
                table: "DensoHotSheet",
                column: "PaymentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheet_DensoPaymentStatus_PaymentStatusId",
                table: "DensoHotSheet",
                column: "PaymentStatusId",
                principalTable: "DensoPaymentStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheet_DensoPaymentStatus_PaymentStatusId",
                table: "DensoHotSheet");

            migrationBuilder.DropTable(
                name: "DensoPaymentStatus");

            migrationBuilder.DropIndex(
                name: "IX_DensoHotSheet_PaymentStatusId",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "PaymentStatusId",
                table: "DensoHotSheet");

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "DensoHotSheet",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
