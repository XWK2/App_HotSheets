using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Denso.HotSheet.Catalogs
{
    [Table("DensoCountries")]
    public class Country : Entity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string NameSpanish { get; set; }

        [StringLength(5)]
        public string DensoCode { get; set; }

        [StringLength(5)]
        public string SatCode { get; set; }

        [StringLength(5)]
        public string SegroveCode { get; set; }

        public bool IsActive { get; set; }
    }
}
