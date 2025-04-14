using Abp.Application.Services;
using Denso.HotSheet.Sheets.Dto;
using Denso.HotSheet.Surveys.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Denso.HotSheet.Surveys
{
    public interface ISurveyAppService : IApplicationService
    {
        Task<List<SurveyDto>> GetSurveys(GetSurveysInput input);
        Task SaveSurvey(SurveyDto input);

        //Task<List<HotSheetsItemDto>> GetHotSheets();
        //Task<List<HotSheetsItemDto>> GetHotSheetById(long HotSheetId);
    }
}
