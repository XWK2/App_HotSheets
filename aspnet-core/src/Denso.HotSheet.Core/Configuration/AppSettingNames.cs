namespace Denso.HotSheet.Configuration
{
    public static class AppSettingNames
    {
        public const string UiTheme = "App.UiTheme";

        public static class Email
        {
            public const string UseHostDefaultEmailSettings = "App.Email.UseHostDefaultEmailSettings";
        }

        public static class DensoHotSheet
        {
            public static class AS400
            {
                public const string DataSource = "Denso.HotSheet.AS400.DataSource";
                public const string UserID = "Denso.HotSheet.AS400.UserID";
                public const string Password = "Denso.HotSheet.AS400.Password";                
            }

            public static class Interfaces
            {
                public const string DaysForReminders = "Denso.HotSheet.DaysForReminders";
                public const string EmailsAddressToNotify = "Denso.HotSheet.EmailsAddressToNotify";
            }

            public static class General
            {
                public const string DaysInAdvanceForNonWorkDaysNotification = "Denso.HotSheet.DaysInAdvanceForNonWorkDaysNotification";

                public const string DefaulHelpUrl = "Denso.HotSheet.DefaulHelpUrl";
            }            
        }
    }
}
