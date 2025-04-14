using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Update_Staff_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensoAccountingStaffCustomers");

            migrationBuilder.DropTable(
                name: "DensoAccountingStaffUsers");

            migrationBuilder.DropTable(
                name: "DensoIEStaffCustomers");

            migrationBuilder.DropTable(
                name: "DensoIEStaffUsers");

            migrationBuilder.DropTable(
                name: "DensoAccountingStaff");

            migrationBuilder.DropTable(
                name: "DensoIEStaff");

            migrationBuilder.CreateTable(
                name: "DensoStaff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoStaff_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoStaffUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoStaffUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoStaffUsers_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DensoStaffUsers_DensoStaff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "DensoStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoStaff_UserId",
                table: "DensoStaff",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DensoStaffUsers_StaffId",
                table: "DensoStaffUsers",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoStaffUsers_UserId",
                table: "DensoStaffUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensoStaffUsers");

            migrationBuilder.DropTable(
                name: "DensoStaff");

            migrationBuilder.CreateTable(
                name: "DensoAccountingStaff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    LeadUserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoAccountingStaff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoIEStaff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    LeadUserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoIEStaff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoAccountingStaffCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    AccountingStaffId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoAccountingStaffCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoAccountingStaffCustomers_DensoAccountingStaff_AccountingStaffId",
                        column: x => x.AccountingStaffId,
                        principalTable: "DensoAccountingStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoAccountingStaffCustomers_DensoCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "DensoCustomers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DensoAccountingStaffUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    AccountingStaffId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoAccountingStaffUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoAccountingStaffUsers_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DensoAccountingStaffUsers_DensoAccountingStaff_AccountingStaffId",
                        column: x => x.AccountingStaffId,
                        principalTable: "DensoAccountingStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoIEStaffCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    IEStaffId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoIEStaffCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoIEStaffCustomers_DensoCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "DensoCustomers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DensoIEStaffCustomers_DensoIEStaff_IEStaffId",
                        column: x => x.IEStaffId,
                        principalTable: "DensoIEStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoIEStaffUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    IEStaffId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoIEStaffUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoIEStaffUsers_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DensoIEStaffUsers_DensoIEStaff_IEStaffId",
                        column: x => x.IEStaffId,
                        principalTable: "DensoIEStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DensoAccountingStaffCustomers_AccountingStaffId",
                table: "DensoAccountingStaffCustomers",
                column: "AccountingStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoAccountingStaffCustomers_CustomerId",
                table: "DensoAccountingStaffCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoAccountingStaffUsers_AccountingStaffId",
                table: "DensoAccountingStaffUsers",
                column: "AccountingStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoAccountingStaffUsers_UserId",
                table: "DensoAccountingStaffUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoIEStaffCustomers_CustomerId",
                table: "DensoIEStaffCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoIEStaffCustomers_IEStaffId",
                table: "DensoIEStaffCustomers",
                column: "IEStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoIEStaffUsers_IEStaffId",
                table: "DensoIEStaffUsers",
                column: "IEStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoIEStaffUsers_UserId",
                table: "DensoIEStaffUsers",
                column: "UserId");
        }
    }
}
