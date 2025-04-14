using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Denso.HotSheet.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Denso_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DensoAccountingStaff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoAccountingStaff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoCountries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoDepartments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoDivisions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoDivisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoDocumentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoDocumentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoDocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoDocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoIEStaff",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoIEStaff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoPaidBy",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPaidBy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoPartNumbers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Description1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescriptionSpanish1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescriptionSpanish2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UnitMeasure = table.Column<int>(type: "int", nullable: false),
                    ProductKeySAT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Fraction = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPartNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoPaymentTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    AlwaysDnmx = table.Column<bool>(type: "bit", nullable: false),
                    AccountingApprovalRequired = table.Column<bool>(type: "bit", nullable: false),
                    ExcludeOnSamples = table.Column<bool>(type: "bit", nullable: false),
                    Warning1CompanyId = table.Column<int>(type: "int", nullable: false),
                    Warning1Message = table.Column<int>(type: "int", nullable: false),
                    Warning2CompanyId = table.Column<int>(type: "int", nullable: false),
                    Warning2Message = table.Column<int>(type: "int", nullable: false),
                    Warning2Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    POWarning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPaymentTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoPlants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Sufix = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPlants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoProductCodesSAT",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoProductCodesSAT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoRMAAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoRMAAssignments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoServices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsNational = table.Column<bool>(type: "bit", nullable: false),
                    IsInternational = table.Column<bool>(type: "bit", nullable: false),
                    ShowHigestCostWarning = table.Column<bool>(type: "bit", nullable: false),
                    Ground = table.Column<bool>(type: "bit", nullable: false),
                    Air = table.Column<bool>(type: "bit", nullable: false),
                    Sea = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoHotSheetReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BNoticeRMARequired = table.Column<bool>(type: "bit", nullable: false),
                    PictureTechnicalInfoMakerModelSerialNumber = table.Column<bool>(type: "bit", nullable: false),
                    AttachPurchaseOrder = table.Column<bool>(type: "bit", nullable: false),
                    TechnicalInfoPicture = table.Column<bool>(type: "bit", nullable: false),
                    AccountingApprovalRequired = table.Column<bool>(type: "bit", nullable: false),
                    ExcludeTermOfPayment = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoHotSheetReasons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoHotSheetTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoHotSheetTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoSuppliers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Rfc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FedexCta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoSuppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoUnitMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoUnitMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DensoAccountingStaffUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountingStaffId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
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
                name: "DensoAccountingStaffCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountingStaffId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
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
                name: "DensoCustomerPlants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoCustomerPlants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoCustomerPlants_DensoCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "DensoCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoDepartmentUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsApprover = table.Column<bool>(type: "bit", nullable: false),
                    IsSupervisor = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoDepartmentUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoDepartmentUsers_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoDepartmentUsers_DensoDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "DensoDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoCarriers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    DivisorNumber = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoCarriers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoCarriers_DensoDocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DensoDocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoIEStaffCustomers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IEStaffId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
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
                    IEStaffId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "DensoPlantUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IsSupervisor = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPlantUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoPlantUsers_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoPlantUsers_DensoPlants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "DensoPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoSupplierAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressLine4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoSupplierAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoSupplierAddresses_DensoSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "DensoSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoSupplierBrokers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoSupplierBrokers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoSupplierBrokers_DensoSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "DensoSuppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DensoUnitMeasuresSAT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoUnitMeasuresSAT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoUnitMeasuresSAT_DensoUnitMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "DensoUnitMeasures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DensoCarrierService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoCarrierService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoCarrierService_DensoCarriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "DensoCarriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoCarrierService_DensoServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "DensoServices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DensoPaymentTermCarriers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentTermId = table.Column<int>(type: "int", nullable: false),
                    CarrierId = table.Column<long>(type: "bigint", nullable: false),
                    WarningType = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoPaymentTermCarriers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoPaymentTermCarriers_DensoCarriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "DensoCarriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DensoPaymentTermCarriers_DensoPaymentTerms_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "DensoPaymentTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoHotSheet",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    Folio = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProformaInvoice = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TrackingNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CarrierId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    HotSheetReasonId = table.Column<int>(type: "int", nullable: false),
                    AdditionalExplanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTermId = table.Column<int>(type: "int", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    PlantId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerPlantId = table.Column<long>(type: "bigint", nullable: false),
                    RMAAssigned = table.Column<int>(type: "int", nullable: false),
                    OtherBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RMANumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BNotice = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CostPaidById = table.Column<long>(type: "bigint", nullable: false),
                    FreightPaidById = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    TelephoneExt = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ShowBehalfFields = table.Column<bool>(type: "bit", nullable: false),
                    IEStaffId = table.Column<long>(type: "bigint", nullable: true),
                    IEStaffApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IEStaffComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerApprovalId = table.Column<long>(type: "bigint", nullable: true),
                    ManagerApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ManagerComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountingApprovalId = table.Column<long>(type: "bigint", nullable: true),
                    AccountingApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountingComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    IsTemplate = table.Column<bool>(type: "bit", nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoHotSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoCarriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "DensoCarriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoCustomerPlants_CustomerPlantId",
                        column: x => x.CustomerPlantId,
                        principalTable: "DensoCustomerPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "DensoCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoDepartments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "DensoDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoDocumentStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DensoDocumentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoDocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DensoDocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoIEStaff_IEStaffId",
                        column: x => x.IEStaffId,
                        principalTable: "DensoIEStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoPaidBy_CostPaidById",
                        column: x => x.CostPaidById,
                        principalTable: "DensoPaidBy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoPaidBy_FreightPaidById",
                        column: x => x.FreightPaidById,
                        principalTable: "DensoPaidBy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoPaymentTerms_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "DensoPaymentTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoPlants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "DensoPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "DensoServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheet_DensoHotSheetReasons_HotSheetReasonId",
                        column: x => x.HotSheetReasonId,
                        principalTable: "DensoHotSheetReasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoCigmaExports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    HotSheetShiptId = table.Column<long>(type: "bigint", nullable: false),
                    ExportedToCigma = table.Column<bool>(type: "bit", nullable: false),
                    ExportedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DetailExported = table.Column<bool>(type: "bit", nullable: false),
                    ErrorOnExport = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoCigmaExports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoCigmaExports_DensoHotSheet_HotSheetShiptId",
                        column: x => x.HotSheetShiptId,
                        principalTable: "DensoHotSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoHotSheetPacking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotSheetShiptId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DimensionLL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionWA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DimensionHA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    NetWeight = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    GrossWeight = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoHotSheetPacking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetPacking_DensoHotSheet_HotSheetShiptId",
                        column: x => x.HotSheetShiptId,
                        principalTable: "DensoHotSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DensoHotSheetShipProducts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotSheetShiptId = table.Column<long>(type: "bigint", nullable: false),
                    PartNumberId = table.Column<long>(type: "bigint", nullable: false),
                    UnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionSpanish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCodeSATId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PoNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginCountryId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DensoHotSheetShipProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetShipProducts_DensoCountries_OriginCountryId",
                        column: x => x.OriginCountryId,
                        principalTable: "DensoCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetShipProducts_DensoPartNumbers_PartNumberId",
                        column: x => x.PartNumberId,
                        principalTable: "DensoPartNumbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetShipProducts_DensoProductCodesSAT_ProductCodeSATId",
                        column: x => x.ProductCodeSATId,
                        principalTable: "DensoProductCodesSAT",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DensoHotSheetShipProducts_DensoHotSheet_HotSheetShiptId",
                        column: x => x.HotSheetShiptId,
                        principalTable: "DensoHotSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DensoHotSheetShipProducts_DensoUnitMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "DensoUnitMeasures",
                        principalColumn: "Id");
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
                name: "IX_DensoCarriers_DocumentTypeId",
                table: "DensoCarriers",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoCarrierService_CarrierId",
                table: "DensoCarrierService",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoCarrierService_ServiceId",
                table: "DensoCarrierService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoCigmaExports_HotSheetShiptId",
                table: "DensoCigmaExports",
                column: "HotSheetShiptId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoCustomerPlants_CustomerId",
                table: "DensoCustomerPlants",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoDepartmentUsers_DepartmentId",
                table: "DensoDepartmentUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoDepartmentUsers_UserId",
                table: "DensoDepartmentUsers",
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

            migrationBuilder.CreateIndex(
                name: "IX_DensoPaymentTermCarriers_CarrierId",
                table: "DensoPaymentTermCarriers",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPaymentTermCarriers_PaymentTermId",
                table: "DensoPaymentTermCarriers",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPlantUsers_PlantId",
                table: "DensoPlantUsers",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoPlantUsers_UserId",
                table: "DensoPlantUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetPacking_HotSheetShiptId",
                table: "DensoHotSheetPacking",
                column: "HotSheetShiptId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetShipProducts_OriginCountryId",
                table: "DensoHotSheetShipProducts",
                column: "OriginCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetShipProducts_PartNumberId",
                table: "DensoHotSheetShipProducts",
                column: "PartNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetShipProducts_ProductCodeSATId",
                table: "DensoHotSheetShipProducts",
                column: "ProductCodeSATId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetShipProducts_HotSheetShiptId",
                table: "DensoHotSheetShipProducts",
                column: "HotSheetShiptId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheetShipProducts_UnitMeasureId",
                table: "DensoHotSheetShipProducts",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_CarrierId",
                table: "DensoHotSheet",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_CostPaidById",
                table: "DensoHotSheet",
                column: "CostPaidById");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_CustomerId",
                table: "DensoHotSheet",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_CustomerPlantId",
                table: "DensoHotSheet",
                column: "CustomerPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_DepartmentId",
                table: "DensoHotSheet",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_DocumentTypeId",
                table: "DensoHotSheet",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_FreightPaidById",
                table: "DensoHotSheet",
                column: "FreightPaidById");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_IEStaffId",
                table: "DensoHotSheet",
                column: "IEStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_PaymentTermId",
                table: "DensoHotSheet",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_PlantId",
                table: "DensoHotSheet",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_ServiceId",
                table: "DensoHotSheet",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_HotSheetReasonId",
                table: "DensoHotSheet",
                column: "HotSheetReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoHotSheet_StatusId",
                table: "DensoHotSheet",
                column: "StatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DensoSupplierAddresses_SupplierId",
                table: "DensoSupplierAddresses",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoSupplierBrokers_SupplierId",
                table: "DensoSupplierBrokers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_DensoUnitMeasuresSAT_UnitMeasureId",
                table: "DensoUnitMeasuresSAT",
                column: "UnitMeasureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DensoAccountingStaffCustomers");

            migrationBuilder.DropTable(
                name: "DensoAccountingStaffUsers");

            migrationBuilder.DropTable(
                name: "DensoCarrierService");

            migrationBuilder.DropTable(
                name: "DensoCigmaExports");

            migrationBuilder.DropTable(
                name: "DensoDepartmentUsers");

            migrationBuilder.DropTable(
                name: "DensoDivisions");

            migrationBuilder.DropTable(
                name: "DensoIEStaffCustomers");

            migrationBuilder.DropTable(
                name: "DensoIEStaffUsers");

            migrationBuilder.DropTable(
                name: "DensoPaymentTermCarriers");

            migrationBuilder.DropTable(
                name: "DensoPlantUsers");

            migrationBuilder.DropTable(
                name: "DensoRMAAssignments");

            migrationBuilder.DropTable(
                name: "DensoHotSheetTerms");

            migrationBuilder.DropTable(
                name: "DensoHotSheetPacking");

            migrationBuilder.DropTable(
                name: "DensoHotSheetShipProducts");

            migrationBuilder.DropTable(
                name: "DensoSupplierAddresses");

            migrationBuilder.DropTable(
                name: "DensoSupplierBrokers");

            migrationBuilder.DropTable(
                name: "DensoUnitMeasuresSAT");

            migrationBuilder.DropTable(
                name: "DensoAccountingStaff");

            migrationBuilder.DropTable(
                name: "DensoCountries");

            migrationBuilder.DropTable(
                name: "DensoPartNumbers");

            migrationBuilder.DropTable(
                name: "DensoProductCodesSAT");

            migrationBuilder.DropTable(
                name: "DensoHotSheet");

            migrationBuilder.DropTable(
                name: "DensoSuppliers");

            migrationBuilder.DropTable(
                name: "DensoUnitMeasures");

            migrationBuilder.DropTable(
                name: "DensoCarriers");

            migrationBuilder.DropTable(
                name: "DensoCustomerPlants");

            migrationBuilder.DropTable(
                name: "DensoDepartments");

            migrationBuilder.DropTable(
                name: "DensoDocumentStatuses");

            migrationBuilder.DropTable(
                name: "DensoIEStaff");

            migrationBuilder.DropTable(
                name: "DensoPaidBy");

            migrationBuilder.DropTable(
                name: "DensoPaymentTerms");

            migrationBuilder.DropTable(
                name: "DensoPlants");

            migrationBuilder.DropTable(
                name: "DensoServices");

            migrationBuilder.DropTable(
                name: "DensoHotSheetReasons");

            migrationBuilder.DropTable(
                name: "DensoDocumentTypes");

            migrationBuilder.DropTable(
                name: "DensoCustomers");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AbpUsers");
        }
    }
}
