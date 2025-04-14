using Denso.HotSheet.Sheets.Dto;
using System.Collections.Generic;

namespace Denso.HotSheet.Catalogs.Dto
{
    public class CatalogsForEditDto
    {
        public List<CarrierDto> Carriers { get; set; } = new List<CarrierDto>();
        public List<DocumentTypeDto> DocumentTypes { get; set; } = new List<DocumentTypeDto>();
        public List<PaymentTermDto> PaymentTerms { get; set; } = new List<PaymentTermDto>();
        public List<HotSheetReasonDto> HotSheetReasons { get; set; } = new List<HotSheetReasonDto>();
        public List<ServiceDto> Services { get; set; } = new List<ServiceDto>();
        public List<DocumentStatusDto> Status { get; set; } = new List<DocumentStatusDto>();

        // More catalogs
        public List<CustomerDto> Customers { get; set; } = new List<CustomerDto>();
        public List<DepartmentByUserDto> DepartmentsByUser { get; set; } = new List<DepartmentByUserDto>();
        public List<PlantByUserDto> PlantsByUser { get; set; } = new List<PlantByUserDto>();
        public List<HotSheetTermDto> HotSheetTerms { get; set; } = new List<HotSheetTermDto>();
        public List<RmaAssignmentDto> RmaAssignments { get; set; } = new List<RmaAssignmentDto>();
        public List<PaidByDto> PaidBy { get; set; } = new List<PaidByDto>();
        public List<SpecialExpeditedReasonDto> SpecialExpeditedReasons { get; set; } = new List<SpecialExpeditedReasonDto>();
        public List<ApproverStaffDto> ApproversStaff { get; set; } = new List<ApproverStaffDto>();
        public List<HelpInfoDto> HelpInfo { get; set; } = new List<HelpInfoDto>();

        public List<PartNumberForSelectDto> PartNumbers { get; set; } = new List<PartNumberForSelectDto>();
        public List<UnitMeasureDto> UnitMeasures { get; set; } = new List<UnitMeasureDto>();
        public List<ProductCodeSATDto> ProductCodesSAT { get; set; } = new List<ProductCodeSATDto>();
        public List<CountryDto> Countries { get; set; } = new List<CountryDto>();
        public List<CurrencyDto> Currencies { get; set; } = new List<CurrencyDto>();
    }
}
