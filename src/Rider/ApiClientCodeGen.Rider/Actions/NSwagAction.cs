using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Feature.Services.Menu;
using JetBrains.ReSharper.Psi.Resources;
using JetBrains.Util;

namespace Rapicgen.Rider.Actions
{
    [Action("NSwag", "Generate C# API client using NSwag")]
    [ActionGroup(ID = "RestApiClientCodeGenerator", InsertAfter = "REST API Client Code Generator")]
    public class NSwagAction : CodeGeneratorActionBase
    {
        public NSwagAction(ILogger logger) 
            : base("nswag", "NSwag", logger)
        {
        }
    }
}
