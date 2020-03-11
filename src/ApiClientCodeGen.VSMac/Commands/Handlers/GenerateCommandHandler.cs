using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public abstract class GenerateCommandHandler : BaseCommandHandler
    {
        protected override void Run()
        {
        }

        protected override void Run(object dataItem)
        {
        }

        protected override void Update(CommandInfo info)
        {
            var item = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
            info.Visible = item?.Name?.EndsWith(".json", System.StringComparison.OrdinalIgnoreCase) == true;
        }
    }
}