using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Create_Manifests_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DensoHotSheetShipManifests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotSheetShiptId = table.Column<long>(type: "bigint", nullable: true),
                    Manifest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManifestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Report = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoHotSheetShipManifests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetShipManifests_DensoHotSheet_HotSheetShiptId",
                        column: x => x.HotSheetShiptId,
                        principalTable: "DensoHotSheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetShipManifests_HotSheetShiptId",
                table: "DensoHotSheetShipManifests",
                column: "HotSheetShiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensoHotSheetShipManifests");
        }
    }
}
