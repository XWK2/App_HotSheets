using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Denso.HotSheet.Authorization
{
    public class HotSheetAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var administration = context.CreatePermission(PermissionNames.Pages_Administration, L("Administration"));
            
            var users = administration.CreateChildPermission(PermissionNames.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(PermissionNames.Pages_Administration_Users_Activation, L("UsersActivation"));

            var roles = administration.CreateChildPermission(PermissionNames.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(PermissionNames.Pages_Administration_Roles_Create, L("CreateNewRole"));
            roles.CreateChildPermission(PermissionNames.Pages_Administration_Roles_Edit, L("EditRole"));
            roles.CreateChildPermission(PermissionNames.Pages_Administration_Roles_Delete, L("DeleteRole"));
            administration.CreateChildPermission(PermissionNames.Pages_Administration_Settings, L("Settings"));

            var HotSheet = context.CreatePermission(PermissionNames.Pages_HotSheet, L("HotSheet"));
            HotSheet.CreateChildPermission(PermissionNames.Pages_HotSheet_Create, L("HotSheetCreate"));
            HotSheet.CreateChildPermission(PermissionNames.Pages_HotSheet_Edit, L("HotSheetEdit"));
            HotSheet.CreateChildPermission(PermissionNames.Pages_HotSheet_Cancel, L("HotSheetCancel"));
            HotSheet.CreateChildPermission(PermissionNames.Pages_HotSheet_Approvals, L("HotSheetApprovals"));
            HotSheet.CreateChildPermission(PermissionNames.Pages_HotSheet_ExportToAS400, L("HotSheetExportToAS400"));
            HotSheet.CreateChildPermission(PermissionNames.Pages_HotSheet_PendingForApproval, L("PendingForApproval"));
            HotSheet.CreateChildPermission(PermissionNames.Pages_HotSheet_Templates, L("Templates"));

            var catalogs = context.CreatePermission(PermissionNames.Pages_Catalogs, L("Catalogs"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Plants, L("Plants"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Divisions, L("Divisions"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Departments, L("Departments"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_DocumentTypes, L("DocumentTypes"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Carriers, L("Carriers"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Services, L("Services"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Suppliers, L("Suppliers"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_HotSheetReasons, L("HotSheetReasons"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_PartNumbers, L("PartNumbers"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_PartNumberPrices, L("PartNumberPrices"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_HotSheetTerms, L("HotSheetTerms"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_ProductCodesSAT, L("ProductCodesSat"));

            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_StatusHotSheet, L("StatusHotSheet"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_ShortageShift, L("ShortageShift"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_TransportMode, L("TransportMode"));
            
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_UnitMeasures, L("UnitMeasures"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_UnitMeasuresSAT, L("UnitMeasuresSat"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_RMAAssignments, L("RMAAssignments"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_PaidBy, L("PaidBy"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_PaymentTerms, L("PaymentTerms"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_IEStaff, L("IEStaff"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_AccountingStaff, L("AccountingStaff"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Customers, L("Customers"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_SpecialExpeditedReasons, L("SpecialExpeditedReasons"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Packaging, L("Packaging"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_HelpInfo, L("HelpInfo"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Employees, L("Employees"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Currencies, L("Currencies"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_LogServices, L("LogServices"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_PartNumbersInternal, L("PartNumbersInternal"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_PartNumberPricesInternal, L("PartNumberPricesInternal"));
            catalogs.CreateChildPermission(PermissionNames.Pages_Catalogs_Notices, L("Notices"));

            var surveys = context.CreatePermission(PermissionNames.Pages_Surveys, L("Surveys"));
            var hotSheets = context.CreatePermission(PermissionNames.Pages_HotSheets, L("HotSheets"));

            var reports = context.CreatePermission(PermissionNames.Pages_Reports, L("Reports"));
            reports.CreateChildPermission(PermissionNames.Pages_Reports_HotSheetsReports, L("HotSheetsReports"));
            reports.CreateChildPermission(PermissionNames.Pages_Reports_TrackingScrapSales, L("TrackingScrapSales"));
            reports.CreateChildPermission(PermissionNames.Pages_Reports_TrackingGuidesReports, L("TrackingGuidesReports"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, HotSheetConsts.LocalizationSourceName);
        }
    }
}
