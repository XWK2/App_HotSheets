using Abp.Application.Services;
using Denso.HotSheet.Catalogs.Dto;
using Denso.HotSheet.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Sheets.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Denso.HotSheet.Catalogs
{
    public interface ICatalogAppService : IApplicationService
    {

        Task<List<ShortageShiftDto>> GetShortageShift(bool? isActive = null);

        Task CreateOrUpdateShortageShift(ShortageShiftDto input);
        Task<List<TransportModeDto>> GetTransportMode(bool? isActive = null);

        Task CreateOrUpdateTransportMode(TransportModeDto input);
        Task<List<StatusHotSheetDto>> GetStatusHotSheet(bool? isActive = null);

        Task CreateOrUpdateStatusHotSheet(StatusHotSheetDto input);
        Task<List<CountryDto>> GetCountries();

        #region Currencies

        Task<List<CurrencyDto>> GetCurrencies();
        Task CreateOrUpdateCurrency(CurrencyDto input);

        #endregion

        #region DocumentTypes

        Task<List<DocumentTypeDto>> GetDocumentTypes(bool? isActive = null, long? id = null);
        Task CreateOrUpdateDocumentType(DocumentTypeDto input);

        #endregion

        #region Payment Terms

        Task<List<PaymentTermDto>> GetPaymentTerms(long? id = null);
        Task CreateOrUpdatePaymentTerm(PaymentTermDto input);       

        #endregion

        #region Departments

        Task<List<DepartmentByUserDto>> GetDepartmentsByCurrentUser();
        Task<List<DepartmentDto>> GetDepartments(bool? isActive = null);
        Task CreateOrUpdateDepartment(DepartmentDto input);
        Task CreateOrUpdateDepartmentSp(DepartmentDto input);

        #endregion

        #region Services 

        Task<List<ServiceDto>> GetServices(bool? isActive = null, long? id = null);

        #endregion

        Task<BaseCatalogsDto> GetBaseCatalogs();
        Task<CatalogsForEditDto> GetCatalogsForEdit(GetCatalogsForEditInput input);

        #region Plants

        Task<List<PlantByUserDto>> GetPlantsByCurrentUser();
        Task<List<PlantDto>> GetPlants(bool? isActive = null);
        Task CreateOrUpdatePlant(PlantDto input);

        #endregion

        #region Carriers
        
        Task<List<CarrierDto>> GetCarriers(bool? isActive = null, long? id = null);

        Task<List<CurrentCarrierServicesDto>> GetCarrierServicesforEdit();
        Task CreateOrUpdateCarrier(CarrierDto input);

        #endregion

        #region HotSheet Reasons
        
        Task<List<HotSheetReasonDto>> GetHotSheetReasons(bool? isActive = null, long? id = null);

        #endregion

        #region Customers
        
        Task<List<CustomerDto>> GetCustomers(bool? isActive = null, long? id = null);
        Task<List<CustomerDto>> GetActiveCustomers();
        Task<CustomerDto> GetCustomerById(long customerId);
        Task CreateOrUpdateCustomer(CustomerDto input);

        #endregion

        #region HotSheet Terms

        Task<List<HotSheetTermDto>> GetHotSheetTerms(bool? isActive = null);

        #endregion

        #region Rma Assignments
        
        Task<List<RmaAssignmentDto>> GetRmaAssignments(bool? isActive = null);

        Task CreateOrUpdateRmaAssignment(RmaAssignmentDto input);

        #endregion

        #region Paid By

        Task<List<PaidByDto>> GetPaidBy(bool? isActive = null);

        Task CreateOrUpdatePaidBy(PaidByDto input);

        #endregion

        #region Unit Measures

        Task<List<UnitMeasureDto>> GetUnitMeasures(bool? isActive = null);
        Task CreateOrUpdateUnitMeasure(UnitMeasureDto input);

        #endregion

        #region Staff
        Task<List<StaffDto>> GetStaff(bool? isActive = null);
        Task<List<ApproverStaffDto>> GetApproversStaff();

        Task CreateOrUpdateStaff(StaffDto input);

        #endregion

        #region Product Codes SAT

        Task<List<ProductCodeSATDto>> GetProductCodesSat(bool? isActive = null, List<long> productCodeSATIds = null);

        Task CreateOrUpdateProductCodeSat(ProductCodeSATDto input);

        #endregion

        #region Part Numbers

        Task<List<PartNumberForSelectDto>> GetPartNumbersForSelect(long? id = null);
        
        Task<List<PartNumberDto>> GetPartNumbers(bool? isActive = null);
        Task CreateOrUpdatePartNumber(PartNumberDto input);

        Task<List<PartNumberInternalDto>> GetPartNumbersInternal(bool? isActive = null);
        Task CreateOrUpdatePartNumberInternal(PartNumberInternalDto input);

        Task<List<PartNumberResXlsxDto>> CreateOrUpdatePartNumberXLSX(List<PartNumberXlsxDto> input);

        Task<List<PartNumberInternalResXlsxDto>> CreateOrUpdatePartNumberInternalXLSX(List<PartNumberInternalXlsxDto> input);

        #endregion

        Task<List<SpecialExpeditedReasonDto>> GetSpecialExpeditedReason(bool? isActive = null, long? id = null);
        Task<List<PackagingDto>> GetPackaging(bool? isActive = null);

        Task<List<DocumentStatusDto>> GetStatus();

        #region Part Number Prices

        Task<List<PartNumberPriceDto>> GetPartNumberPrices(bool? isActive = null);
        Task CreateOrUpdatePartNumberPrice(PartNumberPriceDto input);

        Task<List<PartNumberPriceInternalDto>> GetPartNumberPricesInternal(bool? isActive = null);
        Task CreateOrUpdatePartNumberPriceInternal(PartNumberPriceInternalDto input);
              
        #endregion

        #region Help Info

        Task<List<HelpInfoDto>> GetHelpInfo();
        Task<List<HelpInfoFieldDto>> GetHelpInfoFields();

        Task CreateOrUpdateHelpInfo(HelpInfoDto input);

        #endregion

        #region Employees

        Task<List<EmployeeDto>> GetEmployees();
        Task<List<EmployeeDto>> GetEmployeesList();
        Task<EmployeeOptionsDto> GetEmployeeOptions();
        Task CreateOrUpdateEmployee(EmployeeDto input);

        #endregion

        #region Notices

        Task<List<NoticeDto>> GetNotices();
        Task<List<NoticeDto>> GetNoticesToDisplay();
        Task CreateOrUpdateNotice(NoticeDto input);

        #endregion

        Task<List<PaymentStatus>> GetPaymentStatus();
    }
}
