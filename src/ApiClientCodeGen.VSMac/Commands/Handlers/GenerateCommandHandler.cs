using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public abstract class GenerateCommandHandler : BaseCommandHandler
    {
        protected virtual string SupportedFileExtension => ".json";
        protected string FilePath { get; private set; }
        
        protected override void Update(CommandInfo info)
        {
            var item = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
            info.Visible = item?.Name?.EndsWith(SupportedFileExtension, System.StringComparison.OrdinalIgnoreCase) == true;
            FilePath = item?.FilePath;
        }
    }
}