using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Feature.Services.Menu;
using JetBrains.ReSharper.Psi.Resources;
using JetBrains.Util;

namespace Rapicgen.Rider.Actions
{
    [Action("OpenAPI Generator", "Generate C# API client using OpenAPI Generator")]
    [ActionGroup(ID = "RestApiClientCodeGenerator", InsertAfter = "REST API Client Code Generator")]
    public class OpenApiGeneratorAction : CodeGeneratorActionBase
    {
        public OpenApiGeneratorAction(ILogger logger) 
            : base("openapi", "OpenAPI Generator", logger)
        {
        }
    }
}
