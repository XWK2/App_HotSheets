using Denso.HotSheet.Debugging;

namespace Denso.HotSheet
{
    public class HotSheetConsts
    {
        public const string LocalizationSourceName = "HotSheet";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "fbc1af70f71e45a3bec941732c69114b";
    }
}
