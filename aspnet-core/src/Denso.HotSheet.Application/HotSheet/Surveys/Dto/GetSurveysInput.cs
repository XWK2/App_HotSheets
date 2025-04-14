using System;

namespace Denso.HotSheet.Surveys.Dto
{
    public class GetSurveysInput
    {
        public long? UserId { get; set; }
        public string ShippingCode { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Qualification { get; set; }
    }
}
