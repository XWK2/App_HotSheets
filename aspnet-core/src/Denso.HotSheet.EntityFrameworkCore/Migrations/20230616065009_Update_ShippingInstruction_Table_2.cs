using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_HotSheet_Table_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DensoHotSheet_StatusId",
                table: "DensoHotSheet");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_StatusId",
                table: "DensoHotSheet",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DensoHotSheet_StatusId",
                table: "DensoHotSheet");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_StatusId",
                table: "DensoHotSheet",
                column: "StatusId",
                unique: true);
        }
    }
}
