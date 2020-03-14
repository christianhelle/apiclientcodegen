using ApiClientCodeGen.VSMac.CustomTools.NSwag;
using MonoDevelop.Ide;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateNSwagCommandHandler : GenerateCommandHandler
    {
        protected override void Run() => SetGenerator();

        protected override void Run(object dataItem) => SetGenerator();

        private void SetGenerator()
        {
            var project = IdeApp.ProjectOperations.CurrentSelectedProject;
            var item = project.Files.GetFile(FilePath);
            item.Generator = NSwagSingleFileCustomTool.GeneratorName;
        }
    }
}