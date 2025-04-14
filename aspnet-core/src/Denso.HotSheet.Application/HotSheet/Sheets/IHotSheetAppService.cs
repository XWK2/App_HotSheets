using Abp.Application.Services;
using Denso.HotSheet.Catalogs.Dto;
using Denso.HotSheet.Files;
using Denso.HotSheet.HotSheet.DBServices.Dto;
using Denso.HotSheet.Sheets.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Denso.HotSheet.Sheets
{
    public interface IHotSheetAppService : IApplicationService
    {
        Task<List<HotSheetsItemDto>> GetHotSheets(int StatusHS);
        Task<HotSheetsItemDetailDto> GetHotSheetById(long HotSheetId);

        Task CreateOrUpdateHotSheet(HotSheetsDto input);

        //Task<List<HotSheetShipItemDto>> GetHotSheetShip(GetHotSheeShiptInput input);
        //Task<List<HotSheetShipItemDto>> GetHotSheetShipReports(GetHotSheeShiptInput input);
        //Task<HotSheetShipInfoDto> GetHotSheetShipByIdEncrypted(string idEncrypted);
        //Task<HotSheetShipInfoDto> GetHotSheetShipByIdInfo(long HotSheetShiptId);
        //Task<HotSheetShipItemDto> GetHotSheetShipById(long HotSheetShipId);
        ////Task<List<HotSheetDto>> GetHotSheetOld(GetHotSheetInput input);
        ////Task<HotSheetDto> GetHotSheetByIdOld(long HotSheetShiptId);
        //Task<HotSheetShipDto> CreateOrUpdateHotSheetShip(HotSheetShipDto input);
        //Task<List<FileDto>> GetHotSheetShipFiles(long HotSheetShiptId);

        Task<List<FileDto>> GetHotSheetFiles(long HotSheetId);

        Task<HotSheetsCommetsDto> CreateOrUpdateHotSheetComments(HotSheetsCommetsDto input);

        //Task CreateOrUpdateHotSheetComments(HotSheetsCommetsDto input);

        //Task<List<HotSheetShipItemDto>> GetHotSheetShipPendingForApproval();

        //Task CancelHotSheetShip(long HotSheetShiptId);
        //Task ExportHotSheetShipToAS400(long HotSheetShiptId);

        //Task<List<ApproverDto>> GetApprovers();

        //Task ApproveHotSheetShip(ApproveHotSheetShipInput input);

        //Task SendForApproval(long HotSheetShiptId, string idEncrypted);        

        //Task<DashboardData> GetDashboardData(int? year);

        //Task ResetHotSheetShip(long HotSheetShiptId);

        //Task ChangeApproversHotSheetShip(ChangeApproversHotSheetInput input);

        //Task<List<DBServicesLogsDto>> GetLogsInterfacesServices(string DateStart, string DateEnd);

        //Task<List<CarrierNonWorkingDayDto>> GetNonWorkDaysToNotify();
        //Task DeleteFile(long fileId);

        //Task<TrackingScrapSales> GetTrackingScrapSales(GetTrackingScrapSalesInput input);

        //Task<List<HotSheetShipGuidesResXlsxDto>> UpdateHotSheetShipGuidesXLSX(List<HotSheetShipGuidesXlsxDto> input);
    }
        
}
