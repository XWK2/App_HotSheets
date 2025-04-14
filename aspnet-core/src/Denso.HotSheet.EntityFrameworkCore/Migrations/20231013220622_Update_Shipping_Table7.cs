using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_Shipping_Table7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OnBehalfOfDeptoId",
                table: "DensoHotSheet",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnBehalfOfExt",
                table: "DensoHotSheet",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OnBehalfOfUserId",
                table: "DensoHotSheet",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnBehalfOfDeptoId",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "OnBehalfOfExt",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "OnBehalfOfUserId",
                table: "DensoHotSheet");
        }
    }
}
