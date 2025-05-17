using JetBrains.Application.DataContext;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Application.UI.ActionSystem.ActionsRevised.Menu;
using JetBrains.ProjectModel.DataContext;
using JetBrains.ReSharper.Feature.Services.Menu;
using JetBrains.ReSharper.Psi.Resources;

namespace Rapicgen.Rider.Actions
{
    [Action("REST API Client Code Generator", "Context menu group for code generation options")]
    public class RestApiClientCodeGeneratorAction : IAction
    {
        // This is just a parent menu item, no actual execution logic needed
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            var projectModelElement = context.GetData(ProjectModelDataConstants.PROJECT_MODEL_ELEMENT);
            if (projectModelElement == null)
                return false;
                
            // The child actions will determine if they should be enabled or not
            return true;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            // No action needed - this is just a parent menu item
            nextExecute();
        }
    }
}
