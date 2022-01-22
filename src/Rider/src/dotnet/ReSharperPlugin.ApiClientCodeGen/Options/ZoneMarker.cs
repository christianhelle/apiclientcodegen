using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.Application.UI.Options.OptionPages;

namespace ReSharperPlugin.ApiClientCodeGen.Options
{
    [ZoneMarker]
    public class ZoneMarker : IRequire<IToolsOptionsPageImplZone>
    {
    }
}