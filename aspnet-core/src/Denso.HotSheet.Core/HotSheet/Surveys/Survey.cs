using Abp.Domain.Entities.Auditing;
using Denso.HotSheet.HotSheet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Surveys
{
    [Table("DensoSurveys")]
    public class Survey : AuditedEntity<long>
    {
        [StringLength(100)]
        public string Page { get; set; }

        public string AnswerQuestion1 { get; set; }
        public string AnswerQuestion2 { get; set; }
        public string AnswerQuestion3 { get; set; }
        public string AnswerQuestion4 { get; set; }
        public string AnswerQuestion5 { get; set; }
        public string AnswerQuestion6 { get; set; }
        public string AnswerQuestion7 { get; set; }

        [ForeignKey("HotSheetShipId")]
        public long? HotSheetShipId { get; set; }
        public HotSheetsShip HotSheetShip { get; set; }
    }
}
