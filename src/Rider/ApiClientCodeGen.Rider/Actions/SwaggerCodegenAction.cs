using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Feature.Services.Menu;
using JetBrains.ReSharper.Psi.Resources;
using JetBrains.Util;

namespace Rapicgen.Rider.Actions
{
    [Action("Swagger Codegen", "Generate C# API client using Swagger Codegen")]
    [ActionGroup(ID = "RestApiClientCodeGenerator", InsertAfter = "REST API Client Code Generator")]
    public class SwaggerCodegenAction : CodeGeneratorActionBase
    {
        public SwaggerCodegenAction(ILogger logger) 
            : base("swagger", "Swagger Codegen", logger)
        {
        }
    }
}
