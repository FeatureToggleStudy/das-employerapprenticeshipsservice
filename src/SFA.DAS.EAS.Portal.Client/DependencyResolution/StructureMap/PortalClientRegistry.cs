using SFA.DAS.AutoConfiguration.DependencyResolution;
using StructureMap;

namespace SFA.DAS.EAS.Portal.Client.DependencyResolution.StructureMap
{
    public class PortalClientRegistry : Registry
    {
        public PortalClientRegistry()
        {
            IncludeRegistry<AutoConfigurationRegistry>();
            IncludeRegistry<ConfigurationRegistry>();
            IncludeRegistry<ApplicationRegistry>();
            IncludeRegistry<ReadStoreDataRegistry>();
        }
    }
}