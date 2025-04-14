namespace Denso.HotSheet.Tenants.Dto
{
    public class DensoSettingsEditDto
    {
        public DensoAS400SettingsEditDto AS400 { get; set; }
        public DensoInterfacesSettingsEditDto Interfaces { get; set; }
        public DensoGeneralSettingsEditDto General { get; set; }
    }

    public class DensoAS400SettingsEditDto
    {
        public string DataSource { get; set; }

        public string UserID { get; set; }

        public string Password { get; set; }
    }

    public class DensoInterfacesSettingsEditDto
    {
        public string DaysForReminders { get; set; }
        public string EmailsAddressToNotify { get; set; }
    }

    public class DensoGeneralSettingsEditDto
    {
        public string DaysInAdvanceForNonWorkDaysNotification { get; set; }

        public string DefaulHelpUrl { get; set; }
    }
}
