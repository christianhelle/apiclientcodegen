using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Feature.Services.Menu;
using JetBrains.ReSharper.Psi.Resources;
using JetBrains.Util;

namespace Rapicgen.Rider.Actions
{
    [Action("Kiota", "Generate C# API client using Kiota")]
    [ActionGroup(ID = "RestApiClientCodeGenerator", InsertAfter = "REST API Client Code Generator")]
    public class KiotaAction : CodeGeneratorActionBase
    {
        public KiotaAction(ILogger logger) 
            : base("kiota", "Kiota", logger)
        {
        }
    }
}
