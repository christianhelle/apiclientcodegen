using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateAutoRestCommandHandler : GenerateCommandHandler
    {
    }

    public class GenerateNSwagCommandHandler : GenerateCommandHandler
    {
    }

    public class GenerateSwaggerCommandHandler : GenerateCommandHandler
    {
    }

    public class GenerateOpenApiCommandHandler : GenerateCommandHandler
    {
    }

    public abstract class GenerateCommandHandler : CommandHandler
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

        protected void Hide(CommandInfo info)
            => info.Visible = false;
    }

    public class GenerateNSwagStudioCommandHandler : GenerateCommandHandler
    {
        protected override void Update(CommandInfo info)
        {
            var item = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
            info.Visible = item?.Name?.EndsWith(".nswag", System.StringComparison.OrdinalIgnoreCase) == true;
        }
    }
}
