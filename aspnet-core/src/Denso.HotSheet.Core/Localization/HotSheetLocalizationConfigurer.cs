using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Denso.HotSheet.Localization
{
    public static class HotSheetLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(HotSheetConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(HotSheetLocalizationConfigurer).GetAssembly(),
                        "Denso.HotSheet.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
