namespace Denso.HotSheet.Tenants.Dto
{
    public class TenantSettingsEditDto
    {
        public TenantEmailSettingsEditDto Email { get; set; }
        public DensoSettingsEditDto Denso { get; set; }
    }
}
