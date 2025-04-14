using Microsoft.Extensions.Configuration;

namespace Denso.HotSheet.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
