using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_Survey_Table_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswerQuestion4",
                table: "DensoSurveys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerQuestion5",
                table: "DensoSurveys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerQuestion6",
                table: "DensoSurveys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnswerQuestion7",
                table: "DensoSurveys",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerQuestion4",
                table: "DensoSurveys");

            migrationBuilder.DropColumn(
                name: "AnswerQuestion5",
                table: "DensoSurveys");

            migrationBuilder.DropColumn(
                name: "AnswerQuestion6",
                table: "DensoSurveys");

            migrationBuilder.DropColumn(
                name: "AnswerQuestion7",
                table: "DensoSurveys");
        }
    }
}
