using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_HotSheet_Table_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RMAAssigned",
                table: "DensoHotSheet",
                newName: "HotSheetTermId");

            migrationBuilder.AddColumn<int>(
                name: "RmaAssignmentId",
                table: "DensoHotSheet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_RmaAssignmentId",
                table: "DensoHotSheet",
                column: "RmaAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_HotSheetTermId",
                table: "DensoHotSheet",
                column: "HotSheetTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheet_DensoRMAAssignments_RmaAssignmentId",
                table: "DensoHotSheet",
                column: "RmaAssignmentId",
                principalTable: "DensoRMAAssignments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoHotSheet_DensoHotSheetTerms_HotSheetTermId",
                table: "DensoHotSheet",
                column: "HotSheetTermId",
                principalTable: "DensoHotSheetTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheet_DensoRMAAssignments_RmaAssignmentId",
                table: "DensoHotSheet");

            migrationBuilder.DropForeignKey(
                name: "FK_DensoHotSheet_DensoHotSheetTerms_HotSheetTermId",
                table: "DensoHotSheet");

            migrationBuilder.DropIndex(
                name: "IX_DensoHotSheet_RmaAssignmentId",
                table: "DensoHotSheet");

            migrationBuilder.DropIndex(
                name: "IX_DensoHotSheet_HotSheetTermId",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "RmaAssignmentId",
                table: "DensoHotSheet");

            migrationBuilder.RenameColumn(
                name: "HotSheetTermId",
                table: "DensoHotSheet",
                newName: "RMAAssigned");
        }
    }
}
