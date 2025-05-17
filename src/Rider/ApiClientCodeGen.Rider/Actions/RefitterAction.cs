using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Feature.Services.Menu;
using JetBrains.ReSharper.Psi.Resources;
using JetBrains.Util;

namespace Rapicgen.Rider.Actions
{
    [Action("Refitter", "Generate C# API client using Refitter")]
    [ActionGroup(ID = "RestApiClientCodeGenerator", InsertAfter = "REST API Client Code Generator")]
    public class RefitterAction : CodeGeneratorActionBase
    {
        public RefitterAction(ILogger logger) 
            : base("refitter", "Refitter", logger)
        {
        }
    }
}
