using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.Application.Environment;
using JetBrains.Rider.Backend;
using JetBrains.Rider.Backend.Env;

namespace Rapicgen.Rider
{
    [ZoneDefinition]
    public interface IRestApiClientCodeGeneratorZone : IZone
    {
    }

    [ZoneActivator]
    public class RestApiClientCodeGeneratorZoneActivator : IActivate<IRiderBackendEnvZone>, IActivate<IRestApiClientCodeGeneratorZone>
    {
        public bool ActivatorEnabled() => true;
    }
}
