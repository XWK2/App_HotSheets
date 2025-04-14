using AutoMapper;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.Catalogs;
using Denso.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Configuration;
using Denso.HotSheet.Configuration.Dto;
using Denso.HotSheet.Organization;
using Denso.HotSheet.HotSheet;
using Denso.HotSheet.Sheets.Dto;
using Denso.HotSheet.Surveys;
using Denso.HotSheet.Surveys.Dto;
using Denso.HotSheet.Users.Dto;
using System.Data.SqlTypes;

namespace Denso.HotSheet
{
    internal static class HotSheetCustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            // Users
            configuration.CreateMap<User, UserDto>()
                .ForMember(dest => dest.Departments, act => act.Ignore())
                .ForMember(dest => dest.Plants, act => act.Ignore());

            //Configuration
            configuration.CreateMap<SettingsParameters, SettingsParametersDto>().ReverseMap();

            // Catalogs
            configuration.CreateMap<DocumentType, DocumentTypeDto>().ReverseMap();
            configuration.CreateMap<Plant, PlantDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Id + " - " + src.Name))
                .ReverseMap();
            configuration.CreateMap<Carrier, CarrierDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Id + " - " + src.Name))
                .ReverseMap();
            configuration.CreateMap<CarrierService, CarrierServiceDto>().ReverseMap()
                .ForMember(dest => dest.Service, opt => opt.Ignore());
            configuration.CreateMap<CarrierNonWorkingDay, CarrierNonWorkingDayDto>().ReverseMap();
            configuration.CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Id + " - " + src.Name))
                .ReverseMap();
            configuration.CreateMap<HotSheetReason, HotSheetReasonDto>().ReverseMap();
            configuration.CreateMap<PaymentTerm, PaymentTermDto>()
                .ForMember(dest => dest.Warning1CompanyIds, act => act.Ignore())
                .ForMember(dest => dest.Warning2CompanyIds, act => act.Ignore())
                .ReverseMap();
            configuration.CreateMap<PaymentTermCarrier, PaymentTermCarrierDto>().ReverseMap();
            configuration.CreateMap<Department, DepartmentDto>().ReverseMap();
            configuration.CreateMap<PartNumber, PartNumberDto>()
                .ForMember(dest => dest.FullNumber, opt => opt.MapFrom(src => src.Number + " - " + src.Description + " / " + src.DescriptionSpanish))
                .ReverseMap();
            configuration.CreateMap<UnitMeasure, UnitMeasureDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.Id + " - " + src.Name + " - Denso: " + src.DensoCode + ", SAT: " + src.SatCode + ", Segrove: " + src.SegroveCode))
                .ReverseMap();

            //Nuevos LHH HotSheet
            configuration.CreateMap<TransportMode, TransportModeDto>();
            configuration.CreateMap<StatusHotSheet, StatusHotSheetDto>();
            configuration.CreateMap<ShortageShift, ShortageShiftDto>();
            
            //

            configuration.CreateMap<ProductCodeSAT, ProductCodeSATDto>().ReverseMap();
            configuration.CreateMap<Staff, StaffDto>().ReverseMap();
            configuration.CreateMap<PaidBy, PaidByDto>().ReverseMap();
            configuration.CreateMap<RMAAssignment, RmaAssignmentDto>().ReverseMap();
            configuration.CreateMap<SpecialExpeditedReason, SpecialExpeditedReasonDto>().ReverseMap();
            configuration.CreateMap<Packaging, PackagingDto>().ReverseMap();
            configuration.CreateMap<RMAAssignment, RmaAssignmentDto>().ReverseMap();
            configuration.CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.DensoCustomerId + " - " + src.Name))
                .ReverseMap();
            configuration.CreateMap<CustomerPlant, CustomerPlantDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.ShipToNumber + " - " + src.Name))
                .ReverseMap();
            configuration.CreateMap<File, FileDto>().ReverseMap();
            configuration.CreateMap<DocumentStatus, DocumentStatusDto>().ReverseMap();
            configuration.CreateMap<PartNumberPrice, PartNumberPriceDto>().ReverseMap();
            configuration.CreateMap<CustomerPlantContact, CustomerPlantContactDto>().ReverseMap();
            configuration.CreateMap<Country, CountryDto>().ReverseMap();
            configuration.CreateMap<Country, CountryDto>().ReverseMap();
            configuration.CreateMap<HelpInfo, HelpInfoDto>().ReverseMap();
            configuration.CreateMap<HelpInfoField, HelpInfoFieldDto>().ReverseMap();
            configuration.CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : ""))
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType != null ? src.EmployeeType.Name : ""))
                .ForMember(dest => dest.EmployeeLevel, opt => opt.MapFrom(src => src.EmployeeLevel != null ? src.EmployeeLevel.Name : ""))
                .ForMember(dest => dest.PlantName, opt => opt.MapFrom(src => src.Plant != null ? src.Plant.Name : ""))
                .ForMember(dest => dest.EmployeePosition, opt => opt.MapFrom(src => src.EmployeePosition != null ? src.EmployeePosition.Name : ""))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.DensoEmployeeId.ToString() + " - " + src.Name + " " + src.Surnames))
                .ReverseMap();
            configuration.CreateMap<Currency, CurrencyDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.DensoCode + " - " + src.Name))
                .ReverseMap();
            configuration.CreateMap<PaidByHotSheetTerm, PaidByHotSheetTermDto>().ReverseMap();
            configuration.CreateMap<PaidByPaymentTerm, PaidByPaymentTermDto>().ReverseMap();
            configuration.CreateMap<PartNumberInternal, PartNumberInternalDto>()
                .ForMember(dest => dest.FullNumber, opt => opt.MapFrom(src => src.Number + " - " + src.Description + " / " + src.DescriptionSpanish))
                .ReverseMap();
            configuration.CreateMap<PartNumberPriceInternal, PartNumberPriceInternalDto>().ReverseMap();
            configuration.CreateMap<Notice, NoticeDto>().ReverseMap();

            // Hot Sheet
            configuration.CreateMap<HotSheets, HotSheetsDto>().ReverseMap();
            configuration.CreateMap<HotSheets, HotSheetsItemDto>().ReverseMap();
            configuration.CreateMap<HotSheetsComments, HotSheetsCommetsDto>().ReverseMap();

            configuration.CreateMap<HotSheetsShip, HotSheetShipDto>().ReverseMap();
            configuration.CreateMap<HotSheetShipProduct, HotSheetShipProductDto>()
                .ForMember(dest => dest.ProductSATCode, act => act.Ignore())
                .ForMember(dest => dest.PartNumberSelected,
                    // -0: is not internal, -1: is internal
                    opt => opt.MapFrom(src => src.PartNumberId.HasValue ? src.PartNumberId.ToString() + "-0" : src.PartNumberInternalId.ToString() + "-1"))
                .ReverseMap();
            configuration.CreateMap<HotSheetShipPackaging, HotSheetShipPackagingDto>().ReverseMap();
            configuration.CreateMap<HotSheetShipHistory, HotSheetHistoryDto>().ReverseMap();
            configuration.CreateMap<HotSheetsShip, HotSheetShipItemDto>().ReverseMap();
            configuration.CreateMap<HotSheetsShip, HotSheetShipInfoDto>()
                .ForMember(dest => dest.Folio, opt => opt.MapFrom(src => src.Plant != null ? src.Id + "-" + src.Plant.Sufix : ""));
            configuration.CreateMap<HotSheetShipManifest, HotSheetShipManifestDto>().ReverseMap();

            //Surveys
            configuration.CreateMap<Survey, SurveyDto>().ReverseMap();
        }
    }
}
