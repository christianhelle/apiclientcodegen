using System.IO;
using Cairo;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using Path = System.IO.Path;

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

    public class GenerateNSwagStudioCommandHandler : GenerateCommandHandler
    {
        private string nswagStudioFile;

        protected override void Run() => GenerateCode();

        protected override void Run(object dataItem) => GenerateCode();

        protected override void Update(CommandInfo info)
        {
            var item = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
            info.Visible = item?.Name?.EndsWith(".nswag", System.StringComparison.OrdinalIgnoreCase) == true;
            nswagStudioFile = item?.FilePath;
        }

        private void GenerateCode()
        {
            if (string.IsNullOrWhiteSpace(nswagStudioFile))
                return;

            var codeGenerator = new NSwagStudioCodeGenerator(
                nswagStudioFile,
                new DefaultGeneralOptions(),
                new ProcessLauncher(Path.GetDirectoryName(nswagStudioFile)));
            
            codeGenerator.GenerateCode(null);
        }
    }
}
