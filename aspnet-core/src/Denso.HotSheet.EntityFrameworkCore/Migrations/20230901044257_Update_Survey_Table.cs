using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_Survey_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotSheet",
                table: "DensoSurveys");

            migrationBuilder.AddColumn<long>(
                name: "HotSheetShiptId",
                table: "DensoSurveys",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DensoSurveys_HotSheetShiptId",
                table: "DensoSurveys",
                column: "HotSheetShiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_DensoSurveys_DensoHotSheet_HotSheetShiptId",
                table: "DensoSurveys",
                column: "HotSheetShiptId",
                principalTable: "DensoHotSheet",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DensoSurveys_DensoHotSheet_HotSheetShiptId",
                table: "DensoSurveys");

            migrationBuilder.DropIndex(
                name: "IX_DensoSurveys_HotSheetShiptId",
                table: "DensoSurveys");

            migrationBuilder.DropColumn(
                name: "HotSheetShiptId",
                table: "DensoSurveys");

            migrationBuilder.AddColumn<string>(
                name: "HotSheet",
                table: "DensoSurveys",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
