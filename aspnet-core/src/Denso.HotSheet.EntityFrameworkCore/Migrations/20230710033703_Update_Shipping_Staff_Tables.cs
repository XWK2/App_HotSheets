using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_Shipping_Staff_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheet_DensoIEStaff_IEStaffId",
                table: "DensoHotSheet");

            migrationBuilder.DropIndex(
                name: "IX_DensoHotSheet_IEStaffId",
                table: "DensoHotSheet");

            migrationBuilder.AddColumn<long>(
                name: "LeadUserId",
                table: "DensoIEStaff",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LeadUserId",
                table: "DensoAccountingStaff",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeadUserId",
                table: "DensoIEStaff");

            migrationBuilder.DropColumn(
                name: "LeadUserId",
                table: "DensoAccountingStaff");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_IEStaffId",
                table: "DensoHotSheet",
                column: "IEStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheet_DensoIEStaff_IEStaffId",
                table: "DensoHotSheet",
                column: "IEStaffId",
                principalTable: "DensoIEStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
