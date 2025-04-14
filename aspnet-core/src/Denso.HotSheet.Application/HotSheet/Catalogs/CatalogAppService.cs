using Abp.Authorization;
using Abp.Dapper.Repositories;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Organization;
using Denso.HotSheet.HotSheet;
using Denso.HotSheet.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Sheets.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlTypes;

namespace Denso.HotSheet.Catalogs
{
    [AbpAuthorize]
    public class CatalogAppService : HotSheetAppServiceBase, ICatalogAppService
    {
        private readonly IRepository<StatusHotSheet, long> _statusHotSheetRepository;
        private readonly IRepository<TransportMode, long> _transportModeRepository;
        private readonly IRepository<ShortageShift, long> _shortageShiftRepository;

        private readonly IRepository<DocumentType> _documentTypeRepository;
        private readonly IRepository<Plant, long> _plantRepository;
        private readonly IRepository<Carrier, long> _carrierRepository;
        private readonly IRepository<HotSheetReason> _HotSheetReasonRepository;
        private readonly IRepository<PaymentTerm> _paymentTermRepository;
        private readonly IRepository<Department, long> _departmentRepository;
        private readonly IRepository<Service, long> _serviceRepository;
        private readonly IRepository<CarrierService, long> _carrierServiceRepository;        
        private readonly IRepository<DepartmentUser, long> _departmentUserRepository;
        private readonly IRepository<PlantUser, long> _plantUserRepository;
        private readonly IRepository<Customer, long> _customerRepository;
        private readonly IRepository<CustomerPlant, long> _customerPlantRepository;
        private readonly IRepository<CustomerPlantContact, long> _customerPlantContactRepository;
        private readonly IRepository<HotSheetTerm> _HotSheetTermRepository;
        private readonly IRepository<RMAAssignment> _rmaAssignmentRepository;
        private readonly IRepository<PaidBy, long> _paidByRepository;
        private readonly IRepository<UnitMeasure> _unitMeasureRepository;
        private readonly IRepository<Staff, long> _staffRepository;
        private readonly IRepository<ProductCodeSAT, long> _productCodeSATRepository;
        private readonly IRepository<PartNumber, long> _partNumberRepository;
        private readonly IRepository<SpecialExpeditedReason> _specialExpeditedReasonRepository;
        private readonly IRepository<Packaging, long> _packagingRepository;
        private readonly IRepository<DocumentStatus> _documentStatusRepository;
        private readonly IRepository<PartNumberPrice, long> _partNumberPriceRepository;
        private readonly IRepository<HelpInfo, long> _helpInfoRepository;
        private readonly IRepository<HelpInfoField, long> _helpInfoFieldRepository;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<Employee, long> _employeeRepository;
        private readonly IRepository<PaidByPaymentTerm> _paidByPaymentTermRepository;
        private readonly IRepository<PaidByHotSheetTerm> _paidByHotSheetTermRepository;
        private readonly IRepository<CarrierNonWorkingDay, long> _carrierNonWorkingDayRepository;
        private readonly IRepository<PartNumberInternal, long> _partNumberInternalRepository;
        private readonly IRepository<PartNumberPriceInternal, long> _partNumberPriceInternalRepository;
        private readonly IRepository<PaymentTermCarrier, long> _paymentTermCarrierRepository;
        private readonly IRepository<Notice, long> _noticeRepository;

        private readonly IRepository<EmployeeType> _employeeTypeRepository;
        private readonly IRepository<EmployeeLevel> _employeeLevelRepository;
        private readonly IRepository<EmployeePosition> _employeePositionRepository;
        private readonly IRepository<HotSheetsShip, long> _HotSheetRepository;
        private readonly IRepository<PaymentStatus> _paymentStatusRepository;

        private readonly UserRegistrationManager _userRegistrationManager;

        // Dapper Integration
        private readonly IDapperRepository<Country> _countryDapperRepository;
        private readonly IDapperRepository<Department, long> _departmentDapperRepository;
        private readonly IDapperRepository<Plant, long> _plantDapperRepository;
        private readonly IDapperRepository<PartNumber, long> _partNumberDapperRepository;
        private readonly IDapperRepository<Staff, long> _staffDapperRepository;
        private readonly IDapperRepository<HotSheetsShip, long> _hotSheetDapperRepository;
        private readonly IDapperRepository<HotSheets, long> _hotSheetsDapperRepository;

        public CatalogAppService(
            IRepository<StatusHotSheet, long> statusHotSheetRepository,
            IRepository<TransportMode, long> transportModeRepository,
            IRepository<ShortageShift, long> shortageShiftRepository,

            IRepository<Country> countryTypeRepository,
            IRepository<DocumentType> documentTypeRepository,
            IRepository<Plant, long> plantRepository,
            IRepository<Carrier, long> carrierRepository,
            IRepository<HotSheetReason> HotSheetReasonRepository,
            IRepository<PaymentTerm> paymentTermRepository,
            IRepository<Department, long> departmentRepository,
            IRepository<Service, long> serviceRepository,
            IRepository<CarrierService, long> carrierServiceRepository,
            IRepository<DepartmentUser, long> departmentUserRepository,
            IRepository<PlantUser, long> plantUserRepository,
            IRepository<Customer, long> customerRepository,
            IRepository<CustomerPlant, long> customerPlantRepository,
            IRepository<CustomerPlantContact, long> customerPlantContactRepository,
            IRepository<HotSheetTerm> HotSheetTermRepository,
            IRepository<RMAAssignment> rmaAssignmentRepository,
            IRepository<PaidBy, long> paidByRepository,
            IRepository<PaidByPaymentTerm> paidByPaymentTermRepository,
            IRepository<PaidByHotSheetTerm> paidByHotSheetTermRepository,

            IDapperRepository<Country> countryDapperRepository,
            IDapperRepository<Department, long> departmentDapperRepository,
            IDapperRepository<Plant, long> plantDapperRepository,
            IDapperRepository<PartNumber,long> partNumberDapperRepository,
            IDapperRepository<Staff, long> staffDapperRepository,
            IDapperRepository<HotSheetsShip, long> HotSheetDapperRepository,

            IRepository<UnitMeasure> unitMeasureRepository,
            IRepository<Staff, long> staffRepository,
            IRepository<ProductCodeSAT, long> productCodeSATRepository,          

            IRepository<PartNumber, long> partNumberRepository,
            IRepository<SpecialExpeditedReason> specialExpeditedReasonRepository,
            IRepository<Packaging, long> packagingRepository,
            IRepository<DocumentStatus> documentStatusRepository,
            IRepository<PartNumberPrice, long> partNumberPriceRepository,
            IRepository<HelpInfo, long> helpInfoRepository,
            IRepository<HelpInfoField, long> helpInfoFieldRepository,
            IRepository<Currency> currencyRepository,
            IRepository<Employee, long> employeeRepository,
            IRepository<CarrierNonWorkingDay, long> carrierNonWorkingDayRepository,

            IRepository<PartNumberInternal, long> partNumberInternalRepository,
            IRepository<PartNumberPriceInternal, long> partNumberPriceInternalRepository,
            IRepository<PaymentTermCarrier, long> paymentTermCarrierRepository,
            IRepository<Notice, long> noticeRepository,
            IRepository<EmployeeType> employeeTypeRepository,
            IRepository<EmployeeLevel> employeeLevelRepository,
            IRepository<EmployeePosition> employeePositionRepository,
            IRepository<HotSheetsShip, long> HotSheetRepository,
            IRepository<PaymentStatus> paymentStatusRepository,
            UserRegistrationManager userRegistrationManager
        )
        {
         

            _statusHotSheetRepository = statusHotSheetRepository;
            _transportModeRepository = transportModeRepository;
            _shortageShiftRepository = shortageShiftRepository;

            _documentTypeRepository = documentTypeRepository;
            _plantRepository = plantRepository;
            _carrierRepository = carrierRepository;
            _HotSheetReasonRepository = HotSheetReasonRepository;
            _paymentTermRepository = paymentTermRepository;
            _departmentRepository = departmentRepository;
            _serviceRepository = serviceRepository;
            _carrierServiceRepository = carrierServiceRepository;
            _departmentUserRepository = departmentUserRepository;
            _plantUserRepository = plantUserRepository;
            _customerRepository = customerRepository;
            _customerPlantRepository = customerPlantRepository;
            _customerPlantContactRepository = customerPlantContactRepository;
            _HotSheetTermRepository = HotSheetTermRepository;
            _rmaAssignmentRepository = rmaAssignmentRepository;
            _paidByRepository = paidByRepository;
            _paidByPaymentTermRepository = paidByPaymentTermRepository;
            _paidByHotSheetTermRepository = paidByHotSheetTermRepository;
            _countryDapperRepository = countryDapperRepository;
            _departmentDapperRepository = departmentDapperRepository;
            _plantDapperRepository = plantDapperRepository;
            _partNumberDapperRepository= partNumberDapperRepository;
            _unitMeasureRepository = unitMeasureRepository;
            _staffRepository = staffRepository;
            _productCodeSATRepository = productCodeSATRepository;
            _partNumberRepository = partNumberRepository;
            _specialExpeditedReasonRepository = specialExpeditedReasonRepository;
            _packagingRepository = packagingRepository;
            _documentStatusRepository = documentStatusRepository;
            _partNumberPriceRepository = partNumberPriceRepository;
            _staffDapperRepository = staffDapperRepository;
            _helpInfoRepository = helpInfoRepository;
            _helpInfoFieldRepository = helpInfoFieldRepository;
            _currencyRepository = currencyRepository;
            _employeeRepository = employeeRepository;
            _carrierNonWorkingDayRepository = carrierNonWorkingDayRepository;
            _partNumberInternalRepository = partNumberInternalRepository;
            _partNumberPriceInternalRepository = partNumberPriceInternalRepository;
            _paymentTermCarrierRepository = paymentTermCarrierRepository;
            _noticeRepository = noticeRepository;
            _employeeTypeRepository = employeeTypeRepository;
            _employeeLevelRepository = employeeLevelRepository;
            _employeePositionRepository = employeePositionRepository;
            _HotSheetRepository = HotSheetRepository;
            _hotSheetDapperRepository = HotSheetDapperRepository;
            _paymentStatusRepository = paymentStatusRepository;

            _userRegistrationManager = userRegistrationManager;
        }




        //_statusHotSheetRepository = statusHotSheetRepository;
        //    _transportModeRepository = transportModeRepository;
        //    _shortageShiftRepository = shortageShiftRepository;

        public async Task<List<ShortageShiftDto>> GetShortageShift(bool? isActive = null)
        {
            var parameters = new { IsActive = isActive };
            var sql = "SELECT * FROM DensoShortageShift WHERE @IsActive IS NULL OR IsActive = @IsActive";
            var dapperData = await _hotSheetDapperRepository.QueryAsync<ShortageShift>(sql, parameters);

            var itemsDto = ObjectMapper.Map<List<ShortageShiftDto>>(dapperData.ToList());
            return itemsDto;
        }

        public async Task CreateOrUpdateShortageShift(ShortageShiftDto input)
        {
            if (input.Id.HasValue)
            {
                var shortageShift = await _shortageShiftRepository.GetAsync(input.Id.Value);
                if (shortageShift != null)
                {
                    shortageShift.Code = input.Code;
                    shortageShift.Description = input.Description;
                    shortageShift.IsActive = input.IsActive;

                    await _shortageShiftRepository.UpdateAsync(shortageShift);
                }
            }
            else
            {
                var shortageShift = ObjectMapper.Map<ShortageShift>(input);
                shortageShift.IsActive = true;

                await _shortageShiftRepository.InsertAsync(shortageShift);
            }
        }

        public async Task<List<TransportModeDto>> GetTransportMode(bool? isActive = null)
        {
            var parameters = new { IsActive = isActive };
            var sql = "SELECT * FROM DensoTransportMode WHERE @IsActive IS NULL OR IsActive = @IsActive";
            var dapperData = await _hotSheetDapperRepository.QueryAsync<TransportMode>(sql, parameters);

            var itemsDto = ObjectMapper.Map<List<TransportModeDto>>(dapperData.ToList());
            return itemsDto;
        }

        public async Task CreateOrUpdateTransportMode(TransportModeDto input)
        {
            if (input.Id.HasValue)
            {
                var transportMode = await _transportModeRepository.GetAsync(input.Id.Value);
                if (transportMode != null)
                {
                    transportMode.Code = input.Code;
                    transportMode.Description = input.Description;
                    transportMode.IsActive = input.IsActive;

                    await _transportModeRepository.UpdateAsync(transportMode);
                }
            }
            else
            {
                var transportMode = ObjectMapper.Map<TransportMode>(input);
                transportMode.IsActive = true;

                await _transportModeRepository.InsertAsync(transportMode);
            }
        }

        public async Task<List<StatusHotSheetDto>> GetStatusHotSheet(bool? isActive = null)
        {
            var parameters = new { IsActive = isActive };
            var sql = "SELECT * FROM DensoStatusHotSheet WHERE @IsActive IS NULL OR IsActive = @IsActive";
            var dapperData = await _hotSheetDapperRepository.QueryAsync<StatusHotSheet>(sql, parameters);
            
            var itemsDto = ObjectMapper.Map<List<StatusHotSheetDto>>(dapperData.ToList());
            return itemsDto;
        }

        public async Task CreateOrUpdateStatusHotSheet(StatusHotSheetDto input)
        {
            if (input.Id.HasValue)
            {
                var statusHotSheet = await _statusHotSheetRepository.GetAsync(input.Id.Value);
                if (statusHotSheet != null)
                {
                    statusHotSheet.Code = input.Code;
                    statusHotSheet.Description = input.Description;
                    statusHotSheet.IsActive = input.IsActive;

                    await _statusHotSheetRepository.UpdateAsync(statusHotSheet);
                }
            }
            else
            {
                var statusHotSheet = ObjectMapper.Map<StatusHotSheet>(input);
                statusHotSheet.IsActive = true;

                await _statusHotSheetRepository.InsertAsync(statusHotSheet);
            }
        }

        public async Task<List<CountryDto>> GetCountries()
        {
            // Get data using Dapper Framework
            var itemsDapper = await _countryDapperRepository.QueryAsync<CountryDto>("SELECT * FROM DensoCountries WHERE IsActive = 1;");

            // var items2 = await _countryDapperRepository.GetAllAsync(); // Failing, check entity mapping vs table name
            // var items = await _countryTypeRepository.GetAllListAsync(); // EF wihtoutp Dapper

            foreach (var item in itemsDapper)
            {
                item.FullName = item.DensoCode + " - " + item.Name + " - " + item.SatCode;
            }

            return itemsDapper.ToList();
        }

        #region Currencies

        public async Task<List<CurrencyDto>> GetCurrencies()
        {
            var items = await _currencyRepository.GetAllListAsync(c => c.IsActive);
            var itemsDto = ObjectMapper.Map<List<CurrencyDto>>(items);

            return new List<CurrencyDto>(itemsDto);
        }

        public async Task CreateOrUpdateCurrency(CurrencyDto input)
        {
            if (input.Id.HasValue)
            {
                var currency = await _currencyRepository.GetAsync(input.Id.Value);
                if (currency != null)
                {
                    currency.Name = input.Name;
                    currency.Code = input.Code;
                    currency.DensoCode = input.DensoCode;
                    currency.IsActive = input.IsActive;

                    await _currencyRepository.UpdateAsync(currency);
                }
            }
            else
            {
                var currency = ObjectMapper.Map<Currency>(input);
                currency.IsActive = true;

                await _currencyRepository.InsertAsync(currency);
            }
        }

        #endregion

        #region Document Types

        public async Task<List<DocumentTypeDto>> GetDocumentTypes(bool? isActive = null, long? id = null)
        {
            var sqlParams = new { IsActive = isActive, Id = id };
            var itemsDapper = await _departmentDapperRepository.QueryAsync<DocumentTypeDto>("EXEC GetDocumentTypes @IsActive, @Id", sqlParams);
            return itemsDapper.ToList();
        }

        public async Task CreateOrUpdateDocumentType(DocumentTypeDto input)
        {
            if (input.Id.HasValue)
            {
                var documentType = await _documentTypeRepository.GetAsync(input.Id.Value);
                if (documentType != null)
                {
                    documentType.Name = input.Name;
                    documentType.IsActive = input.IsActive;

                    await _documentTypeRepository.UpdateAsync(documentType);
                }
            }
            else
            {
                var documentType = ObjectMapper.Map<DocumentType>(input);
                documentType.IsActive = true;

                await _documentTypeRepository.InsertAsync(documentType);
            }
        }

        #endregion

        #region Payment Terms

        public async Task<List<PaymentTermDto>> GetPaymentTerms(long? id = null)
        {
            var items = await _paymentTermRepository.GetAll()
                .Include(i => i.Carriers)
                .Where(c => !id.HasValue || c.Id == id)
                .ToListAsync();
            var itemsDto = ObjectMapper.Map<List<PaymentTermDto>>(items);

            foreach (var item in itemsDto)
            {
                string warning1CompanyIds = items.First(c => c.Id == item.Id)?.Warning1CompanyIds;
                string warning2CompanyIds = items.First(c => c.Id == item.Id)?.Warning2CompanyIds;

                item.Warning1CompanyIds = new List<int>();
                item.Warning2CompanyIds = new List<int>();

                if (!string.IsNullOrEmpty(warning1CompanyIds))
                {
                    item.Warning1CompanyIds = warning1CompanyIds.Split(',').Select(int.Parse).ToList();
                }
                if (!string.IsNullOrEmpty(warning2CompanyIds))
                {
                    item.Warning2CompanyIds = warning2CompanyIds.Split(',').Select(int.Parse).ToList();
                }
            }

            return new List<PaymentTermDto>(itemsDto);
        }

        public async Task CreateOrUpdatePaymentTerm(PaymentTermDto input)
        {
            if (input.Id.HasValue)
            {
                var paymentTerm = await _paymentTermRepository.GetAsync(input.Id.Value);
                if (paymentTerm != null)
                {
                    _paymentTermCarrierRepository.Delete(c => c.PaymentTermId == input.Id);

                    paymentTerm.Name = input.Name;
                    paymentTerm.Description = input.Description;
                    paymentTerm.AlwaysDnmx = input.AlwaysDnmx;
                    paymentTerm.AccountingApprovalRequired = input.AccountingApprovalRequired;
                    paymentTerm.ExcludeOnSamples = input.ExcludeOnSamples;
                    paymentTerm.Warning1CompanyIds = string.Join(",", input.Warning1CompanyIds.ToArray());
                    paymentTerm.Warning1Message = input.Warning1Message;
                    paymentTerm.Warning2CompanyIds = string.Join(",", input.Warning2CompanyIds.ToArray());
                    paymentTerm.Warning2Message = input.Warning2Message;
                    paymentTerm.Warning2Amount = input.Warning2Amount;
                    paymentTerm.POWarning = input.POWarning;
                    paymentTerm.Carriers = ObjectMapper.Map<List<PaymentTermCarrier>>(input.Carriers);

                    paymentTerm.IsActive = input.IsActive;

                    await _paymentTermRepository.UpdateAsync(paymentTerm);

                    // TODO: Carriers configs
                    //await Task.Run(async () =>
                    //{
                    //    List<PaymentTermCarrier> currentPaymentTermCarriers = await _paymentTermCarrierRepository.GetAllListAsync(c => c.PaymentTermId == input.Id);
                    //    List<long> newCarrierServicesIds = input.Services.Select(s => s.ServiceId).ToList();
                    //    List<long> carrierServicesIdsDeleted = currentCarrierServices.Where(cs => cs.ServiceId.HasValue
                    //        && !newCarrierServicesIds.Contains(cs.ServiceId.Value)).ToList()
                    //        .Select(s => s.Id).ToList();

                    //    _paymentTermCarrierRepository.Delete(c => carrierServicesIdsDeleted.Contains(c.Id));
                    //});
                }
            }
            else
            {
                var paymentTerm = ObjectMapper.Map<PaymentTerm>(input);
                paymentTerm.IsActive = true;

                await _paymentTermRepository.InsertAsync(paymentTerm);
            }
        }

        #endregion

        public async Task<BaseCatalogsDto> GetBaseCatalogs()
        {
            var catalogs = new BaseCatalogsDto();

            catalogs.Carriers = await GetCarriers(true);
            catalogs.DocumentTypes = await GetDocumentTypes(true);
            catalogs.PaymentTerms = await GetPaymentTerms();
            catalogs.HotSheetReasons = await GetHotSheetReasons(true);
            catalogs.Services = await GetServices(true);
            catalogs.Status = await GetStatus();

            return catalogs;
        }

        public async Task<CatalogsForEditDto> GetCatalogsForEdit(GetCatalogsForEditInput input)
        {
            var catalogs = new CatalogsForEditDto();
            var HotSheetInfo = new HotSheetsShip();
            
            if(input.IsView && input.HotSheetShiptId.HasValue)
            {
                var parameters = new { Id = input.HotSheetShiptId };
                var sql = "SELECT * FROM DensoHotSheet WHERE Id = @Id";
                var HotSheetData = await _hotSheetDapperRepository.QueryAsync<HotSheetsShip>(sql, parameters);
                HotSheetInfo = HotSheetData.FirstOrDefault();
            }
            else
            {
                HotSheetInfo = null;
            }

            catalogs.Carriers = await GetCarriers(true, HotSheetInfo?.CarrierId);
            catalogs.DocumentTypes = await GetDocumentTypes(true, HotSheetInfo?.DocumentTypeId);
            catalogs.PaymentTerms = await GetPaymentTerms(HotSheetInfo?.PaymentTermId);
            catalogs.HotSheetReasons = await GetHotSheetReasons(true, HotSheetInfo?.HotSheetReasonId);
            catalogs.Services = await GetServices(true, HotSheetInfo?.ServiceId);
            catalogs.Customers = await GetCustomers(null, HotSheetInfo?.CustomerId);

            catalogs.Status = await GetStatus();
            catalogs.HotSheetTerms = await GetHotSheetTerms();
            catalogs.RmaAssignments = await GetRmaAssignments();
            catalogs.PaidBy = await GetPaidBy();
            catalogs.SpecialExpeditedReasons = await GetSpecialExpeditedReason(null, HotSheetInfo?.SpecialExpeditedReasonId);
            catalogs.HelpInfo = await GetHelpInfo();
            catalogs.UnitMeasures = await GetUnitMeasures();
            catalogs.Countries = await GetCountries();
            catalogs.Currencies = await GetCurrencies();
            catalogs.ApproversStaff = await GetApproversStaff();

            if (!input.IsView)
            {
                catalogs.PlantsByUser = await GetPlantsByCurrentUser();
                catalogs.DepartmentsByUser = await GetDepartmentsByCurrentUser();                
                catalogs.ProductCodesSAT = await GetProductCodesSat();
            }

            return catalogs;
        }

        #region Departments

        public async Task<List<DepartmentByUserDto>> GetDepartmentsByCurrentUser()
        {
            var sqlParams = new { UserId = AbpSession.UserId };
            var itemsDapper = await _departmentDapperRepository.QueryAsync<DepartmentByUserDto>("EXEC GetDepartmentsByUser @UserId", sqlParams);

            foreach (var item in itemsDapper)
            {
                item.FullName = item.DepartmentId + " - " + item.DepartmentName;
            }

            return new List<DepartmentByUserDto>(itemsDapper);
        }

        public async Task<List<DepartmentDto>> GetDepartments(bool? isActive = null)
        {
            var sqlParams = new { IsActive = isActive };
            var itemsDapper = await _departmentDapperRepository.QueryAsync<DepartmentDto>("EXEC GetDepartments @IsActive", sqlParams);
            return itemsDapper.ToList();
        }

        public async Task CreateOrUpdateDepartment(DepartmentDto input)
        {
            if (input.Id.HasValue)
            {
                var department = await _departmentRepository.GetAsync(input.Id.Value);
                if (department != null)
                {
                    department.Name = input.Name;
                    department.IsActive = input.IsActive;

                    await _departmentRepository.UpdateAsync(department);
                }
            }
            else
            {
                var department = ObjectMapper.Map<Department>(input);
                department.IsActive = true;

                await _departmentRepository.InsertAsync(department);
            }
        }

        public async Task CreateOrUpdateDepartmentSp(DepartmentDto input)
        {
            //if (input.Id.HasValue)
            //{
                try
                {
                    int previousId = 0;
                    if (input.FullName != null) {
                        string[] listNameFull = input.FullName.Split("-");
                        previousId = int.Parse(listNameFull[0]);
                    }                   

                    var sqlParams = new { iId = input.Id, sNombre = input.Name, bActive = input.IsActive, iPreviousid = previousId };
                    await _partNumberDapperRepository.QueryAsync<Department>("EXEC UpdateOrInsertDepartment @iId, @sNombre, @bActive, @iPreviousid", sqlParams);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            //}
        }

        #endregion

        #region Services

        public async Task<List<ServiceDto>> GetServices(bool? isActive = null, long? id = null)
        {
            var items = await _serviceRepository.GetAllListAsync(c => (c.IsActive == isActive || isActive == null)
                    && (!id.HasValue || c.Id == id));
            var itemsDto = ObjectMapper.Map<List<ServiceDto>>(items);

            return new List<ServiceDto>(itemsDto);
        }

        public async Task CreateOrUpdateService(ServiceDto input)
        {
            if (input.Id.HasValue)
            {
                var service = await _serviceRepository.GetAsync(input.Id.Value);
                if (service != null)
                {
                    service.Name = input.Name;
                    service.Description = input.Description;
                    service.IsNational = input.IsNational;
                    service.IsInternational = input.IsInternational;
                    service.ShowHigestCostWarning = input.ShowHigestCostWarning;
                    service.Ground = input.Ground;
                    service.Air = input.Air;
                    service.Sea = input.Sea;

                    await _serviceRepository.UpdateAsync(service);
                }
            }
            else
            {
                var service = ObjectMapper.Map<Service>(input);
                await _serviceRepository.InsertAsync(service);
            }
        }

        #endregion

        #region Plants

        public async Task<List<PlantByUserDto>> GetPlantsByCurrentUser()
        {
            var sqlParams = new { UserId = AbpSession.UserId };
            var itemsDapper = await _plantDapperRepository.QueryAsync<PlantByUserDto>("EXEC GetPlantsByUser @UserId", sqlParams);

            //var itemsDapper = await _plantDapperRepository.QueryAsync<PlantByUserDto>(@"SELECT p.Id As PlantId,
            //    p.Name As PlantName,                
            //    p.AddressLine1,
            //    p.AddressLine2,
            //    p.AddressLine3,
            //    p.AddressLine4,
            //    p.RFC
            //    FROM DensoPlants p");

            return new List<PlantByUserDto>(itemsDapper);
        }

        public async Task<List<PlantDto>> GetPlants(bool? isActive = null)
        {
            var plantUsers = await _plantUserRepository.GetAllListAsync();

            var items = await _plantRepository.GetAllListAsync(c => c.IsActive == isActive || isActive == null);
            var itemsDto = ObjectMapper.Map<List<PlantDto>>(items);

            foreach (var item in itemsDto)
            {
                item.TotalUsers = plantUsers.Count(c => c.PlantId == item.Id);
            }

            return new List<PlantDto>(itemsDto);
        }

        public async Task CreateOrUpdatePlant(PlantDto input)
        {
            if (input.Id.HasValue)
            {
                var plant = await _plantRepository.GetAsync(input.Id.Value);
                if (plant != null)
                {
                    plant.Name = input.Name;
                    plant.AddressLine1 = input.AddressLine1;
                    plant.AddressLine2 = input.AddressLine2;
                    plant.AddressLine3 = input.AddressLine3;
                    plant.AddressLine4 = input.AddressLine4;
                    plant.RFC = input.RFC;
                    plant.Sufix = input.Sufix;
                    plant.IsActive = input.IsActive;

                    await _plantRepository.UpdateAsync(plant);
                }
            }
            else
            {
                var plant = ObjectMapper.Map<Plant>(input);
                plant.IsActive = true;

                await _plantRepository.InsertAsync(plant);
            }
        }

        #endregion

        #region Carriers

        public async Task<List<CarrierDto>> GetCarriers(bool? isActive = null, long? id = null)
        {
            var items = await _carrierRepository.GetAll()
                .Include(i => i.DocumentType)
                .Include(i => i.Services).ThenInclude(ti => ti.Service)
                .Include(i => i.NonWorkingDays)
                .Where(c => (c.IsActive == isActive || isActive == null)
                    && (!id.HasValue || c.Id == id))
                //.AsSplitQuery()
                .ToListAsync();

            var itemsDto = ObjectMapper.Map<List<CarrierDto>>(items);

            return itemsDto;
        }

        public async Task<List<CurrentCarrierServicesDto>> GetCarrierServicesforEdit()
        {
            //var services = await _serviceRepository.GetAll().ToListAsync();
            var services = await _serviceRepository.GetAllListAsync(c => c.IsActive == true);

            List<CurrentCarrierServicesDto> newItems = new List<CurrentCarrierServicesDto>();
            foreach (var itemServices in services)
            {
                CurrentCarrierServicesDto itemCCS = new CurrentCarrierServicesDto();
                itemCCS.ServiceId = itemServices.Id;
                itemCCS.ServiceName = itemServices.Name;
                itemCCS.IsActive = false;

                itemCCS.Ground = itemServices.Ground;
                itemCCS.Air = itemServices.Air;
                itemCCS.Sea = itemServices.Sea;

                newItems.Add(itemCCS);                
            }
            return newItems;
        }

        public async Task CreateOrUpdateCarrier(CarrierDto input)
        {
            if (input.Id.HasValue)
            {
                var carrier = await _carrierRepository.GetAsync(input.Id.Value);
                if (carrier != null)
                {
                    //var carrierNonWorkingDays = await _carrierNonWorkingDayRepository.GetAllListAsync(c => c.CarrierId == carrier.Id);
                    //foreach (var carrierNonWorkingDayItem in carrierNonWorkingDays)
                    //{
                    //    carrierNonWorkingDayItem.IsActive = false;
                    //    await _carrierNonWorkingDayRepository.UpdateAsync(carrierNonWorkingDayItem);
                    //}

                    carrier.Name = input.Name;
                    carrier.DocumentTypeId = input.DocumentTypeId;                                        
                    carrier.DivisorNumber = input.DivisorNumber;
                    carrier.IsActive = input.IsActive;
                    carrier.Services = ObjectMapper.Map<List<CarrierService>>(input.Services);
                    carrier.TenantId = AbpSession.TenantId;
                    carrier.NonWorkingDays = ObjectMapper.Map<List<CarrierNonWorkingDay>>(input.NonWorkingDays);
                    carrier.NonWorkingDays.ToList().ForEach(item => item.IsActive = true);

                    await _carrierRepository.UpdateAsync(carrier);

                    await Task.Run(async () =>
                    {
                        List<CarrierService> currentCarrierServices = await _carrierServiceRepository.GetAllListAsync(c => c.CarrierId == input.Id);
                        List<long> newCarrierServicesIds = input.Services.Select(s => s.ServiceId).ToList();
                        List<long> carrierServicesIdsDeleted = currentCarrierServices.Where(cs => cs.ServiceId.HasValue
                            && !newCarrierServicesIds.Contains(cs.ServiceId.Value)).ToList()
                            .Select(s => s.Id).ToList();

                        _carrierServiceRepository.Delete(c => carrierServicesIdsDeleted.Contains(c.Id));

                        var carrierNonWorkingDays = await _carrierNonWorkingDayRepository.GetAllListAsync(c => c.CarrierId == carrier.Id);
                        List<DateTime> newCarrierNonWorkingDaysDates = input.NonWorkingDays.Where(nwd => nwd.IsActive).Select(s => s.NonWorkingDay).ToList();
                        List<CarrierNonWorkingDay> carrierNonWorkingDaysDeleted = carrierNonWorkingDays.Where(cs =>
                            !newCarrierNonWorkingDaysDates.Contains(cs.NonWorkingDay)).ToList();

                        foreach (var carrierNonWorkingDayItem in carrierNonWorkingDaysDeleted)
                        {
                            carrierNonWorkingDayItem.IsActive = false;
                            await _carrierNonWorkingDayRepository.UpdateAsync(carrierNonWorkingDayItem);
                        }
                    });
                }
            }
            else
            {
                var carrier = ObjectMapper.Map<Carrier>(input);
                carrier.IsActive = true;
                await _carrierRepository.InsertAsync(carrier);
            }
        }

        #endregion

        #region HotSheet Reasons

        public async Task<List<HotSheetReasonDto>> GetHotSheetReasons(bool? isActive = null, long? id = null)
        {
            var sqlParams = new { IsActive = isActive, Id = id };
            var itemsDapper = await _departmentDapperRepository.QueryAsync<HotSheetReasonDto>("EXEC GetHotSheetReasons @IsActive, @Id", sqlParams);
            return itemsDapper.ToList();
        }

        public async Task CreateOrUpdateHotSheetReason(HotSheetReasonDto input)
        {
            if (input.Id.HasValue)
            {
                var HotSheetReason = await _HotSheetReasonRepository.GetAsync(input.Id.Value);
                if (HotSheetReason != null)
                {                    
                    HotSheetReason.Description = input.Description;
                    HotSheetReason.BNoticeRMARequired = input.BNoticeRMARequired;
                    HotSheetReason.PictureTechnicalInfoMakerModelSerialNumber = input.PictureTechnicalInfoMakerModelSerialNumber;
                    HotSheetReason.AttachPurchaseOrder = input.AttachPurchaseOrder;
                    HotSheetReason.TechnicalInfoPicture = input.TechnicalInfoPicture;
                    HotSheetReason.AccountingApprovalRequired = input.AccountingApprovalRequired;
                    HotSheetReason.ExcludeTermOfPayment = input.ExcludeTermOfPayment;
                    HotSheetReason.Remittence = input.Remittence;
                    HotSheetReason.NoPayment = input.NoPayment;
                    HotSheetReason.IsActive = input.IsActive;

                    await _HotSheetReasonRepository.UpdateAsync(HotSheetReason);
                }
            }
            else
            {
                var HotSheetReason = ObjectMapper.Map<HotSheetReason>(input);
                HotSheetReason.IsActive = true;

                await _HotSheetReasonRepository.InsertAsync(HotSheetReason);
            }
        }

        #endregion

        #region Customers

        public async Task<List<CustomerDto>> GetCustomers(bool? isActive = null, long? id = null)
        {
            var items = await _customerRepository.GetAll()
                .Include(c => c.Plants)
                .ThenInclude(pc => pc.Contacts)
                .Where(c => (c.IsActive == isActive || isActive == null)
                    && (!id.HasValue || c.Id == id))
                //.AsSplitQuery()
                .ToListAsync();

            var itemsDto = ObjectMapper.Map<List<CustomerDto>>(items);

            return itemsDto;
        }

        public async Task<List<CustomerDto>> GetActiveCustomers()
        {
            var items = await _customerRepository.GetAll()               
                .Where(c => c.IsActive)
                .ToListAsync();

            var itemsDto = ObjectMapper.Map<List<CustomerDto>>(items);

            return itemsDto;
        }

        public async Task<CustomerDto> GetCustomerById(long customerId)
        {
            var item = await _customerRepository.GetAll()
                .Include(c => c.Plants)
                .ThenInclude(pc => pc.Contacts)
                .Where(c => c.Id == customerId)
                .FirstOrDefaultAsync();

            var itemDto = ObjectMapper.Map<CustomerDto>(item);
            return itemDto;
        }

        public async Task CreateOrUpdateCustomer(CustomerDto input)
        {
            if (input.Id.HasValue)
            {
                var customer = await _customerRepository.GetAsync(input.Id.Value);
                if (customer != null)
                {
                    customer.Name = input.Name;
                    customer.AddressLine1 = input.AddressLine1;
                    customer.AddressLine2 = input.AddressLine2;
                    customer.AddressLine3 = input.AddressLine3;
                    customer.AddressLine4 = input.AddressLine4;
                    customer.RFC = input.RFC;
                    customer.State = input.State;
                    customer.Country = input.Country;
                    customer.TaxId = input.TaxId;
                    customer.Contact = input.Contact;
                    customer.FedexCta = input.FedexCta;
                    customer.Phone = input.Phone;
                    customer.ZipCode = input.ZipCode;
                    customer.Payment = input.Payment;
                    customer.IsActive = input.IsActive;

                    await _customerRepository.UpdateAsync(customer);

                    foreach (var plant in input.Plants)
                    {
                        await CreateOrUpdateCustomerPlant(plant);
                    }
                }
            }
            else
            {
                var customer = ObjectMapper.Map<Customer>(input);
                customer.IsActive = true;

                foreach (var plant in customer.Plants)
                {
                    plant.IsActive = true;
                }

                await _customerRepository.InsertAsync(customer);
            }
        }

        public async Task CreateOrUpdateCustomerPlant(CustomerPlantDto input)
        {
            if (input.Id.HasValue)
            {
                var customerPlant = await _customerPlantRepository.GetAsync(input.Id.Value);
                if (customerPlant != null)
                {
                    customerPlant.Name = input.Name;
                    customerPlant.AddressLine1 = input.AddressLine1;
                    customerPlant.AddressLine2 = input.AddressLine2;
                    customerPlant.AddressLine3 = input.AddressLine3;
                    customerPlant.AddressLine4 = input.AddressLine4;
                    customerPlant.RFC = input.RFC;
                    customerPlant.State = input.State;
                    customerPlant.Country = input.Country;
                    customerPlant.ZipCode = input.ZipCode;
                    customerPlant.TaxId = input.TaxId;
                    customerPlant.ShipToNumber = input.ShipToNumber;
                    customerPlant.IsActive = input.IsActive;

                    await _customerPlantRepository.UpdateAsync(customerPlant);

                    foreach (var contact in input.Contacts)
                    {
                        await CreateOrUpdateCustomerPlantContact(contact);
                    }
                }
            }
            else
            {
                var customerPlant = ObjectMapper.Map<CustomerPlant>(input);
                customerPlant.IsActive = true;

                foreach (var contact in customerPlant.Contacts)
                {
                    contact.IsActive = true;
                }

                await _customerPlantRepository.InsertAsync(customerPlant);
            }
        }

        public async Task CreateOrUpdateCustomerPlantContact(CustomerPlantContactDto input)
        {
            if (input.Id.HasValue)
            {
                var customerPlantContact = await _customerPlantContactRepository.GetAsync(input.Id.Value);
                if (customerPlantContact != null)
                {
                    customerPlantContact.ContactName = input.ContactName;
                    customerPlantContact.PhoneNumber = input.PhoneNumber;
                    customerPlantContact.DepartmentOrSection = input.DepartmentOrSection;
                    customerPlantContact.NetNumber = input.NetNumber;
                    customerPlantContact.IsActive = input.IsActive;

                    await _customerPlantContactRepository.UpdateAsync(customerPlantContact);
                }
            }
            else
            {
                var customerPlantContact = ObjectMapper.Map<CustomerPlantContact>(input);
                customerPlantContact.IsActive = true;

                await _customerPlantContactRepository.InsertAsync(customerPlantContact);
            }
        }

        #endregion

        #region HotSheet Terms

        public async Task<List<HotSheetTermDto>> GetHotSheetTerms(bool? isActive = null)
        {
            var items = await _HotSheetTermRepository.GetAllListAsync(c => c.IsActive == isActive || isActive == null);

            var itemsDto = ObjectMapper.Map<List<HotSheetTermDto>>(items);

            return itemsDto;
        }

        public async Task CreateOrUpdateHotSheetTerm(HotSheetTermDto input)
        {
            if (input.Id.HasValue)
            {
                var HotSheetTerm = await _HotSheetTermRepository.GetAsync(input.Id.Value);
                if (HotSheetTerm != null)
                {
                    HotSheetTerm.Name = input.Name;
                    HotSheetTerm.Description = input.Description;
                    HotSheetTerm.IsActive = input.IsActive;

                    await _HotSheetTermRepository.UpdateAsync(HotSheetTerm);
                }
            }
            else
            {
                var HotSheetTerm = ObjectMapper.Map<HotSheetTerm>(input);
                HotSheetTerm.IsActive = true;

                await _HotSheetTermRepository.InsertAsync(HotSheetTerm);
            }
        }

        #endregion

        #region Rma Assignments
        
        public async Task<List<RmaAssignmentDto>> GetRmaAssignments(bool? isActive = null)
        {
            var items = await _rmaAssignmentRepository.GetAllListAsync(c => c.IsActive == isActive || isActive == null);

            var itemsDto = ObjectMapper.Map<List<RmaAssignmentDto>>(items);

            return itemsDto;
        }

        public async Task CreateOrUpdateRmaAssignment(RmaAssignmentDto input)
        {
            if (input.Id.HasValue)
            {
                var rmaAssignment = await _rmaAssignmentRepository.GetAsync(((int)input.Id.Value));
                if (rmaAssignment != null)
                {
                    rmaAssignment.Name = input.Name;
                    rmaAssignment.IsActive = input.IsActive;
                    await _rmaAssignmentRepository.UpdateAsync(rmaAssignment);
                }
            }
            else
            {
                var rmaAssignment = ObjectMapper.Map<RMAAssignment>(input);
                rmaAssignment.IsActive = true;

                await _rmaAssignmentRepository.InsertAsync(rmaAssignment);
            }
        }

        #endregion

        #region Paid By

        public async Task<List<PaidByDto>> GetPaidBy(bool? isActive = null)
        {
            //var query = _paidByRepository.GetAll()
            //    .Where(c => c.IsActive == isActive || isActive == null);

            //query.Select(c => c.PaymentTerms).Load();
            //query.Select(c => c.HotSheetTerms).Load();

            //var items = query.ToList();

            var items = await _paidByRepository.GetAll()
                .Include(c => c.PaymentTerms)
                .Include(c => c.HotSheetTerms)
                //.AsSplitQuery()
                .ToListAsync();

            var itemsDto = ObjectMapper.Map<List<PaidByDto>>(items);

            return itemsDto;
        }

        public async Task CreateOrUpdatePaidBy(PaidByDto input)
        {
            if (input.Id.HasValue)
            {
                var paidBy = await _paidByRepository.GetAsync(((int)input.Id.Value));
                if (paidBy != null)
                {
                    paidBy.Name = input.Name;
                    paidBy.IsActive = input.IsActive;
                    paidBy.PaymentTerms = ObjectMapper.Map<List<PaidByPaymentTerm>>(input.PaymentTerms);
                    paidBy.HotSheetTerms = ObjectMapper.Map<List<PaidByHotSheetTerm>>(input.HotSheetTerms);

                    await _paidByRepository.UpdateAsync(paidBy);

                    await Task.Run(async () =>
                    {
                        var currentPaidByPaymentTerms = await _paidByPaymentTermRepository.GetAllListAsync(c => c.PaidById == input.Id);
                        var currentPaidByHotSheetTerm = await _paidByHotSheetTermRepository.GetAllListAsync(c => c.PaidById == input.Id);

                        List<int> newPaymentTermIds = input.PaymentTerms.Select(s => s.PaymentTermId).ToList();
                        List<int> newHotSheetTermIds = input.HotSheetTerms.Select(s => s.HotSheetTermId).ToList();

                        List<int> paidByPaymentTermsIdsDeleted = currentPaidByPaymentTerms.Where(cs => !newPaymentTermIds.Contains(cs.PaymentTermId)).ToList()
                            .Select(s => s.Id).ToList();
                        List<int> paidByHotSheetTermsIdsDeleted = currentPaidByHotSheetTerm.Where(cs => !newHotSheetTermIds.Contains(cs.HotSheetTermId)).ToList()
                            .Select(s => s.Id).ToList();

                        _paidByPaymentTermRepository.Delete(c => paidByPaymentTermsIdsDeleted.Contains(c.Id));
                        _paidByHotSheetTermRepository.Delete(c => paidByHotSheetTermsIdsDeleted.Contains(c.Id));
                    });
                }
            }
            else
            {
                var paidBy = ObjectMapper.Map<PaidBy>(input);
                paidBy.IsActive = true;

                await _paidByRepository.InsertAsync(paidBy);
            }
        }

        #endregion

        #region Unit Measures

        public async Task<List<UnitMeasureDto>> GetUnitMeasures(bool? isActive = null)
        {
            var items = await _unitMeasureRepository.GetAllListAsync(c => c.IsActive == isActive || isActive == null);
            var itemsDto = ObjectMapper.Map<List<UnitMeasureDto>>(items);            

            return itemsDto;
        }

        public async Task CreateOrUpdateUnitMeasure(UnitMeasureDto input)
        {
            if (input.Id.HasValue)
            {
                var unitMeasure = await _unitMeasureRepository.GetAsync(((int)input.Id.Value));
                if (unitMeasure != null)
                {
                    unitMeasure.Name = input.Name;
                    unitMeasure.DensoCode = input.DensoCode;
                    unitMeasure.SatCode = input.SatCode;
                    unitMeasure.SegroveCode = input.SegroveCode;
                    unitMeasure.IsActive = input.IsActive;
                    await _unitMeasureRepository.UpdateAsync(unitMeasure);
                }
            }
            else
            {
                var unitMeasure = ObjectMapper.Map<UnitMeasure>(input);
                unitMeasure.IsActive = true;

                await _unitMeasureRepository.InsertAsync(unitMeasure);
            }
        }


        #endregion

        #region Staff

        public async Task<List<StaffDto>> GetStaff(bool? isActive = null)
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var items = await _staffRepository.GetAll()
                .Include(i => i.User)
                .Where(c => c.IsActive == isActive || isActive == null)
                .ToListAsync();

                var employeeIds = items.Where(i => i.User != null && i.User.EmployeeId.HasValue).Select(s => s.User.EmployeeId).ToList();
                var employees = await _employeeRepository.GetAllListAsync(e => employeeIds.Contains(e.Id));

                var itemsDto = ObjectMapper.Map<List<StaffDto>>(items);
                foreach (var item in itemsDto)
                {
                    if (item.User != null)
                    {
                        item.User.DensoFullName = item.User.FullName;
                        if (item.User.EmployeeId.HasValue)
                        {
                            var densoEmployeeInfo = employees.FirstOrDefault(e => e.Id == item.User.EmployeeId.Value);
                            if (densoEmployeeInfo != null)
                            {
                                item.User.DensoFullName = densoEmployeeInfo.DensoEmployeeId + " - " + item.User.FullName;
                            }
                        }
                    }
                }

                return itemsDto;
            }
        }

        public async Task<List<ApproverStaffDto>> GetApproversStaff()
        {
            string sqlQuery = "EXEC GetApproversStaffByUser @UserId";
            var sqlParams = new
            {
                UserId = AbpSession.UserId
            };
            var itemsDapper = await _staffDapperRepository.QueryAsync<ApproverStaffDto>(sqlQuery, sqlParams);
            return itemsDapper.ToList();            
        }

        public async Task CreateOrUpdateStaff(StaffDto input)
        {
            if (input.Id.HasValue)
            {
                var staff = await _staffRepository.GetAsync(((int)input.Id.Value));
                if (staff != null)
                {
                    staff.Type = input.Type;
                    staff.UserId = (long)input.UserId;
                    staff.IsActive = input.IsActive;
                    await _staffRepository.UpdateAsync(staff);
                }
            }
            else
            {
                var staff = ObjectMapper.Map<Staff>(input);
                staff.IsActive = true;

                await _staffRepository.InsertAsync(staff);
            }
        }

        #endregion

        #region Product Codes SAT

        public async Task<List<ProductCodeSATDto>> GetProductCodesSat(bool? isActive = null, List<long> productCodeSATIds = null)
        {
            var parameters = new { IsActive = isActive };
            var sql = "SELECT * FROM DensoProductCodesSAT WHERE @IsActive IS NULL OR IsActive = @IsActive";
            var dapperData = await _hotSheetDapperRepository.QueryAsync<ProductCodeSAT>(sql, parameters);

            //var items = await _productCodeSATRepository.GetAllListAsync(c => (c.IsActive == isActive || isActive == null)
            //    && (productCodeSATIds == null || productCodeSATIds.Count == 0 || productCodeSATIds.Contains(c.Id)));
            var itemsDto = ObjectMapper.Map<List<ProductCodeSATDto>>(dapperData.ToList());
            return itemsDto;
        }

        public async Task CreateOrUpdateProductCodeSat(ProductCodeSATDto input)
        {
            if (input.Id.HasValue)
            {
                var productCodeSat = await _productCodeSATRepository.GetAsync(input.Id.Value);
                if (productCodeSat != null)
                {
                    productCodeSat.Code = input.Code;
                    productCodeSat.Description = input.Description;
                    productCodeSat.IsActive = input.IsActive;

                    await _productCodeSATRepository.UpdateAsync(productCodeSat);
                }
            }
            else
            {
                var productCodeSat = ObjectMapper.Map<ProductCodeSAT>(input);
                productCodeSat.IsActive = true;

                await _productCodeSATRepository.InsertAsync(productCodeSat);
            }
        }

        #endregion

        #region Part Numbers

        public async Task<List<PartNumberForSelectDto>> GetPartNumbersForSelect(long? HotSheetShiptId = null)
        {
            var sqlParams = new { IsActive = true, HotSheetShiptId = HotSheetShiptId };
            var itemsDapper = await _partNumberDapperRepository.QueryAsync<PartNumberForSelectDto>("EXEC GetPartNumbersForSelect @IsActive, @HotSheetShiptId", sqlParams);
            return itemsDapper.ToList();
        }

        public async Task<List<PartNumberDto>> GetPartNumbers(bool? isActive = null)
        {
            var items = await _partNumberRepository.GetAll()
                .Include(c => c.ProductCodeSAT)
                .Include(c => c.UnitMeasure)
                //.Include(c => c.OriginCountry)
                .Where(c => c.IsActive == isActive || isActive == null)
                .ToListAsync();
            
            var itemsDto = ObjectMapper.Map<List<PartNumberDto>>(items);
            return itemsDto;
        }

        public async Task<List<PartNumberInternalDto>> GetPartNumbersInternal(bool? isActive = null)
        {
            var items = await _partNumberInternalRepository.GetAll()
                .Include(c => c.ProductCodeSAT)
                .Include(c => c.UnitMeasure)
                .Where(c => c.IsActive == isActive || isActive == null)
                .ToListAsync();

            var itemsDto = ObjectMapper.Map<List<PartNumberInternalDto>>(items);
            return itemsDto;
        }

        public async Task CreateOrUpdatePartNumber(PartNumberDto input)
        {
            if (input.Id.HasValue)
            {
                var partNumber = await _partNumberRepository.GetAsync(input.Id.Value);
                if (partNumber != null)
                {
                    partNumber.Description = input.Description;
                    partNumber.DescriptionSpanish = input.DescriptionSpanish;
                    partNumber.Fraction = input.Fraction;
                    partNumber.OriginCountryId = input.OriginCountryId;
                    partNumber.ProductCodeSATId = input.ProductCodeSATId;
                    partNumber.UnitMeasureId = input.UnitMeasureId;

                    partNumber.OriginCountry = null;
                    partNumber.ProductCodeSAT = null;
                    partNumber.UnitMeasure = null;
                    partNumber.IsActive = input.IsActive;

                    await _partNumberRepository.UpdateAsync(partNumber);
                }
            }
            else
            {
                var partNumber = ObjectMapper.Map<PartNumber>(input);
                partNumber.IsActive = true;

                await _partNumberRepository.InsertAsync(partNumber);
            }
        }

        public async Task CreateOrUpdatePartNumberInternal(PartNumberInternalDto input)
        {
            if (input.Id.HasValue)
            {
                var partNumberInternal = await _partNumberInternalRepository.GetAsync(input.Id.Value);
                if (partNumberInternal != null)
                {
                    partNumberInternal.Description = input.Description;
                    partNumberInternal.DescriptionSpanish = input.DescriptionSpanish;
                    partNumberInternal.Fraction = input.Fraction;
                    partNumberInternal.OriginCountryId = input.OriginCountryId;
                    partNumberInternal.ProductCodeSATId = input.ProductCodeSATId;
                    partNumberInternal.UnitMeasureId = input.UnitMeasureId;
                    partNumberInternal.Weight = input.Weight;
                    partNumberInternal.Price = input.Price;

                    partNumberInternal.OriginCountry = null;
                    partNumberInternal.ProductCodeSAT = null;
                    partNumberInternal.UnitMeasure = null;
                    partNumberInternal.IsActive = input.IsActive;

                    await _partNumberInternalRepository.UpdateAsync(partNumberInternal);
                }
            }
            else
            {
                var partNumberInternal = ObjectMapper.Map<PartNumberInternal>(input);
                partNumberInternal.IsActive = true;

                await _partNumberInternalRepository.InsertAsync(partNumberInternal);
            }
        }
        
        public async Task <List<PartNumberResXlsxDto>>CreateOrUpdatePartNumberXLSX(List<PartNumberXlsxDto> input) {
                        
            IEnumerable<PartNumberResXlsxDto> itemsDapper = null;            
            
            int registro = 0;
            List<PartNumberXlsxDto> newList = new List<PartNumberXlsxDto>();
            PartNumberXlsxDto itemError = new PartNumberXlsxDto(); //Nos ayuda a determinar en que item esta el error.
            string sPartsList = string.Empty;
            try
            {
                for (int i = 0; i < input.Count; i++)
                {
                    registro = i;

                    PartNumberXlsxDto item = new PartNumberXlsxDto();                 

                    item.PartNumber = Regex.Replace(input[i].PartNumber, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");
                    item.DescriptionSpanish = Regex.Replace(input[i].DescriptionSpanish, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");
                    item.DescriptionInglish = Regex.Replace(input[i].DescriptionInglish, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");

                    decimal? dWeight = input[i].Weight;
                    if (dWeight == null)
                    {
                        item.Weight = 0;
                    }
                    else {
                        item.Weight = input[i].Weight;
                    }
                    
                    item.UnitMeasureId = input[i].UnitMeasureId;
                    item.OriginCountryId = input[i].OriginCountryId;
                    item.Fraction = input[i].Fraction;
                    item.ProductCodeSATId = input[i].ProductCodeSATId;
                    newList.Add(item);
                    itemError = item;
                }

                XmlSerializer oSerializer = new XmlSerializer(typeof(List<PartNumberXlsxDto>));
                StringWriter sWriter = new StringWriter();
                XmlWriter writer = XmlWriter.Create(sWriter);
                oSerializer.Serialize(writer, newList);
                sPartsList = sWriter.ToString();
                int iIdx = sPartsList.IndexOf("<PartNumberXlsxDto>");
                sPartsList = "<ArrayOfPartNumberXlsxDto>" + sPartsList.Substring(iIdx);
                sPartsList = sPartsList.Replace(" xmlns=\"http://tempuri.org/\"", "");
                sPartsList = sPartsList.Replace("xsi:nil=\"true\"", ""); //evita error en sql por prefijo no declarado

                var sqlParams = new { iIdUser = 1, PartsXML = sPartsList };
                itemsDapper = await _partNumberDapperRepository.QueryAsync<PartNumberResXlsxDto>("EXEC UpdateOrInsertPartNumberXlsx @iIdUser, @PartsXML", sqlParams);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return  itemsDapper.ToList();

        }


        public async Task<List<PartNumberInternalResXlsxDto>> CreateOrUpdatePartNumberInternalXLSX(List<PartNumberInternalXlsxDto> input)
        {

            IEnumerable<PartNumberInternalResXlsxDto> itemsDapper = null;

            int registro = 0;
            List<PartNumberInternalXlsxDto> newList = new List<PartNumberInternalXlsxDto>();
            PartNumberInternalXlsxDto itemError = new PartNumberInternalXlsxDto(); //Nos ayuda a determinar en que item esta el error.
            string sPartsList = string.Empty;
            try
            {
                for (int i = 0; i < input.Count; i++)
                {
                    registro = i;

                    PartNumberInternalXlsxDto item = new PartNumberInternalXlsxDto();

                    item.PartNumber = Regex.Replace(input[i].PartNumber, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");
                    item.DescriptionSpanish = Regex.Replace(input[i].DescriptionSpanish, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");
                    item.DescriptionInglish = Regex.Replace(input[i].DescriptionInglish, @"[\u0000-\u0008\u000A-\u001F\u0100-\uFFFF]", "");

                    decimal? dWeight = input[i].Weight;
                    if (dWeight == null)
                    {
                        item.Weight = 0;
                    }
                    else
                    {
                        item.Weight = input[i].Weight;
                    }

                    item.UnitMeasureId = input[i].UnitMeasureId;
                    item.OriginCountryId = input[i].OriginCountryId;
                    item.Fraction = input[i].Fraction;
                    item.ProductCodeSATId = input[i].ProductCodeSATId;
                    newList.Add(item);
                    itemError = item;
                }

                XmlSerializer oSerializer = new XmlSerializer(typeof(List<PartNumberInternalXlsxDto>));
                StringWriter sWriter = new StringWriter();
                XmlWriter writer = XmlWriter.Create(sWriter);
                oSerializer.Serialize(writer, newList);
                sPartsList = sWriter.ToString();
                int iIdx = sPartsList.IndexOf("<PartNumberInternalXlsxDto>");
                sPartsList = "<ArrayOfPartNumberInternalXlsxDto>" + sPartsList.Substring(iIdx);
                sPartsList = sPartsList.Replace(" xmlns=\"http://tempuri.org/\"", "");
                sPartsList = sPartsList.Replace("xsi:nil=\"true\"", ""); //evita error en sql por prefijo no declarado

                var sqlParams = new { iIdUser = 1, PartsXML = sPartsList };
                itemsDapper = await _partNumberDapperRepository.QueryAsync<PartNumberInternalResXlsxDto>("EXEC UpdateOrInsertPartNumberInternalXlsx @iIdUser, @PartsXML", sqlParams);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return itemsDapper.ToList();

        }

        #endregion

        public async Task<List<SpecialExpeditedReasonDto>> GetSpecialExpeditedReason(bool? isActive = null, long? id = null)
        {
            var items = await _specialExpeditedReasonRepository.GetAllListAsync(c => (c.IsActive == isActive || isActive == null)
                    && (!id.HasValue || c.Id == id));
            var itemsDto = ObjectMapper.Map<List<SpecialExpeditedReasonDto>>(items);

            return new List<SpecialExpeditedReasonDto>(itemsDto);
        }

        public async Task<List<PackagingDto>> GetPackaging(bool? isActive = null)
        {
            var items = await _packagingRepository.GetAllListAsync(c => c.IsActive == isActive || isActive == null);
            var itemsDto = ObjectMapper.Map<List<PackagingDto>>(items);

            return new List<PackagingDto>(itemsDto);
        }

        public async Task<List<DocumentStatusDto>> GetStatus()
        {
            var items = await _documentStatusRepository.GetAllListAsync();
            var itemsDto = ObjectMapper.Map<List<DocumentStatusDto>>(items);

            return new List<DocumentStatusDto>(itemsDto);
        }

        #region Part Number Prices

        public async Task<List<PartNumberPriceDto>> GetPartNumberPrices(bool? isActive = null)
        {
            var items = await _partNumberPriceRepository.GetAll()
                .Include(c => c.Customer)
                //.Include(c => c.PartNumber)                
                .Where(c => c.IsActive == isActive || isActive == null)
                .ToListAsync();
            var itemsDto = ObjectMapper.Map<List<PartNumberPriceDto>>(items);

            return new List<PartNumberPriceDto>(itemsDto);
        }

        public async Task<List<PartNumberPriceInternalDto>> GetPartNumberPricesInternal(bool? isActive = null)
        {
            var sqlParams = new { IsActive = isActive };
            var itemsDapper = await _partNumberDapperRepository.QueryAsync<PartNumberPriceInternalDto>("EXEC GetPartNumberPricesInternal @IsActive", sqlParams);
            return itemsDapper.ToList();
        }

        public async Task CreateOrUpdatePartNumberPrice(PartNumberPriceDto input)
        {
            if (input.Id.HasValue)
            {
                var partNumberPrice = await _partNumberPriceRepository.GetAsync(input.Id.Value);
                if (partNumberPrice != null)
                {
                    partNumberPrice.CustomerId = input.CustomerId;
                    partNumberPrice.PartNumberId = input.PartNumberId;
                    partNumberPrice.UnitPrice = input.UnitPrice;
                    partNumberPrice.Currency = input.Currency;
                    partNumberPrice.PublishDate = input.PublishDate;
                    partNumberPrice.IsActive = input.IsActive;

                    partNumberPrice.Customer = null;
                    //partNumberPrice.PartNumberInternal = null;

                    await _partNumberPriceRepository.UpdateAsync(partNumberPrice);
                }
            }
            else
            {
                var partNumberPrice = ObjectMapper.Map<PartNumberPrice>(input);
                partNumberPrice.IsActive = true;

                await _partNumberPriceRepository.InsertAsync(partNumberPrice);
            }
        }

        public async Task CreateOrUpdatePartNumberPriceInternal(PartNumberPriceInternalDto input)
        {
            if (input.Id.HasValue)
            {
                var partNumberPriceInternal = await _partNumberPriceInternalRepository.GetAsync(input.Id.Value);
                if (partNumberPriceInternal != null)
                {
                    partNumberPriceInternal.CustomerId = input.CustomerId;
                    partNumberPriceInternal.PartNumberInternalId = input.PartNumberInternalId;
                    partNumberPriceInternal.UnitPrice = input.UnitPrice;
                    partNumberPriceInternal.Currency = input.Currency;
                    partNumberPriceInternal.PublishDate = input.PublishDate;
                    partNumberPriceInternal.IsActive = input.IsActive;

                    partNumberPriceInternal.Customer = null;
                    partNumberPriceInternal.PartNumber = null;

                    await _partNumberPriceInternalRepository.UpdateAsync(partNumberPriceInternal);
                }
            }
            else
            {
                var partNumberPriceInternal = ObjectMapper.Map<PartNumberPriceInternal>(input);
                partNumberPriceInternal.IsActive = true;

                await _partNumberPriceInternalRepository.InsertAsync(partNumberPriceInternal);
            }
        }

        #endregion

        #region Help Info

        public async Task<List<HelpInfoDto>> GetHelpInfo()
        {
            var query = _helpInfoRepository.GetAll();
            query.Select(c => c.HelpInfoField).Load();
            var items = query.ToList();

            var itemsDto = ObjectMapper.Map<List<HelpInfoDto>>(items);

            return new List<HelpInfoDto>(itemsDto);
        }

        public async Task<List<HelpInfoFieldDto>> GetHelpInfoFields()
        {
            var items = await _helpInfoFieldRepository.GetAllListAsync();
            var itemsDto = ObjectMapper.Map<List<HelpInfoFieldDto>>(items);

            return new List<HelpInfoFieldDto>(itemsDto);
        }

        public async Task CreateOrUpdateHelpInfo(HelpInfoDto input)
        {
            if (input.Id.HasValue)
            {
                var helpInfo = await _helpInfoRepository.GetAsync(input.Id.Value);
                if (helpInfo != null)
                {
                    helpInfo.HelpInfoFieldId = input.HelpInfoFieldId;
                    helpInfo.HelpInfoField = null;
                    helpInfo.HelpTextEnglish = input.HelpTextEnglish;
                    helpInfo.HelpTextSpanish = input.HelpTextSpanish;
                    helpInfo.IsActive = input.IsActive;

                    await _helpInfoRepository.UpdateAsync(helpInfo);
                }
            }
            else
            {
                var helpInfo = ObjectMapper.Map<HelpInfo>(input);
                helpInfo.IsActive = true;

                await _helpInfoRepository.InsertAsync(helpInfo);
            }
        }

        #endregion

        #region Employees

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var items = await _employeeRepository.GetAll()
                .Include(c => c.Department)
                .Include(c => c.EmployeeType)
                .Include(c => c.EmployeeLevel)
                .Include(c => c.Plant)
                .Include(c => c.EmployeePosition)
                .ToListAsync();

            var itemsDto = ObjectMapper.Map<List<EmployeeDto>>(items);

            return new List<EmployeeDto>(itemsDto);
        }

        public async Task<List<EmployeeDto>> GetEmployeesList()
        {
            var itemsDapper = await _departmentDapperRepository.QueryAsync<EmployeeDto>("SELECT * FROM DensoEmployees");
            foreach (var item in itemsDapper)
            {
                item.FullName = item.DensoEmployeeId + " - " + item.Name + ' ' + item.Surnames;
            }
            return itemsDapper.ToList();
        }

        public async Task<EmployeeOptionsDto> GetEmployeeOptions()
        {
            var dataItems = new EmployeeOptionsDto();
            dataItems.Types = await _employeeTypeRepository.GetAll().ToListAsync();
            dataItems.Levels = await _employeeLevelRepository.GetAll().ToListAsync();
            dataItems.Positions = await _employeePositionRepository.GetAll().ToListAsync();

            var itemsDeptos = await _departmentRepository.GetAllListAsync();
            var itemsDtoDeptos = ObjectMapper.Map<List<DepartmentDto>>(itemsDeptos);

            foreach (var item in itemsDtoDeptos)
            {
                item.FullName = item.Id + " - " + item.Name;
            }

            dataItems.Departments = itemsDtoDeptos;

            var itemsPlants = await _plantRepository.GetAllListAsync();
            dataItems.Plants = ObjectMapper.Map<List<PlantDto>>(itemsPlants);            

            return dataItems;
        }

        public async Task<List<EmployeeType>> GetEmployeeTypes()
        {
            var items = await _employeeTypeRepository.GetAll()
                .ToListAsync();

            return items;
        }

        public async Task<List<EmployeeLevel>> GetEmployeeLevels()
        {
            var items = await _employeeLevelRepository.GetAll()
                .ToListAsync();

            return items;
        }

        public async Task<List<EmployeePosition>> GetEmployeePositions()
        {
            var items = await _employeePositionRepository.GetAll()
                .ToListAsync();

            return items;
        }

        public async Task CreateOrUpdateEmployee(EmployeeDto input)
        {
            if (input.Id.HasValue)
            {
                var employee = await _employeeRepository.GetAsync(input.Id.Value);
                if (employee != null)
                {
                    employee.DensoEmployeeId = input.DensoEmployeeId;
                    employee.Credential = input.Credential;
                    employee.Name = input.Name;
                    employee.Surnames = input.Surnames;
                    employee.Rfc = input.Rfc;
                    employee.BirthDate = input.BirthDate;
                    employee.Nss = input.Nss;
                    employee.Curp = input.Curp;
                    employee.DepartmentId = input.DepartmentId;
                    employee.TypeId = input.TypeId;
                    employee.LevelId = input.LevelId;
                    employee.PlantId = input.PlantId;
                    employee.EntryDate = input.EntryDate;
                    employee.PositionId = input.PositionId;
                    employee.Extras = input.Extras;
                    employee.NotRequiredAHE = input.NotRequiredAHE;
                    employee.Supervisor = input.Supervisor;
                    employee.Subsidy = input.Subsidy;
                    employee.PositionLevel = input.PositionLevel;
                    employee.AddressLine1 = input.AddressLine1;
                    employee.AddressLine2 = input.AddressLine2;
                    employee.AddressLine3 = input.AddressLine3;
                    employee.AddressLine4 = input.AddressLine4;
                    employee.EmailAddress = input.EmailAddress;

                    employee.IsActive = input.IsActive;

                    await _employeeRepository.UpdateAsync(employee);
                }
            }
            else
            {
                var employee = ObjectMapper.Map<Employee>(input);
                employee.IsActive = true;

                employee.Department = null;
                employee.Plant = null;
                employee.EmployeeType = null;
                employee.EmployeeLevel = null;
                employee.EmployeePosition = null;

                long employeeIdCreated = await _employeeRepository.InsertAndGetIdAsync(employee);

                var user = await _userRegistrationManager.RegisterAsync(
                    employee.Name,
                    employee.Surnames,
                    employee.EmailAddress,
                    employee.Credential,
                    "123qwe",
                    false,
                    employeeIdCreated
                );

                if (employee.DepartmentId.HasValue)
                {
                    await _departmentUserRepository.InsertAsync(new DepartmentUser { UserId = user.Id, DepartmentId = employee.DepartmentId.Value });
                }

                if (employee.PlantId.HasValue)
                {
                    await _plantUserRepository.InsertAsync(new PlantUser { UserId = user.Id, PlantId = employee.PlantId.Value });
                }
            }
        }

        #endregion


        #region Notices

        public async Task<List<NoticeDto>> GetNotices()
        {
            var items = await _noticeRepository.GetAll()
                .ToListAsync();

            var itemsDto = ObjectMapper.Map<List<NoticeDto>>(items);

            return new List<NoticeDto>(itemsDto);
        }

        public async Task<List<NoticeDto>> GetNoticesToDisplay()
        {
            var items = await _noticeRepository.GetAll()
                .Where(c => c.NoticeDay >= DateTime.Now
                    && DateTime.Now >= c.NoticeDay.AddDays(-c.AnticipationDays)
                    && c.IsActive)
                .ToListAsync();

            var itemsDto = ObjectMapper.Map<List<NoticeDto>>(items);

            return new List<NoticeDto>(itemsDto);
        }

        public async Task CreateOrUpdateNotice(NoticeDto input)
        {
            if (input.Id.HasValue)
            {
                var notice = await _noticeRepository.GetAsync(input.Id.Value);
                if (notice != null)
                {
                    notice.Message = input.Message;
                    notice.NoticeDay = input.NoticeDay;
                    notice.AnticipationDays = input.AnticipationDays;                    
                    notice.IsActive = input.IsActive;

                    await _noticeRepository.UpdateAsync(notice);
                }
            }
            else
            {
                var notice = ObjectMapper.Map<Notice>(input);
                notice.IsActive = true;

                await _noticeRepository.InsertAsync(notice);
            }
        }

        #endregion

        public async Task<List<PaymentStatus>> GetPaymentStatus()
        {
            var items = await _paymentStatusRepository.GetAll()
                .ToListAsync();

            return items;
        }
    }
}
