using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;

namespace ApiClientCodeGen.VSMac.Commands.Handlers
{
    public class GenerateNSwagStudioCommandHandler : GenerateCommandHandler
    {
        private string nswagStudioFile;

        protected override void Run() => GenerateCode();

        protected override void Run(object dataItem) => GenerateCode();

        protected override void Update(CommandInfo info)
        {
            var item = IdeApp.ProjectOperations.CurrentSelectedItem as ProjectFile;
            info.Visible = item?.Name?.EndsWith(".nswag", StringComparison.OrdinalIgnoreCase) == true;
            nswagStudioFile = item?.FilePath;
        }

        private void GenerateCode()
        {
            if (string.IsNullOrWhiteSpace(nswagStudioFile))
                return;

            var codeGenerator = new NSwagStudioCodeGenerator(
                nswagStudioFile,
                new DefaultGeneralOptions(),
                new ProcessLauncher());
            
            codeGenerator.GenerateCode(null);
        }
    }
}