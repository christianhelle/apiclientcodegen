using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Feature.Services.Menu;
using JetBrains.ReSharper.Psi.Resources;
using JetBrains.Util;

namespace Rapicgen.Rider.Actions
{
    [Action("AutoRest", "Generate C# API client using AutoRest")]
    [ActionGroup(ID = "RestApiClientCodeGenerator", InsertAfter = "REST API Client Code Generator")]
    public class AutoRestAction : CodeGeneratorActionBase
    {
        public AutoRestAction(ILogger logger) 
            : base("autorest", "AutoRest", logger)
        {
        }
    }
}
