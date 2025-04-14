using Abp.Authorization;
using Abp.Dapper.Repositories;
using Abp.Domain.Repositories;
using Denso.HotSheet.HotSheet;
using Denso.HotSheet.Sheets.Dto;
using Denso.HotSheet.Surveys.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denso.HotSheet.Surveys
{
    [AbpAuthorize]
    public class SurveyAppService : HotSheetAppServiceBase, ISurveyAppService
    {
        private readonly IRepository<Survey, long> _surveyRepository;
        private readonly IDapperRepository<Survey, long> _surveyDapperRepository;

        //private readonly IRepository<HotSheets, long> _hotSheetsRepository;
        //private readonly IDapperRepository<HotSheets, long> _hotSheetsDapperRepository;        

        public SurveyAppService(
            IRepository<Survey, long> surveyRepository,
            IDapperRepository<Survey, long> surveyDapperRepository

            //IRepository<HotSheets, long> hotSheetsRepository,
            //IDapperRepository<HotSheets, long> hotSheetsDapperRepository
        )
        {
            _surveyRepository = surveyRepository;
            _surveyDapperRepository = surveyDapperRepository;

            //_hotSheetsDapperRepository = hotSheetsDapperRepository;
            //_hotSheetsRepository = hotSheetsRepository;

        }

        public async Task<List<SurveyDto>> GetSurveys(GetSurveysInput input)
        {
            string sqlQuery = "EXEC GetSurveys @UserId, @ShippingCode, @CreationDate, @Qualification";
            var sqlParams = new
            {
                UserId = input.UserId,
                ShippingCode = input.ShippingCode,
                CreationDate = input.CreationDate,
                Qualification = input.Qualification,
            };
            
            var itemsDapper = await _surveyDapperRepository.QueryAsync<SurveyDto>(sqlQuery, sqlParams);
            return itemsDapper.ToList();
        }

        public async Task SaveSurvey(SurveyDto input)
        {
            var survey = ObjectMapper.Map<Survey>(input);
            survey.CreatorUserId = AbpSession.UserId;

            await _surveyRepository.InsertAsync(survey);
        }

        //public async Task<List<HotSheetsItemDto>> GetHotSheets()
        //{
        //    string sqlQuery = "EXEC GetHotSheets @UserId";
        //    var sqlParams = new
        //    {
        //        UserId = AbpSession.UserId,
        //    };

        //    try
        //    {
        //        //var itemsDapper = await _surveyDapperRepository.QueryAsync<HotSheetsItemDto>(sqlQuery, sqlParams);

        //        var itemsDapper = await _hotSheetsDapperRepository.QueryAsync<HotSheetsItemDto>(sqlQuery, sqlParams);

        //        return itemsDapper.ToList();
        //    }
        //    catch (System.Exception ex)
        //    {

        //        throw ex;
        //    }
            

        //}


        //public async Task<List<HotSheetsItemDto>> GetHotSheetById(long HotSheetId)
        //{
        //    string sqlQuery = "EXEC GetHotSheetById @HotSheetId, @UserId";
        //    var sqlParams = new
        //    {
        //        HotSheetId = HotSheetId,
        //        UserId = AbpSession.UserId,
        //    };
        //    var itemsDapper = await _surveyDapperRepository.QueryAsync<HotSheetsItemDto>(sqlQuery, sqlParams);

        //    return itemsDapper.ToList();
        //}
    }
}
