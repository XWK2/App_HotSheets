using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Denso.HotSheet.Catalogs.Dto
{
    [AutoMapFrom(typeof(Customer))]
    public class CustomerDto : EntityDto<long?>
    {
        public long? DensoCustomerId { get; set; }
        public string Name { get; set; }        
        public string RFC { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public bool Payment { get; set; }
        public bool IsActive { get; set; }              
        public string Contact { get; set; }        
        public string Phone { get; set; }
        public string FedexCta { get; set; }

        public virtual IList<CustomerPlantDto> Plants { get; set; } = new List<CustomerPlantDto>();

        public string State { get; set; }        
        public string Country { get; set; }        
        public string ZipCode { get; set; }
        public string TaxId { get; set; }

        public string FullName { get; set; }
    }

    [AutoMapFrom(typeof(CustomerPlant))]
    public class CustomerPlantDto : EntityDto<long?>
    {
        public long CustomerId { get; set; }       
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string RFC { get; set; }
        public string State { get; set; }
        public string Country { get; set; }        
        public string ZipCode { get; set; }
        public string TaxId { get; set; }
        public bool IsActive { get; set; }

        public int ShipToNumber { get; set; }
        public string FullName { get; set; }

        public virtual IList<CustomerPlantContactDto> Contacts { get; set; } = new List<CustomerPlantContactDto>();
    }
}
