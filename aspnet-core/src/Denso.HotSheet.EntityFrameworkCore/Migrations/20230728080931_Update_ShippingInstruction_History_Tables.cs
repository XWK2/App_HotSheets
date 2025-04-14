using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_HotSheet_History_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountingApprovalDate",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "AccountingApprovalId",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "AccountingComments",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "Folio",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "IEStaffApprovalDate",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "IEStaffComments",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "IEStaffId",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "ManagerApprovalDate",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "ManagerApprovalId",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "ManagerComments",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "SentForApprovalBy",
                table: "DensoHotSheet");

            migrationBuilder.DropColumn(
                name: "SentForApprovalDate",
                table: "DensoHotSheet");

            migrationBuilder.CreateTable(
                name: "DensoHotSheetApprovals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotSheetShiptId = table.Column<long>(type: "bigint", nullable: false),
                    IEStaffId = table.Column<long>(type: "bigint", nullable: true),
                    IEStaffIsApproved = table.Column<bool>(type: "bit", nullable: true),
                    IEStaffApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ManagerApprovalId = table.Column<long>(type: "bigint", nullable: true),
                    ManagerIsApproved = table.Column<bool>(type: "bit", nullable: true),
                    ManagerApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountingApprovalId = table.Column<long>(type: "bigint", nullable: true),
                    AccountingIsApproved = table.Column<bool>(type: "bit", nullable: true),
                    AccountingApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoHotSheetApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetApprovals_AbpUsers_ManagerApprovalId",
                        column: x => x.ManagerApprovalId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DensoHotSheetApprovals_DensoHotSheet_HotSheetShiptId",
                        column: x => x.HotSheetShiptId,
                        principalTable: "DensoHotSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetApprovals_DensoStaff_AccountingApprovalId",
                        column: x => x.AccountingApprovalId,
                        principalTable: "DensoStaff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DensoHotSheetApprovals_DensoStaff_IEStaffId",
                        column: x => x.IEStaffId,
                        principalTable: "DensoStaff",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DensoHotSheetHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotSheetShiptId = table.Column<long>(type: "bigint", nullable: false),
                    HistoryType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoHotSheetHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetHistory_DensoHotSheet_HotSheetShiptId",
                        column: x => x.HotSheetShiptId,
                        principalTable: "DensoHotSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetApprovals_AccountingApprovalId",
                table: "DensoHotSheetApprovals",
                column: "AccountingApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetApprovals_IEStaffId",
                table: "DensoHotSheetApprovals",
                column: "IEStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetApprovals_ManagerApprovalId",
                table: "DensoHotSheetApprovals",
                column: "ManagerApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetApprovals_HotSheetShiptId",
                table: "DensoHotSheetApprovals",
                column: "HotSheetShiptId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetHistory_HotSheetShiptId",
                table: "DensoHotSheetHistory",
                column: "HotSheetShiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensoHotSheetApprovals");

            migrationBuilder.DropTable(
                name: "DensoHotSheetHistory");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountingApprovalDate",
                table: "DensoHotSheet",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AccountingApprovalId",
                table: "DensoHotSheet",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountingComments",
                table: "DensoHotSheet",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Folio",
                table: "DensoHotSheet",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IEStaffApprovalDate",
                table: "DensoHotSheet",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IEStaffComments",
                table: "DensoHotSheet",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IEStaffId",
                table: "DensoHotSheet",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ManagerApprovalDate",
                table: "DensoHotSheet",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ManagerApprovalId",
                table: "DensoHotSheet",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerComments",
                table: "DensoHotSheet",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SentForApprovalBy",
                table: "DensoHotSheet",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentForApprovalDate",
                table: "DensoHotSheet",
                type: "datetime2",
                nullable: true);
        }
    }
}
