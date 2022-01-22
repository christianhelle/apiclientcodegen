using JetBrains.Application.BuildScript.Application.Zones;

namespace ReSharperPlugin.ApiClientCodeGen
{
    [ZoneMarker]
    public class ZoneMarker : IRequire<IApiClientCodeGenZone>
    {
    }
}