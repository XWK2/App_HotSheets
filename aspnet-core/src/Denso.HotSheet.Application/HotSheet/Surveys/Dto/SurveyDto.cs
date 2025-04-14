using Abp.Application.Services.Dto;
using System;

namespace Denso.HotSheet.Surveys.Dto
{
    public class SurveyDto : EntityDto<long?>
    {
        public string Page { get; set; }
        public string AnswerQuestion1 { get; set; }
        public string AnswerQuestion2 { get; set; }
        public string AnswerQuestion3 { get; set; }
        public string AnswerQuestion4 { get; set; }
        public string AnswerQuestion5 { get; set; }
        public string AnswerQuestion6 { get; set; }
        public string AnswerQuestion7 { get; set; }

        public long? HotSheetShipId { get; set; }
        public string HotSheetShipFolio { get; set; }

        public DateTime CreationTime { get; set; }
        public long CreatorUserId { get; set; }
        public string CreatorFullName { get; set; }
    }
}
