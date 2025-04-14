using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Add_Surveys_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DensoSurveys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Page = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AnswerQuestion1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerQuestion2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerQuestion3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HotSheet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoSurveys", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensoSurveys");
        }
    }
}
